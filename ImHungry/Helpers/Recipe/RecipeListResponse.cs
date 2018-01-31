using ImHungry.Helpers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImHungry.Helpers.Recipe
{
    public class RecipeListResponse : ApiResponse<RecipesModel>
    {
    }

    public class RecipeResponse : ApiResponse<OneRecipeModel>
    {
    }
}