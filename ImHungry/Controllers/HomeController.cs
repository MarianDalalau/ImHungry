using ImHungry.Helpers;
using ImHungry.Helpers.Api;
using ImHungry.Helpers.Recipe;
using ImHungry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImHungry.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRecipeClient recipeClient;

        public HomeController()
        {
            var apiClient = new ApiClient(HttpClientInstance.Instance);
            recipeClient = new RecipeClient(apiClient);
        }

        public HomeController(IRecipeClient recipeClient)
        {
            this.recipeClient = recipeClient;
        }

        public async Task<ActionResult> Index()
        {
            var response = await recipeClient.GetRecipes("", null);
            if (!response.StatusIsSuccessful)
                ModelState.AddModelError("apiError", "There is an error retrieving the recipes!");
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> RefreshRecipeList(SearchModel model)
        {
            var response = await recipeClient.GetRecipes(model.Intent, model.Ingredients);

            string result = ControllerContext.RenderPartialToString("_RecipeList", response);

            return Json(new {
                recipeList = result
            });
        }

        public async Task<ActionResult> Detail(string id)
        {
            var response = await recipeClient.GetRecipe(id);
            if (!response.StatusIsSuccessful)
                ModelState.AddModelError("apiError", "There is an error retrieving the recipe!");
            return View(response);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}