using System;
namespace Cobol
{
	public class Variable
	{
		public Variable(string name_in, string value_in)
		{
			name = name_in;
			value = value_in;
		}

		public string name { get; set; }
		public int type { get; set; }
		public string value { get; set; }

		
	}
}

