using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mvc.ConsApp {
    class Program {
        static void Main(string[] args) {
            string input = @"

            Console.WriteLine(input);
            string pattern = "^<a class=\"Print\"\\s+href=\"[~/\\w]+\">打印</a>$";
            string result = Regex.Replace(input, pattern, "");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
