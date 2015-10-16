using System;

namespace RecipeGrabbing
{
	public class DataClass
	{
		//Data is pretty much a container for the recipes
		public System.Collections.ObjectModel.Collection<Recipe> matches { get; set; }
		public string cat = "meow";
		public DataClass ()
		{
		}
	}
}

