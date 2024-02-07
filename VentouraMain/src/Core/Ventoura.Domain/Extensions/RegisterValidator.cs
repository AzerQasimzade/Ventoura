using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ventoura.Domain.Extensions
{
    public static class RegisterValidator
    {
        public static bool IsEmailValid(this string Email)
        {
            Regex regex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            return regex.IsMatch(Email);
        }
        public static string Capitalize(this string name)
        {
            string[] strings = name.Split(' ');
            for (int i = 0; i < strings.Length; i++)
            {
                if (!string.IsNullOrEmpty(strings[i]))
                {
                    strings[i] = char.ToUpper(strings[i][0]) + strings[i].Substring(1);
                }
            }
            return string.Join(" ", strings);
        }
        public static bool IsDigit(this string name)
        {
            foreach (var item in name)
            {
                if (Char.IsDigit(item))
                {
                    return true;
                }  
            }
            return false;
        }
        public static bool ContainsSymbol(this string name)
        {
            foreach (var item in name)
            {
                if (!Char.IsLetterOrDigit(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
