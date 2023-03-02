using NPinyin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelConvertToDto.Helpers
{
    public static class NPinyinHelper
    {
        public static List<string> GetAllPinYin(string comments)
        {
            var pinYin = Pinyin.GetPinyin(comments);
            var pinYinList = new List<string>();
            if (!string.IsNullOrWhiteSpace(pinYin))
            {
                pinYinList = pinYin.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.ToUpper()).ToList();
            }
            return pinYinList;
        }
    }
}
