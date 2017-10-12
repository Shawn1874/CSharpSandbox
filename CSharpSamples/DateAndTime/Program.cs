using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAndTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.UtcNow;
            Console.WriteLine(now.ToString());

            DateTime dateValue = new DateTime(2008, 6, 15, 21, 15, 07);
            // Create an array of standard format strings.
            string[] standardFmts = {"d", "D", "f", "F", "g", "G", "m", "o",
                               "R", "s", "t", "T", "u", "U", "y"};
            // Output date and time using each standard format string.
            foreach (string standardFmt in standardFmts)
                Console.WriteLine("{0}: {1}", standardFmt,
                                  dateValue.ToString(standardFmt));
            Console.WriteLine();

            // Create an array of some custom format strings.
            string[] customFmts = {"h:mm:ss.ff t", "d MMM yyyy", "HH:mm:ss.f",
                             "dd MMM HH:mm:ss", @"\Mon    \h\: M", "HH:mm:ss.ffffzzz" };
            // Output date and time using each custom format string.
            foreach (string customFmt in customFmts)
                Console.WriteLine("'{0}': {1}", customFmt,
                                  dateValue.ToString(customFmt));

            Console.WriteLine("Test Time Spans");

            var ts = new TimeSpan(0, 0, 5); // 5 seconds
            Console.WriteLine(ts.ToString());

            Console.WriteLine(ts.ToString("ss"));
            Console.WriteLine(ts.ToString("%s"));
        }
    }
}
