using System;
using System.Text;

namespace CustomString
{
    public class Methods
    {
        public static string reverse(String input)
        {
            var reversed = new StringBuilder();
            int j = 0;
            for (int i = input.Length - 1; i >= 0; --i)
            {
                reversed.Append(input[i]);
            }
            return reversed.ToString();
        }
    }
}
