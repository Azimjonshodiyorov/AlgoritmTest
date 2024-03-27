using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(ElementlarFunc("K4(OK(SO3)2)2"));
        Console.WriteLine(ElementlarFunc("Na(OH)2"));
    }

    
    


    static string ElementlarFunc(string value)
    {
        var result = Func(value, 0, value.Length);
        var strb = new StringBuilder();

        foreach (var val in result.Keys.OrderBy(x=>x))
        {
            strb.Append(val);
            if (result[val] > 1)
                strb.Append(result[val]); 

        }
        return strb.ToString();
    }
    
    
    
        static IDictionary<string, int> Func(string value, int bosh, int len)
        {
            var result = new Dictionary<string, int>();

            var  count = 0;
            var elem = "";
            for (int i = bosh; i < len; i++)
            {
                var ch = value[i];
                if (char.IsDigit(ch))
                    count = count * 10 + ch - '0';
                else if (char.IsLower(ch))
                    elem += ch;
                else if (char.IsUpper(ch))
                {
                    if (elem != "")
                    {
                        if (result.ContainsKey(elem))
                            result[elem] += count == 0 ? 1 : count;
                        else
                            result[elem] = count == 0 ? 1 : count;
                    }
                    elem = ch.ToString();
                    count = 0;
                }
                else if (ch == '(')
                {
                    if (elem != "")
                    {
                        if (result.ContainsKey(elem))
                            result[elem] += count == 0 ? 1 : count;
                        else
                            result[elem] = count == 0 ? 1 : count;
                    }
                    elem = "";
                    
                    count = 0;

                    var nums = 1;
                    
                    var j = i;
                    
                    while (nums != 0)
                    {
                        j++;
                        if (value[j] == '(')
                            nums++;
                        else if (value[j] == ')')
                            nums--;
                    }
                    
                    var dic = Func(value, i + 1, j);
                    i = j;
                    while (++i < len && char.IsDigit(value[i]))
                        count = count * 10 + value[i] - '0';
                    i--;

                    foreach (var key in dic.Keys)
                    {
                        if (result.ContainsKey(key))
                            result[key] += dic[key] * count;
                        else
                            result[key] = dic[key] * count;
                    }
                    count = 0;
                }
            }
            
            
            
            
            if (elem != "")
            {
                if (result.ContainsKey(elem))
                    result[elem] += count == 0 ? 1 : count;
                else
                    result[elem] = count == 0 ? 1 : count;
            }
            
            
            return result;
        }
        
        
        
    
        
        
        
}
