using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImHungry.Helpers
{
    public class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static string FormatSearchString(string search)
        {
            if (string.IsNullOrEmpty(search))
                return string.Empty;
            return  string.Join(",", search.Split(new char[] { ' ' }));
            
        }
    }
}