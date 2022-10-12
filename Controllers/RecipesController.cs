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

        public IActionResult RecipeDetails(int recipeId)
        {
            Recipe recipe = new Recipe();

            using (var client = new HttpClient())
            {
                string uriString = "https://api.spoonacular.com/recipes/" + recipeId + "/information?apiKey=" + _config["API_KEY"];

                var uri = new Uri(uriString);

                Console.WriteLine(uriString);

                var response = client.GetAsync(uri).Result;

                dynamic dynamicJson = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                recipe.ReadyInMinutes = dynamicJson.readyInMinutes;

                recipe.Instructions = dynamicJson.instructions;

                recipe.ImageSource = dynamicJson.image;

                recipe.Title = dynamicJson.title;

                recipe.Id = dynamicJson.id;

                recipe.SourceName = dynamicJson.sourceName;

                recipe.Summary = dynamicJson.summary;

                List<Ingredient> ingredients = new List<Ingredient>();

                for (int i = 0; i < dynamicJson.extendedIngredients.Count; i++)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.ImageSource = dynamicJson.extendedIngredients[i].image;
                    ingredient.Name = dynamicJson.extendedIngredients[i].name;
                    ingredient.Quantity = $"{dynamicJson.extendedIngredients[i].amount} {dynamicJson.extendedIngredients[i].unit}";
                    ingredients.Add(ingredient);
                }

                recipe.Ingredients = ingredients;

            }

            return View(recipe);
        }
    }
}
