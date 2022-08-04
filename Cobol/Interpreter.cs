using System;
using System.Text.RegularExpressions;

namespace Cobol
{
	public class Interpreter
	{
        Program program;
		public string InterpretCobol(string cobol_code)
        {
            string code_aux = spaces_formatting(cobol_code);
            Console.WriteLine("code = "+code_aux);
            cobol_code = code_aux;
            code_aux = "";
			String[] statements;
			string result = "";
            //split the cobol code in statements
            try
            {
				statements = cobol_code.Split(".");

            }
            catch (Exception)
            {
                return "Error while splitting code in statements";
            }
            //Interpret every statement
            //
            //foreach(string statement in statements)
            program.IC = 0;
            while(program.IC<statements.Length-1)
            {
                
                string curr_result = InterpretStatement(statements[program.IC]);
                if (curr_result != "")
                {
                    if (curr_result.Contains("$GOTO_RESULT="))
                    {
                        string line_str = curr_result.Substring(13, curr_result.Length-13);
                        int line_to_jump=int.Parse(line_str);
                        if (line_to_jump <= statements.Length)
                        {
                            program.IC = line_to_jump;
                            Console.WriteLine("JMP TO " + line_to_jump.ToString());
                        }
                       
                        curr_result = "";
                    }
                    
                    result = result + Environment.NewLine;
                    result = result + curr_result;
                }
                program.IC++;
            }
			return result;
        }
        public void InitializeProgram(string name)
        {
            Program p = new();
            p.Name = name;
            program = p;
        }
        public string InterpretStatement(string statement)
        {
            string result = "";
            string[] statement_words;
            statement_words = statement.Split(" ");
            //Console.WriteLine("DOING = "+statement);
            //Try to interpret statements in first word
            switch (statement_words[0])
            {
                case "SET":
                    //set a variable
                    string var_to_set=statement_words[1];
                    string value = statement_words[3];
                    program.setValue(var_to_set, value);
                    break;
                case "DISPLAY":
                    //display value
                    string var_to_display = statement_words[1];
                    result = program.getValue(var_to_display);
                    //Console.WriteLine("INTERNAL DISPLAY REACHED :" + result);
                    break;
                case "COMPUTE":
                    result = this.Compute(statement);
                    break;
                case "PERFORM":
                    result = this.Perform(statement);
                    break;
                case "GOTO":
                    result = this.Goto(statement);
                    break;
                default:
                    break;
            }
            
            //Console.WriteLine(result);
            return result;
        }
        

        private string Compute(string statement)
        {
            //Only works with 2 operands for now
            string result = "";
            string[] words;
            words = statement.Split(" ");
            string var_to_compute = words[1];
            string v1 = words[3];
            string v2 = words[5];
            double v1_num=0;
            double v2_num=0;

            //try parsing this, if it fails, try getting a value, if this also fails return null
            try
            {
                v1_num = double.Parse(v1);
            }
            catch
            {
                try
                {
                    v1_num = double.Parse(program.getValue(v1));
                }
                catch
                {
                    //Not a number, or a variable
                }
            }
            try
            {
                v2_num = double.Parse(v2);
            }
            catch
            {
                try
                {
                    v2_num = double.Parse(program.getValue(v2));
                }
                catch
                {
                    //Not a number, or a variable
                }
            }
            switch (words[4]){
                case "+":
                    result = (v1_num+v2_num).ToString();
                    break;
                case "-":
                    result = (v1_num-v2_num).ToString();
                    break;
                case "x":
                    result = (v1_num*v2_num).ToString();
                    break;
                case "/":
                    result = (v1_num/v2_num).ToString();
                    break;
            }
            program.setValue(var_to_compute, result);
            result = "";
            //result = "Computed";
            return result;
        }

        private string Perform(string statement)
        {
            //Supported perform statements :
            // -Perform N TIMES
            //
            string result = "";

            return result;
        }

        private string Goto(string statement)
        {
            string result = "";
            string[] words;
            words = statement.Split(" ");
            result = "$GOTO_RESULT=" + words[1];
            return result;
        }
        private string spaces_formatting(string cobol_code)
        {
            string result= Regex.Replace(cobol_code, @"\s+", " ");
            result = result.Replace("\n", "");
            
            return result;
        }
	}
}

