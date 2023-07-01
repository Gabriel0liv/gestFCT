using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GestaoFCT
{
    public class GlobalFunctions
    {
        //verificar se a string contém palavras-chave suspeitas de SQL Injection
        public static bool SqlInjectionChecker(string input)
        {
            string[] sqlKeywords = { "SELECT", "INSERT", 
                                    "UPDATE", "DELETE", 
                                    "DROP", "TRUNCATE", 
                                    "EXECUTE", "ALTER", 
                                    "CREATE", "TABLE", 
                                    "UNION", "WHERE", 
                                    "OR", "AND" };
            string[] inputWords = input.ToUpper().Split(' ');

            foreach (string word in inputWords)
            {
                if (sqlKeywords.Contains(word))
                {
                    return true; // A string contém uma palavra-chave suspeita de SQL Injection
                }
            }

            return false; // A string não contém palavras-chave suspeitas de SQL Injection
        }

        //verificar se a string contém caracteres especiais ou padrões comuns
        //usados em tentativas de SQL Injection
        public static bool RegexInjectionChecker(string input)
        {
            string pattern = @"[;'\(\)\[\]{}<>%]";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(input))
            {
                return true; // A string contém caracteres especiais suspeitos de SQL Injection
            }

            return false; // A string não contém caracteres especiais suspeitos de SQL Injection
        }


        // As duas funções de prevenção de SQL Injection combinadas
        // Verificação mais eficiente
        public static bool HasSqlInjection(string input)
        {
            return SqlInjectionChecker(input) || RegexInjectionChecker(input);
        }


    }
}