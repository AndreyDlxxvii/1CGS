using System.Collections.Generic;

namespace Code
{
    public class NumberSeparator
    {
        public const char DEFAULT_SEPARATOR = ' ';
        
        public static string Separate(int value, char separator = DEFAULT_SEPARATOR, int oneBlockSize = 3)
        {
            var stringedValue = value.ToString();
            
            if (stringedValue.Length <= oneBlockSize)
                return stringedValue;
            
            if (separator == default)
                separator = (char)32;

            var charArray = stringedValue.ToCharArray();
            var result = new List<char>();
            var counter = 0;
            
            for (var i = charArray.Length - 1; i >= 0; i--)
            {
                result.Add(charArray[i]);
                counter++;
                
                if (counter != oneBlockSize)
                    continue;
                
                counter = 0;
                result.Add(separator);
            }

            result.Reverse();
            return new string(result.ToArray());
        }
            
    }
}