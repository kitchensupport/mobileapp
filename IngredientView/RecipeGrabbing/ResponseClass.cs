using System;
using System.Collections;

namespace RecipeGrabbing
{
	public class ResponseClass
	{
		//For names to be updated, they MUST match the names found in the JSON
		public string status { get; set; }
		public DataClass data { get; set; } //Contains the recipes
		public ResponseClass ()
		{
		}
	}
}

