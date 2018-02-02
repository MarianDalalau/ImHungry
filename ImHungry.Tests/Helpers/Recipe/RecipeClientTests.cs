using ImHungry.Helpers.Api;
using ImHungry.Helpers.Recipe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImHungry.Tests.Helpers.Recipe
{
    [TestClass()]
    public class RecipeClientTests
    {
        [TestMethod()]
        public void GetRecipesTestFails()
        {
            Mock<IApiClient> mock = new Mock<IApiClient>();

            IRecipeClient client = new RecipeClient(mock.Object);

            Task<RecipeResponse> response = client.GetRecipe("");

            var actual = response.Status;
            var expected = TaskStatus.Faulted;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetRecipeTest()
        {
            Mock<IApiClient> mock = new Mock<IApiClient>();
            
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                { Content = new StringContent("{ 'recipe': { 'publisher': 'Jamie Oliver', 'f2f_url': 'http://food2fork.com/view/bc8acd', 'ingredients': ['12  Jacob's cream crackers', '8 sprigs of fresh flat-leaf parsley', '500 g quality minced beef', '2 heaped tablespoons Dijon mustard, optional'], 'source_url': 'http://www.jamieoliver.com/recipes/beef-recipes/a-cracking-burger', 'recipe_id': 'bc8acd', 'image_url': 'http://static.food2fork.com/7_1_1350663561_lrgf1c4.jpg', 'social_rank': 99.99999543148996, 'publisher_url': 'http://www.jamieoliver.com', 'title': 'A cracking burger'}}") };

            mock.SetReturnsDefault<Task<HttpResponseMessage>>(Task.FromResult(responseMessage));

            IRecipeClient client = new RecipeClient(mock.Object);
            
            mock.Setup(x => x.GetFormEncodedContent("", new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("", "") }))                
                .Returns(Task.FromResult(responseMessage));

            Task<RecipeResponse> response = client.GetRecipe("bc8acd");

            var actual = response.Status;
            var expected = TaskStatus.WaitingForActivation;
            Assert.AreEqual(expected, actual);
        }
    }
}
