namespace Cobol;
public class Program
{
    public string Name { get; set; }
    List<Variable> variables = new();
    public int IC = 0;//instruction counter

    public string getValue(string var_name)
    {
        string result = "NULL";
        foreach(Variable v in variables)
        {
            if (var_name == v.name)
            {
                result = v.value;
            }
        }
        return result;

    }

    public void setValue(string var_name, string var_value)
    {
        bool already_exists = false;
        int list_index = 0;
        int i = 0;
        foreach(Variable v in variables)
        {
            if (v.name == var_name)
            {
                already_exists = true;
                list_index = i;
            }
            i++;
        }
        if (already_exists)
        {
            variables[list_index].value = var_value;
        }
        else
        {
            Variable new_var = new(var_name, var_value);
            variables.Add(new_var);
           

        }

    }









}

