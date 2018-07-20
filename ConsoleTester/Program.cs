using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = ArraysAndStrings.InterviewQuestions.URLify("uno dos tres   ".ToCharArray());
            Console.WriteLine(result);

            Console.ReadLine();
        }

        //TODO: check for the longest permutation
    }
}
