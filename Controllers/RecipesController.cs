using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeFinder.Models;

namespace RecipeFinder.Controllers
{
    public class RecipesController : Controller
    {

        private readonly IConfiguration _config;

        public RecipesController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Index(FormModel formModel, [FromForm] string[] intolerances)
        {
            List<Recipe> recipes = new List<Recipe>();
            formModel.Intolerances = intolerances;
            using (var client = new HttpClient())
            {
                string uriString = "https://api.spoonacular.com/recipes/complexSearch?apiKey=" + _config["API_KEY"] + "&instructionsRequired=true&number=10";

                if (formModel.Intolerances.Length > 0)
                {
                    uriString += "&intolerances=";
                    for (int i = 0; i < formModel.Intolerances.Length; i++)
                    {
                        if (i == 0) uriString += formModel.Intolerances[i];
                        else uriString += "," + formModel.Intolerances[i];
                    }
                }

                if (formModel.Cuisine != "All cuisines")
                {
                    uriString += "&cuisine=" + formModel.Cuisine;
                }

                if (formModel.MealType != "All types")
                {
                    uriString += "&type=" + formModel.MealType;
                }

                if (formModel.MaxCookingTime != null)
                {
                    uriString += "&maxReadyTime=" + formModel.MaxCookingTime;
                }

                if (formModel.Keyword != null)
                {
                    uriString += "&query=" + formModel.Keyword;
                }

                var uri = new Uri(uriString);

                var response = client.GetAsync(uri).Result;

                dynamic dynamicJson = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                for (int i = 0; i < dynamicJson.results.Count; i++)
                {
                    Recipe recipe = new Recipe();

                    recipe.Title = dynamicJson.results[i].title;

                    recipe.Id = dynamicJson.results[i].id;

                    recipe.ImageSource = dynamicJson.results[i].image;

                    recipes.Add(recipe);
                }
            }

            return View(recipes);
        }
    }
}
