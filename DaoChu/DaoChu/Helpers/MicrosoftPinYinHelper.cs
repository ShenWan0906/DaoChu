using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ModelConvertToDto.Helpers
{
    public static class MicrosoftPinYinHelper
    {
        /// <summary>
        /// 返回字符串的简拼
        /// </summary>
        /// <param name="inputTxt"></param>
        /// <returns></returns>
        public static string GetShortPinYin(string inputTxt)
        {
            string shortPinYin = "";
            foreach (char c in inputTxt.Trim())
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    shortPinYin += chineseChar.Pinyins[0].Substring(0, 1).ToUpper();
                    continue;
                }

                if (Regex.IsMatch(c.ToString(), @"^[a-zA-Z]$"))
                {
                    shortPinYin += c.ToString().ToUpper();
                    continue;
                }

                shortPinYin += "?";
            }
            return shortPinYin;
        }

        /// <summary>
        /// 返回字符串全拼
        /// </summary>
        /// <param name="inputTxt"></param>
        /// <returns></returns>
        public static List<string> GetAllPinYin(string inputTxt)
        {
            var allPinYin = new List<string>();
            foreach (char c in inputTxt.Trim())
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    allPinYin.Add(chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1).ToUpper());
                }
                catch (Exception)
                {
                    
                }
            }
            return allPinYin;
        }
    }
}
