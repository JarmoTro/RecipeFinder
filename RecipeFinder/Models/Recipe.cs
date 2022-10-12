namespace RecipeFinder.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ImageSource { get; set; }

        public string? Summary { get; set; }

        public string? Instructions { get; set; }

        public List<Ingredient>? Ingredients { get; set; }

        public int ReadyInMinutes { get; set; }

        public string? SourceName { get; set; }
    }
}
