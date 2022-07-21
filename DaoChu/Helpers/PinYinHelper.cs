using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ModelConvertToDto.Helpers
{
    public class PinYinHelper
    {
        /// <summary>
        /// 汉字拼音
        /// </summary>
        /// <param name="columns">汉字</param>
        /// <param name="columnName">大写</param>
        /// <returns></returns>
        public static string PinYin(string columns, string columnName)
        {
            var name = columnName.Clone().ToString();
            if (!string.IsNullOrWhiteSpace(columns))
            {
                var comments = Regex.Replace(columns, "^[A-Za-z0-9]+$", "", RegexOptions.IgnoreCase);
                if (!string.IsNullOrWhiteSpace(comments))
                {
                    var pinYinList = MicrosoftPinYinHelper.GetAllPinYin(comments); //NPinyinHelper.GetAllPinYin(comments);
                    if (pinYinList.Any())
                    {
                        columnName = ConvertColumnName(columnName, pinYinList);
                    }
                    if (name == columnName)
                    {
                        pinYinList = NPinyinHelper.GetAllPinYin(comments);
                        if (pinYinList.Any())
                        {
                            columnName = ConvertColumnName(columnName, pinYinList);
                        }
                    }
                }
            }
            return columnName;
        }

        /// <summary>
        /// 汉字拼音
        /// </summary>
        /// <param name="columns">汉字</param>
        /// <returns></returns>
        public static string PinYin(string columns)
        {
            var columnName = string.Empty;
            if (!string.IsNullOrWhiteSpace(columns))
            {
                var comments = Regex.Replace(columns, "^[A-Za-z0-9]+$", "", RegexOptions.IgnoreCase);
                if (!string.IsNullOrWhiteSpace(comments))
                {
                    var pinYinList = MicrosoftPinYinHelper.GetAllPinYin(comments); //NPinyinHelper.GetAllPinYin(comments);
                    if (pinYinList.Any())
                    {
                        columnName = ConvertColumnName( pinYinList);
                    }
                    if (string.IsNullOrWhiteSpace(columnName) )
                    {
                        columnName = ConvertColumnName(columnName, pinYinList);
                    }
                }
            }
            return columnName;
        }

        private static string ConvertColumnName(string columnName, List<string> pinYinList)
        {
            var count = pinYinList.Count();
            var i = 0;
            var final = false;
            while (i < count - 1 && !final)
            {
                var temp = columnName;
                //匹配第一个拼音
                if (!string.IsNullOrWhiteSpace(pinYinList[i]) && columnName.Contains(pinYinList[i]))
                {
                    var indexOf = temp.IndexOf(pinYinList[i]);
                    if (indexOf == 0)
                    {
                        temp = temp.Substring(pinYinList[i].Length, temp.Length - indexOf - pinYinList[i].Length);
                        //匹配第二个拼音
                        indexOf = temp.IndexOf(pinYinList[i + 1]);
                        if (indexOf == 0)
                        {
                            temp = temp.Substring(pinYinList[i + 1].Length, temp.Length - indexOf - pinYinList[i + 1].Length);
                            //如果只有两个字
                            if (string.IsNullOrWhiteSpace(temp))
                            {
                                columnName = FirstCharToUpper(pinYinList[i]) + FirstCharToUpper(pinYinList[i + 1]);
                                final = true;
                            }
                            else
                            {
                                //如果三个汉字
                                if (i + 2 <= count - 1 && temp == pinYinList[i + 2])
                                {
                                    columnName = FirstCharToUpper(pinYinList[i]) + FirstCharToUpper(pinYinList[i + 1]) + FirstCharToUpper(pinYinList[i + 2]);
                                    final = true;
                                }
                                else//否则前面两个首字母大写
                                {
                                    columnName = columnName.Replace(pinYinList[i], FirstCharToUpper(pinYinList[i])).Replace(pinYinList[i + 1], FirstCharToUpper(pinYinList[i + 1]));
                                    final = true;
                                }
                            }
                        }
                        i++;
                    }
                }
                i++;
            }
            return columnName;
        }

        private static string ConvertColumnName(List<string> pinYinList)
        {
            var column = string.Empty;
            if (pinYinList.Count > 3)
            {
                var index = 0;
                foreach (var temp in pinYinList)
                {
                    column += index < 2 ? FirstCharToUpper(temp) : SimpleToUpper(temp);
                    index++;
                }
            }
            else
            {
                foreach (var temp in pinYinList)
                {
                    column += FirstCharToUpper(temp);
                }
            }
            return column;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            return str;
        }
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string SimpleToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                return input;
            return input.First().ToString().ToUpper();
        }
    }
}
