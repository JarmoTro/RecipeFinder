namespace RecipeFinder.Models
{
    public class FormModel
    {
        public string? Keyword { get; set; }

        public string? MealType { get; set; }

        public string[]? Intolerances { get; set; }

        public int? MaxCookingTime { get; set; }

        public string? Cuisine { get; set; }
    }
}
