using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ConvertNumberToWord.Common
{
    public abstract class NumToWordBase
    {
        // Arrays for text baesd on condition will pic by method
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty-", "Thirty-", "Forty-", "Fifty-", "Sixty-", "Seventy-", "Eighty-", "Ninety-" };
        string[] words3 = { "Thousand ", "hundred thousand ", "Million " };

        // centconversion bool parameter for cent conversion 
        private bool centconversion = false;

        //method to convert number to text
        protected string GetWordsFromNumber(string numbers)
        {
            StringBuilder resultString = new StringBuilder();

            // find index of '.' char to get amount after '.' char
            var centindex = numbers.IndexOf(".");

            var centamt = 0;
            int number = 0;

            // if index of '.' char is not negative or zero
            if (centindex > 0)
            {
                // get cent amount by substring method using '.' char index + 1 to 2 decimal poins
                centamt = Convert.ToInt32(numbers.Substring(centindex + 1, 2));
                centconversion = true;

                // get number before '.' char
                number = Convert.ToInt32(numbers.Substring(0, centindex));
            }
            else
            {
                // if '.' char is not present than direct convert number string to int datatype for calculation
                number = Convert.ToInt32(numbers);
            }

            // Zero condition
            if (number == 0) return "Zero dollar";

            // define array of int upto 4 length
            int[] num = new int[4];
            int first = 0;
            int u, h, t;


            if (number < 0)
            {
                // checking for minus amount
                resultString.Append("Minus ");
                number = -number;
            }

            num[0] = number % 1000; // get units from amount
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // get thousands from amount
            num[3] = number / 1000000; // get crores from amount
            num[2] = num[2] - 100 * num[3]; // get lakhs from amount


            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }

            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                // hundreds is greater than 0 append  Hundred text
                if (h > 0) resultString.Append(words0[h] + "Hundred ");

                // if unit or tens are greater than 0 than go inside condition
                if (u > 0 || t > 0)
                {
                    // hundreds and index value is 0 and number length is greater than 2 than appand this text
                    if (h > 0 || i == 0 && number.ToString().Length > 2) resultString.Append("and ");

                    // tens is 0 than appand words which are in single digit based on unit value (u)
                    if (t == 0)
                        resultString.Append(words0[u]);
                    else if (t == 1)
                        // tens is 1 than appand words which are > 10 and < than 20 digit based on unit value (u)
                        resultString.Append(words1[u]);
                    else
                        // appand text if t > 1 based on t and u values 
                        resultString.Append(words2[t - 2] + words0[u]);
                }

                // this will always call if i!=0 than appand text from word3 array based on i value
                if (i != 0) resultString.Append(words3[i - 1]);
            }

            // if input number is not in decimal 
            if (centamt == 0 && centconversion == false)
            {
                resultString.Append("dollars");
            }
            else if (centamt > 0) // if input number is in decimal 
            {
                var centtext = GetWordsFromNumber(centamt.ToString());
                resultString.AppendFormat("dollars and {0} cents", centtext);
            }

            // return result
            return resultString.ToString().TrimEnd();
        }

    }
}