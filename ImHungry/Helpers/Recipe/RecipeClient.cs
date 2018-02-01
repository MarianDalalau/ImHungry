using ImHungry.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImHungry.Helpers.Recipe
{
    public class RecipeClient : ClientBase, IRecipeClient
    {

        private const string SearchApiUrl = "api/search";
        private const string RecipeRequestUrl = "api/get";
        private const string SearchKey = "q";
        private const string SortKey = "sort";  // can be r or t   rating/trendiness
        private const string APIKey = "key";
        private const string RecipeIdKey = "rId";

        List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

        public RecipeClient(IApiClient apiClient) : base(apiClient)
        {
            parameters.Add(new KeyValuePair<string, string>(APIKey, "f71d028931f04412459f4d1be98539f9"));
            
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="ingredients"></param>
        /// <returns></returns>
        public async Task<RecipeListResponse> GetRecipes(string intent, string[] ingredients)
        {
            string sortValue = "t";
            if (intent != null && intent != "popular")
            {
                sortValue = "r";
                //add this if not popular, for popular we make a general search
                parameters.Add(new KeyValuePair<string, string>(SearchKey, Utils.FormatSearchString(ingredients)));
            }
            parameters.Add(new KeyValuePair<string, string>(SortKey, sortValue));
            
            return await GetJsonDecodedContent<RecipeListResponse, RecipesModel>(SearchApiUrl, parameters.ToArray());
        }

        public async Task<RecipeResponse> GetRecipe(string recipeId)
        {
            parameters.Add(new KeyValuePair<string, string>(RecipeIdKey, recipeId));
            return await GetJsonDecodedContent<RecipeResponse, OneRecipeModel>(RecipeRequestUrl, parameters.ToArray());
        }
    }
}