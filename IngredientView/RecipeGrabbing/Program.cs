using System;
using Newtonsoft;
using RestSharp;

namespace RecipeGrabbing
{
	class MainClass
	{
		public static Recipe[] getFeatured() {
			return getRecipesFromURL ("http://api.kitchen.support/recipes/featured");
		}

		public static Recipe[] getSearch(String query) {
			return getRecipesFromURL ("http://api.kitchen.support/recipes/search/" + query);
		}

		public static Recipe[] getRecipesFromURL(String URL) {
			var client = new RestClient (URL);
			var request = new RestRequest (Method.GET);
			IRestResponse response = client.Execute (request);
			var content = response.Content;
			ResponseClass first = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseClass>(content);
			Recipe[] recipes = new Recipe[first.data.matches.Count];
			first.data.matches.CopyTo(recipes, 0);
			return recipes;
		}

		public static void Main (string[] args)
		{
			Recipe[] recipes = getSearch("Fudge");

			for (int i = 0; i < recipes.Length; i++) {
				Console.WriteLine (recipes[i].recipeName);
				Console.WriteLine ("   " + recipes [i].rating);
				Console.WriteLine ("   " + recipes [i].totalTimeInSeconds);
				Console.WriteLine ("   Ingredients");
				for (int j = 0; j < recipes [i].ingredients.Count; j++) {
					Console.WriteLine ("         " + recipes [i].ingredients [j]);
				}
			}
		}
	}
}
