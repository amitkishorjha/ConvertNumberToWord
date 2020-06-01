using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConvertNumberToWord.Common
{
    public class NumToWordLogic : NumToWordBase
    {
        //method to call abstract call method which protected
        public string GetWords(string numbers)
        {
            return GetWordsFromNumber(numbers);
        }
    }
}