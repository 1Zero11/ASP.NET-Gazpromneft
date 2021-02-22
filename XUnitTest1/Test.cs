using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest1
{
    class Test
    {
        public static int BinaryArrayToNumber(int[] binaryArray)
        {
            int multiplier = 1;
            int output = 0;
            Array.Reverse(binaryArray);

            foreach(int digit in binaryArray)
            {
                output += digit * multiplier;
                multiplier *= 2;
            }
            return output;
        }

        public static string ReverseWordsAndCamelCase(string str)
        {
            string output = "";
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < str.Length; i++)
            {
                char letter = str[i];
                if (letter == ' ')
                {
                    output += char.ToUpper(stack.Pop());
                    while (stack.Count > 0)
                    {
                        output += stack.Pop();
                    }
                }
                else
                    stack.Push(letter);

                if (i == str.Length - 1)
                {
                    output += char.ToUpper(stack.Pop());
                    while (stack.Count > 0)
                    {
                        output += stack.Pop();
                    }
                }
            }


            return output;
        }

        public class NumericShiftDetector
        {
            // метод принимает массив, составленный из отсортированного набора чисел, "сдвинутого" на некоторое количество позиций
            public int GetShiftPosition(int[] initialArray)
            {

                if (initialArray[0] < initialArray[initialArray.Length - 1])
                    return 0;

                return GetPosition(ref initialArray, 0, initialArray.Length);


            }

            private int GetPosition(ref int[] initialArray, int start, int end)
            {
                int middle = (end+start) / 2;
                
                
                if (initialArray[end -1] >= initialArray[middle])
                    if (start == middle)
                        return middle + 1;
                    else
                        return GetPosition(ref initialArray, start, middle);
                else
                    if (middle == end)
                        return end;
                    else
                        return GetPosition(ref initialArray, middle, end);
                
            }

        }


    }
}
