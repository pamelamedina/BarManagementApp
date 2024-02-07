using BarManagement.Api.Models;
using BarManagement.Models;
using BarManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BarManagement.Web.Pages
{
    public class RecipeDetailModel : PageModel
    {
		//private IBarService barService { get; set; }

		private IRecipeService recipeService { get; set; }

		public RecipeObject recipeObject { get; set; }

		public int Id { get; set; }
		public string RecipeTitle { get; set; }
		public string RecipeDescription { get; set; }

		public List<Ingredient> Ingredients { get; set; }  = new List<Ingredient>();

		public List<Instruction> Instructions { get; set; } = new List<Instruction>();

			
		public RecipeDetailModel(IRecipeService recipeService)
		{
			//	this.barService = barService;	
			this.recipeService = recipeService;		
		}

		public async Task OnGet(int Id)
		{
			this.Id = Id;
			recipeObject = await recipeService.GetRecipeDetail(Id);

			if (recipeObject != null)
			{ 
				RecipeTitle = recipeObject._recipe.Title;
		    	RecipeDescription = recipeObject._recipe.Description;
		    	Ingredients = (recipeObject._ingredientList).ToList();
		    	Instructions = (recipeObject._instructionList).ToList();
		    }
		}


		public async Task<IActionResult>  OnPostDelete(int  Id)
		{
            await recipeService.DeleteRecipe(Id);
            return RedirectToPage("/DisplayAllRecipes");
        }
    }
}
