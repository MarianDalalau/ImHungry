using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImHungry.Helpers.Recipe
{
    public class RecipesModel 
    {
        /// <summary>
        ///  Number of recipes in result (Max 30)
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// List of Recipe Parameters 
        /// </summary>
        public List<RecipeModel> Recipes { get; set; }    
    }

    public class OneRecipeModel
    {
        /// <summary>
        /// The Recipe
        /// </summary>
        public RecipeModel Recipe { get; set; }
    }

    public class RecipeModel
    {
        /// <summary>
        /// The Recipe Id
        /// </summary>
        public string Recipe_id { get; set; }

        /// <summary>
        /// Name of the Publisher
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Title of the recipe
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL of the image
        /// </summary>
        public string Image_url { get; set; }

        /// <summary>
        /// Original Url of the recipe on the publisher's site
        /// </summary>
        public string Source_url { get; set; }        

        /// <summary>
        ///  Url of the recipe on Food2Fork.com
        /// </summary>
        public string F2f_url { get; set; }

        /// <summary>
        /// Base url of the publisher
        /// </summary>
        public string Publisher_url { get; set; }

        /// <summary>
        /// The Social Ranking of the Recipe (As determined by our Ranking Algorithm)
        /// </summary>
        public string Social_rank { get; set; }

        ///// <summary>
        ///// The page number that is being returned(To keep track of concurrent requests)
        ///// </summary>
        public string Page { get; set; }

        /// <summary>
        /// Ingredients of the recipe
        /// </summary>
        public string[] Ingredients { get; set; }
    }
}