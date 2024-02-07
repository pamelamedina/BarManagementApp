using Microsoft.EntityFrameworkCore;
using BarManagement.Models;
using Newtonsoft.Json;

namespace BarManagement.Api.Models
{
    public class RecipeBuilder : IRecipeBuilder
    {
        RecipeDbContext _context { get; set; }
        private Recipe _recipe { get; set; }
        private List<Ingredient> _ingredientList { get; set; }
        private List<Instruction> _instructionList { get; set; }


        public async Task BuildRecipe(int recipeId)
        {
            _recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);
            _ingredientList = await _context.Ingredients.Where(x => x.RecipeId == recipeId).ToListAsync();
            _instructionList = await _context.Instructions.Where(x => x.RecipeId == recipeId).OrderBy(x => x.StepNumber).ToListAsync();

            RecipeObject recipeObject = new RecipeObject();
            recipeObject._recipe = this._recipe;
            recipeObject._ingredientList = this._ingredientList;
            recipeObject._instructionList = this._instructionList;
            var serializeRecipe = JsonConvert.SerializeObject(recipeObject);
        }
    }
}



       
 
