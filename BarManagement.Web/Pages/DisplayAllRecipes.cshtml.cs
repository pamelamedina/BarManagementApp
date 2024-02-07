using BarManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarManagement.Models;


namespace BarManagement.Web.Pages
{
    public class DisplayAllRecipesModel : PageModel
    {     

        public List<Recipe> recipes { get; set; } = new List<Recipe>();

		
		private IRecipeService recipeService { get; set; }


        public DisplayAllRecipesModel(IRecipeService   recipeService)
        {
            this.recipeService = recipeService;
        }



		public async  Task  OnGetAsync()
        {
            recipes = (await recipeService.GetRecipes()).ToList();

        }
    }
}
