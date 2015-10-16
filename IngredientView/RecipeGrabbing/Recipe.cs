using System;
using System.Collections;

namespace RecipeGrabbing
{
	public class Recipe
	{
		//For names to be updated, they MUST match the names found in the JSON
		public string recipeName { get; set; }
		public System.Collections.ObjectModel.Collection<string> smallImageUrls { get; set; } //For things that are presented in list format
		public int totalTimeInSeconds { get; set; }
		public int rating { get; set; }
		public string id { get; set; }
		public System.Collections.ObjectModel.Collection<string> ingredients { get; set; }

		public Recipe ()
		{
		}
	}
}

