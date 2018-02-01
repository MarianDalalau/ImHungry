﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ImHungry.Helpers
{
    public static class RenderPartialToStringExtensions
    {
        /// <summary>
        /// render PartialView and return string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderPartialToString(this ControllerContext context, string partialViewName, object model)
        {
            return RenderPartialToStringMethod(context, partialViewName, model);
        }

        private static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, object model)
        {
            ViewDataDictionary viewData = new ViewDataDictionary(model);
            TempDataDictionary tempData = new TempDataDictionary();
            return RenderPartialToStringMethod(context, partialViewName, viewData, tempData);
        }

        private static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(context, partialViewName);

            if (result.View != null)
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter output = new HtmlTextWriter(sw))
                    {
                        ViewContext viewContext = new ViewContext(context, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }
            return String.Empty;
        }
    }
}