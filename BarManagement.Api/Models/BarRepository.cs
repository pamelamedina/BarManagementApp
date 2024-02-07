using BarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace BarManagement.Api.Models
{
    public class BarRepository : IBarRepository
    {
        RecipeDbContext _context { get; set; }       
        private Recipe _recipe { get; set; }
        private List<Ingredient> _ingredients { get; set; }
        private List<Instruction> _instructions { get; set; }     


        public BarRepository(RecipeDbContext context)
        {
            _context = context;
        }


//*****************************************************************************************************
//                          Delete Recipe
//*****************************************************************************************************

        public async Task<int> DeleteRecipe(int recipeId)
        {       

           var ingredientsToDelete =  _context.Ingredients.Where(x => x.RecipeId == recipeId).ToList();
            foreach (Ingredient  i in ingredientsToDelete)
            {
                _context.Ingredients.Remove(i);
                await _context.SaveChangesAsync();
            }

            var instructionsToDelete = _context.Instructions.Where(x => x.RecipeId == recipeId).ToList();
            foreach (Instruction i in instructionsToDelete)
            {
                _context.Instructions.Remove(i);
                await _context.SaveChangesAsync();
            }

            Recipe recipeToRemove  = _context.Recipes.FirstOrDefault(x => x.RecipeId == recipeId);
            _context.Recipes.Remove(recipeToRemove);
            await _context.SaveChangesAsync();
            return recipeId;
        }



//*****************************************************************************************************
//                       Get Recipe By Id
//*****************************************************************************************************

        public async Task<Recipe> GetRecipe(int recipeId)
        {
            return await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        }




//*****************************************************************************************************
//                          Get Recipes
//*****************************************************************************************************

        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }


//*****************************************************************************************************
//                         Get Recipe Detail
//*****************************************************************************************************

        public async Task<string> GetRecipeDetail(int recipeId)
        {
            _recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == recipeId);
            _ingredients = await _context.Ingredients.Where(x => x.RecipeId == recipeId).ToListAsync();
            _instructions = await _context.Instructions.Where(x => x.RecipeId == recipeId).OrderBy(x => x.StepNumber).ToListAsync();

            RecipeObject recipeObject = new RecipeObject();
            recipeObject._recipe = this._recipe;
            recipeObject._ingredientList = this._ingredients;
            recipeObject._instructionList = this._instructions;
            string serializeRecipe = JsonConvert.SerializeObject(recipeObject);
            return serializeRecipe;
        }




//*****************************************************************************************************
//                           Create Recipe
//*****************************************************************************************************

        public async Task<string> CreateRecipe(RecipeObject r)
        {
            _recipe = r._recipe;
            _ingredients = r._ingredientList;
            _instructions = r._instructionList;

             await _context.Recipes.AddAsync(_recipe);
             await _context.SaveChangesAsync();


            // Add Ingredient to the database

            var index = 0;
            foreach (var i in _ingredients)
            {
                i.RecipeId = _recipe.RecipeId;
                await _context.Ingredients.AddAsync(i);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                index++;
            }


            // Add Instructions to the database

            int indexInstruction = 1;
            foreach (var i in _instructions)
            {
                i.StepNumber = indexInstruction;
                i.RecipeId = _recipe.RecipeId;
                await _context.Instructions.AddAsync(i);

                try
                {
                   await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                indexInstruction++;
            }           

            string serializeRecipe = JsonConvert.SerializeObject(r);
            return serializeRecipe;
        }


//*****************************************************************************************************
//                           UPDATE RECIPE
//*****************************************************************************************************

        public async Task<RecipeObject> UpdateRecipe(RecipeObject updateRecipeOb)
        {
            Recipe upRecipe = updateRecipeOb._recipe;
            List<Ingredient> upIngredients = updateRecipeOb._ingredientList;
            List<Instruction> upInstructions = updateRecipeOb._instructionList;

            var resultRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.RecipeId == upRecipe.RecipeId);
            if (resultRecipe != null)
            {
                resultRecipe.Title = upRecipe.Title;
                resultRecipe.Description = upRecipe.Description;
                await _context.SaveChangesAsync();
            }



            //*********************************
            //   Ingredients
            //*********************************

            // Delete Ingredients and Add Ingredients with same RecipeId
            var ingredientsToDelete = _context.Ingredients.Where(x => x.RecipeId == upRecipe.RecipeId).ToList();

            foreach (Ingredient i in ingredientsToDelete)
            {
                _context.Ingredients.Remove(i);
                await _context.SaveChangesAsync();
            }
            // Add Ingredient to the database
            var index = 0;
            foreach (var i in upIngredients)
            {
                i.RecipeId = _recipe.RecipeId;
                await _context.Ingredients.AddAsync(i);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                index++;
            }


            //*********************************
            //   Instructions
            //*********************************
            // Delete Instructions and then Add the new Instructions

            var instructionsToDelete = _context.Instructions.Where(x => x.RecipeId == upRecipe.RecipeId).ToList();
            foreach (Instruction i in instructionsToDelete)
            {
                _context.Instructions.Remove(i);
                await _context.SaveChangesAsync();
            }

            int indexInstruction = 1;
            foreach (var i in upInstructions)
            {
                i.StepNumber = indexInstruction;
                i.RecipeId = _recipe.RecipeId;
                await _context.Instructions.AddAsync(i);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                indexInstruction++;
            }
            //string serializeRecipe = JsonConvert.SerializeObject(updateRecipeOb);
            return updateRecipeOb;

        }     
    }
   
}
