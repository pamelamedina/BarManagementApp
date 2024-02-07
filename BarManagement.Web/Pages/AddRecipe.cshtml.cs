using BarManagement.Api.Models;
using BarManagement.Models;
using BarManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BarManagement.Web.Pages
{

    public class AddRecipeModel : PageModel
    {

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public List<Ingredient> Ingredients { get; set; }

        [BindProperty]
        public List<Instruction> Instructions { get; set; }

        [BindProperty]
        public RecipeObject RecipeObject { get; set; }


        private IRecipeService recipeService { get; set; }


        public AddRecipeModel(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
            Recipe = new Recipe();
            Ingredients = new List<Ingredient>();
            Instructions = new List<Instruction>();
            Ingredients.Add(new Ingredient());
            Instructions.Add(new Instruction());
            RecipeObject = new RecipeObject();
        }


        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync()
        {

            // Add Recipe to the database			 

            RecipeObject recipeObject = new RecipeObject();

            recipeObject._recipe = Recipe;
            recipeObject._ingredientList = Ingredients;
            recipeObject._instructionList = Instructions;

            var result = await recipeService.CreateRecipe(recipeObject);

            return RedirectToPage("/DisplayAllRecipes");

        }
    }
}













