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
        /// <param name="elements"></param>
        /// <returns></returns>
        public static string FormatSearchString(string[] elements)
        {
            if (elements == null)
                return string.Empty;
            return  string.Join(",", elements);
            
        }
    }
}