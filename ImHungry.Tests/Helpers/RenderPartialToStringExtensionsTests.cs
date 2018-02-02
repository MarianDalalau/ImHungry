using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ImHungry.Helpers;
using ImHungry.Controllers;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using System.Security.Principal;
using Moq;
using System.Web.Routing;

namespace ImHungry.Tests.Helpers
{
    [TestClass]
    public class RenderPartialToStringExtensionsTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            var mock = new Mock<ControllerContext>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "HomeController");
            mock.SetupGet(m => m.RouteData).Returns(routeData);

            var view = new Mock<IView>();
            var engine = new Mock<IViewEngine>();
            var viewEngineResult = new ViewEngineResult(view.Object, engine.Object);
            engine.Setup(e => e.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(viewEngineResult);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(engine.Object);

        }

        [TestCleanup()]
        public void Cleanup()
        {
            HttpContext.Current = null;
        }


        [TestMethod()]
        public void RenderPartialTest()
        {
            HomeController controller = new HomeController();

            controller.ControllerContext = new ControllerContext(new System.Web.Routing.RequestContext(), controller);

            string actual = controller.ControllerContext.RenderPartialToString("_RecipeList", null);

            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }
    }
}
