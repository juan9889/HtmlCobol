using System;
namespace Client.Reader
{
	public class PageReader
	{
		public static string ReadPage(string content)
        {

            String page_content = content;

            String result_page = "";

            //Separate html and cobol
            List<String> html_parts = new();
            List<String> cobol_parts = new();
            int mode = 1;
            //mode 1 - html, mode 2-cobol
            char[] char_content = page_content.ToCharArray();
            int index = 0;
            int html_index = 0;
            int cobol_index = -1;
            string current_part = "";
            string first_html_part = "";
            html_parts.Add(first_html_part);
            foreach (char character in char_content)
            {

                if (character == '<' && char_content[index + 1] == '~')
                {
                    //Set mode to cobol
                    //First delete last 2 chars in previous html part
                    if (html_index > 0)
                    {
                        html_parts[html_index] = html_parts[html_index].Substring(2, html_parts[html_index].Length - 2);
                    }
                    mode = 2;
                    cobol_index++;
                    string new_cobol_part = "";
                    cobol_parts.Add(new_cobol_part);
                }
                if (character == '~' && char_content[index + 1] == '>')
                {
                    //Set mode to html
                    //But first delete las 2 chars in previous cobol part

                    cobol_parts[cobol_index] = cobol_parts[cobol_index].Substring(2, cobol_parts[cobol_index].Length - 2);
                    mode = 1;
                    html_index++;
                    string new_html_part = "";
                    html_parts.Add(new_html_part);

                }
                if (mode == 1)
                {
                    html_parts[html_index] = html_parts[html_index] + character.ToString();
                }
                if (mode == 2)
                {
                    cobol_parts[cobol_index] = cobol_parts[cobol_index] + character.ToString();
                }
                index++;
            }
            //Trim last cobol part
            html_parts[html_index] = html_parts[html_index].Substring(2, html_parts[html_index].Length - 2);
            //End read
            //Console.WriteLine("Parsing...");
            Cobol.Interpreter interpreter = new();
            interpreter.InitializeProgram("MUESTRA_NRO");
            int total_part_count = cobol_parts.Count + html_parts.Count;
            mode = 1;
            int html_i = 0;
            int cobol_i = 0;
            for (int i = 0; i < total_part_count; i++)
            {
                if (mode == 1)
                {
                    result_page = result_page + html_parts[html_i];
                    html_i++;

                }
                if (mode == 2)
                {
                    result_page = result_page + interpreter.InterpretCobol(cobol_parts[cobol_i]);
                    //Console.WriteLine("COBOL CODE : " + cobol_parts[cobol_i]);
                    //Console.WriteLine(interpreter.InterpretCobol(cobol_parts[cobol_i]));
                    cobol_i++;
                }
                if (mode == 1)
                {
                    mode = 2;
                }
                else
                {
                    mode = 1;
                }
            }

            //Console.WriteLine("Writing html...");
            return result_page;


        }
    }
}

