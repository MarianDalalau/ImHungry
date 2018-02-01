using ImHungry.Helpers.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImHungry.Models
{
    public class SearchModel
    {
        public string Intent { get; set; }
        public string[] Ingredients { get; set; }
    }
}