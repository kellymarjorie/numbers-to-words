using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CucumberTechnicalTest.Models
{
    public class CurrencyConverterModel
    {
        public string YourNameHere { get; set; }
        public decimal NumberToConvert { get; set; }
        public string ResultingText { get; set; }

        public string str { get; set; }
        public string teens { get; set; }
        public List<string> b { get; set; }
        public List<string> answer { get; set; }
        public string cents { get; set; }
        public List<string> units { get; set; }
        public List<int> teensNum { get; set; }
        public List<string> teensWord { get; set; }
        public List<string> tens { get; set; }
        public List<string> scales { get; set; }
        public int CentNum { get; set; }

        public void CalculateCents(decimal number)
        {
            if (number % 1 != 0)
            {
                // checks to see if cents in the number
                cents = str.Substring(str.IndexOf(".")); // saves the cents value to a new string
                str = str.Replace(cents, ""); // removes the cents from the original string
                cents = cents.Replace(".", ""); //removes the decimal point from the cents
            }
            else
            {
                return;
            }

            IEnumerable<string> centsArray = cents.Split("");
            CentNum = Convert.ToInt32(centsArray.ElementAt(0));
            if (CentNum >= 11 && CentNum <= 19)
            {
                for (var j = 0; j < teensNum.Count; j++)
                { // runs through the array of teen nums
                    if (CentNum == teensNum[j])
                    { // if the teen from my num matches one of the teens in the array
                        b.Insert(0, teensWord[j]); // push the word to the temp answer array
                    }
                }
            }
            else
            {
                // handle case where cent value is passed single digit
                var unitNum = centsArray.ElementAt(0).Length > 1  ? Convert.ToInt32(centsArray.ElementAt(0)[1].ToString()) : Convert.ToInt32("0"); // takes the last num in the str to a new var
               // converts it to a num
                if (unitNum > 0)
                {
                    b.Insert(0, units[unitNum - 1]); // pushes the word to temp answer array
                }
                var tensNum = Convert.ToInt32(centsArray.ElementAt(0)[0].ToString()); // takes the second num in the str to a new var for the tens
                // converts it to a num
                if (tensNum > 0)
                {
                    b.Insert(0, tens[tensNum - 1]); // pushes the word to temp answer array
                }
            }
            b.Insert(0, "and");
            b.Add("cents");
            answer.Add(string.Join(" ", b)); // pushes b to the answer  
            b.Clear();
        }

        public void CalculateDollars(IEnumerable<string> listOfThrees)
        {
            for (var i = 0; i < listOfThrees.Count(); i++)
            {
                var a = listOfThrees.ElementAt(i); // for storing the length of this set
                var q = Convert.ToInt32(a); // for converting the set back to an integer

                // if statement to add in the scale words - thousand, million, etc
                if (q > 0 && i > 0)
                {
                    answer.Insert(0, scales[i]);
                }

                //check for teens
                teens = listOfThrees.ElementAt(i).Length >= 2 ? listOfThrees.ElementAt(i)[a.Length - 2].ToString() + listOfThrees.ElementAt(i)[a.Length - 1].ToString() : null; // adds the last two nums in the str to a new var
                int steens = Convert.ToInt32(teens); // converts it to a num to compare

                if (steens <= 19 && steens >= 11)
                { // checking if the var matches a teen num
                    for (var j = 0; j < teensNum.Count; j++)
                    { // runs through the array of teen nums
                        if (steens == teensNum[j])
                        { // if the teen from my num matches one of the teens in the array
                            b.Insert(0, teensWord[j]); // push the word to the temp answer array
                        }
                    }
                    // next checks for any valid numbers in the hundreds field
                    var bigNum = listOfThrees.ElementAt(i).Length > 2 ? listOfThrees.ElementAt(i)[a.Length - 3].ToString() : null;
                    int bigNums = Convert.ToInt32(bigNum);
                    if (bigNums > 0)
                    {
                        b.Insert(0, units[bigNums - 1] + " hundred");
                        if (b.Count > 1)
                        { // if there are numbers inside this hundred, add an 'and' to make sense
                            b.Insert(1, "and");
                        }
                    }
                    answer.Insert(0, string.Join(" ", b)); // pushes b to the answer  
                    b.Clear(); // clears the temp array for the next round 
                }
                else if (q > 0)
                { // if not a teen but the string is greater than 0, now working bakwards through the str to work out the correct two nums

                    var unitNum = listOfThrees.ElementAt(i)[a.Length - 1].ToString(); // takes the last num in the str to a new var
                    int unitNums = Convert.ToInt32(unitNum); // converts it to a num
                    if (unitNums > 0)
                    {
                        b.Insert(0, units[unitNums - 1]); // pushes the word to temp answer array
                    }

                    var tensNum = listOfThrees.ElementAt(i).Length >= 2 ? listOfThrees.ElementAt(i)[a.Length - 2].ToString() : null; // takes the second num in the str to a new var for the tens
                    int tensNums = Convert.ToInt32(tensNum); // converts it to a num
                    if (tensNums > 0)
                    {
                        b.Insert(0, tens[tensNums - 1]); // pushes the word to temp answer array
                    }

                    var bigNum = listOfThrees.ElementAt(i).Length > 2 ? listOfThrees.ElementAt(i)[a.Length - 3].ToString() : null;
                    int bigNums = Convert.ToInt32(bigNum);
                    if (bigNums > 0)
                    { // checks if it's over 0
                        b.Insert(0, units[bigNums - 1] + " hundred"); // pushes the number with hundred to the temp array
                        if (b.Count > 1)
                        { // if there are other numbers, this adds the and
                            b.Insert(1, "and");
                        }
                    }

                    // check if it's the first item in array, so last in the number, and if less than 100 it will need 'and' in front of it.
                    if (i == 0 && q < 100 && listOfThrees.Count() > 1)
                    {
                        b.Insert(0, "and");
                    }

                    answer.Insert(0, string.Join(" ", b)); // pushes b to the answer  
                    b.Clear(); // clears the temp array for the next round 
                }
            }
        }

        public string ConvertNumberToString(decimal numberToConvert)
        {
            b = new List<string>();
            units = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            teensNum = new List<int> { 11, 12, 13, 14, 15, 16, 17, 18, 19 };
            teensWord = new List<string> { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            tens = new List<string> { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            scales = new List<string> { "", " thousand ", " million " };
            answer = new List<string>();
            if (numberToConvert > 999999999) // set max value at 999 million, 999 thousand, 999
            {
                ResultingText = "waaaaaaay too much money. Wowzers!";
                return ResultingText;
            }

            if (numberToConvert < 1)
            {
                answer.Add("Zero dollars ");
            }
            else
            {
                answer.Add(" dollars ");
            }

            str = numberToConvert.ToString(); // converts number to string

            CalculateCents(numberToConvert);

            var numAsString = String.Format("{0:n0}", Convert.ToInt32(str));
            IEnumerable<string> listOfThrees = numAsString.Split(",").Reverse();

            // only calculate dollar amount if greater than zero - zero is already handled above
            if (numberToConvert > 1)
            {
                CalculateDollars(listOfThrees);
            }

            foreach (var item in answer)
            {
                ResultingText += item;
            }
            return ResultingText;
        }
    }
}
