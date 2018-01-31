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

        public async Task<ActionResult> Index(string search)
        {
            var response = await recipeClient.GetRecipes(search);
            if (!response.StatusIsSuccessful)
                ModelState.AddModelError("apiError", "There is an error retrieving the recipes!");
            return View(response);
        }

        public async Task<ActionResult> Detail(int id)
        {
            var response = await recipeClient.GetRecipe(id);
            if (!response.StatusIsSuccessful)
                ModelState.AddModelError("apiError", "There is an error retrieving the recipe!");
            return View(response);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}