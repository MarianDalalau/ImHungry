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
        private const string SortKey = "sort";
        private const string APIKey = "key";
        private const string RecipeIdKey = "rId";

        List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();

        public RecipeClient(IApiClient apiClient) : base(apiClient)
        {
            parameters.Add(new KeyValuePair<string, string>(APIKey, "f71d028931f04412459f4d1be98539f9"));
            
        }

        public async Task<RecipeListResponse> GetRecipes(string searchStr)
        {
            parameters.Add(new KeyValuePair<string, string>(SortKey, "r"));
            parameters.Add(new KeyValuePair<string, string>(SearchKey, Utils.FormatSearchString(searchStr)));
            return await GetJsonDecodedContent<RecipeListResponse, RecipesModel>(SearchApiUrl, parameters.ToArray());
        }

        public async Task<RecipeResponse> GetRecipe(int recipeId)
        {
            parameters.Add(new KeyValuePair<string, string>(RecipeIdKey, recipeId.ToString()));
            return await GetJsonDecodedContent<RecipeResponse, OneRecipeModel>(RecipeRequestUrl, parameters.ToArray());
        }
    }
}