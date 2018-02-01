using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImHungry.Helpers.Recipe
{
    public interface IRecipeClient
    {
        Task<RecipeListResponse> GetRecipes(string intent, string[] ingredients);
        Task<RecipeResponse> GetRecipe(string recipeId);
    }
}