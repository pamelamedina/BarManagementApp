using BarManagement.Api.Models;
using BarManagement.Models;
using BarManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using System;

namespace BarManagement.Web.Pages
{
    public class EditRecipeModel : PageModel
    {

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public int Id { get; set; }


        [BindProperty]
        public List<Ingredient> Ingredients { get; set; }

        [BindProperty]
        public List<Instruction> Instructions { get; set; }

        [BindProperty]
        public RecipeObject RecipeObject { get; set; }


        private IRecipeService  recipeService { get; set; }


        public EditRecipeModel(IRecipeService  recipeService)
        {
            this.recipeService = recipeService;
            Recipe = new Recipe();
            Ingredients = new List<Ingredient>();
            Instructions = new List<Instruction>();
            Ingredients.Add(new Ingredient());
            Instructions.Add(new Instruction());
            RecipeObject = new RecipeObject();
        }


        public async Task OnGet(int Id)
        {
            this.Id = Id;
            RecipeObject = await recipeService.GetRecipeDetail(Id);

            Recipe = RecipeObject._recipe;
            Ingredients = RecipeObject._ingredientList;
            Instructions = RecipeObject._instructionList;
        }

        public async Task<IActionResult> OnPostUpdate()
        {

            // Add Recipe to the database			 

            RecipeObject recipeObject = new RecipeObject();

            recipeObject._recipe = Recipe;
            recipeObject._ingredientList = Ingredients;
            recipeObject._instructionList = Instructions;

            var result = await recipeService.UpdateRecipe(recipeObject);
            Id = result._recipe.RecipeId;
            //return RedirectToPage($"/RecipeDetail/{Id}" );
            return RedirectToPage("/RecipeDetail", new { Id = Id });

        }
    }
}
