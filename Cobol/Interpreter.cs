using System;
namespace Cobol
{
	public class Interpreter
	{
        Program program;
		public string InterpretCobol(string cobol_code)
        {
			String[] statements;
			string result = "";
            //slash the cobol code in statements
            try
            {
				statements = cobol_code.Split(".");

            }
            catch (Exception)
            {
                return "Error while splitting code in statements";
            }
            //Interpret every statement
            foreach(string statement in statements)
            {
                string curr_result = InterpretStatement(statement);
                if (curr_result != "")
                {
                    result = result + Environment.NewLine;
                    result = result + curr_result;
                }
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
                default:
                    break;
            }
            
            //Console.WriteLine(result);
            return result;
        }
        

        private string Compute(string statement)
        {
            string result = "";
            string[] words;
            words = statement.Split(" ");
            string var_to_compute = words[1];
            string v1 = words[3];
            string v2 = words[5];

            switch(words[4]){
                case "+":
                    result = ((double.Parse(v1) + double.Parse(v2)).ToString());
                    break;
                case "-":
                    result = ((double.Parse(v1) - double.Parse(v2)).ToString());
                    break;
                case "x":
                    result = ((double.Parse(v1) * double.Parse(v2)).ToString());
                    break;
                case "%":
                    result = ((double.Parse(v1) / double.Parse(v2)).ToString());
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
	}
}

