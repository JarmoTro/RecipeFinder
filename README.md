# Recipe Finder

## By Jarmo Troska

[What is a Recipe Finder?](#what-is-a-recipe-finder)

[Technologies used](#technologies-used)

[Configuring the application to run in your environment](#configuring-the-application-to-run-in-your-environment)

## What is a Recipe Finder?

Ever find yourself in a situation where you don't know what to cook for dinner?

Recipe Finder can help you find mouth-watering recipes with the help of the Spoonacular Recipe API!

## Technologies used

ASP.NET Core MVC https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-6.0&tabs=visual-studio

Bootstrap 5 https://getbootstrap.com/docs/5.1/getting-started/introduction/

Font Awesome https://fontawesome.com/

Spoonacular API https://spoonacular.com/food-api/

## Configuring the application to run in your environment

### Prerequisites

- .NET 6.0
- Visual Studio 2022 with the ASP.NET and web development workload installed
- A Spoonacular API key

### Getting started

1. Open the RecipeFinder.csproj file with Visual Studio 2022 to open the project in the IDE.

2. Open the User Secrets menu by right clicking on your .csproj file and selecting "Manage User Secrets". This will open your secrets.json file. To learn more about how User Secrets work, click [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows).

![Untitled](https://user-images.githubusercontent.com/82982361/195657913-1379988d-d2b2-4a34-8912-b6a8f179448b.png)

3. In the secrets.json file, add the "API_KEY" variable and assign your Spoonacular API key to it.

```json
{
  "API_KEY": "your_api_key_here"
}
```

4. Now everything should be good to go. Press F5 or select Debug->Start Debugging to assure that the application works correctly.

![Untitled](https://user-images.githubusercontent.com/82982361/195683093-bb9917e7-24d2-49e7-a24b-641e18f4a920.png)



