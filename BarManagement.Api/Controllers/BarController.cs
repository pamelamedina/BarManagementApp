using BarManagement.Api.Models;
using BarManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarManagement.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BarController : ControllerBase
    {
        private readonly IBarRepository barRepository;

        public BarController(IBarRepository barRepository)
        {
            this.barRepository = barRepository;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>>  DeleteRecipe(int id)
        {
            try
            {
                var recipeToDelete = await barRepository.GetRecipe(id);
                if (recipeToDelete == null)
                {
                    return NotFound();
                }
                return await barRepository.DeleteRecipe(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      "Error deleting data from the database");
            }

        }



        [HttpGet]
        public async Task<ActionResult> GetRecipes()
        {
            try
            {
                return Ok(await barRepository.GetRecipes());
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            try
            {
                var result = await barRepository.GetRecipe(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        [HttpGet("Detail/{id:int}")]
        public async Task<ActionResult<RecipeObject>> GetRecipeDetail(int id)
        {
            try
            {

                var result = await barRepository.GetRecipeDetail(id);
                if (result == null)
                {
                    return NotFound();
                }
                var deserializedObject = JsonConvert.DeserializeObject<RecipeObject>(result);
                return deserializedObject;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
       


        [HttpPost()]
        public async Task<ActionResult<RecipeObject>> AddRecipe(RecipeObject  recipeObject)
        {
            var result =  await barRepository.CreateRecipe(recipeObject);
            var deserializedObject = JsonConvert.DeserializeObject<RecipeObject>(result);
            return deserializedObject;
        }


        [HttpPut()]
        public async Task<ActionResult<RecipeObject>>    UpdateRecipe(RecipeObject recipeObject)
        {
            try
            {
                int id = recipeObject._recipe.RecipeId;
                var employeeToUpdate = await barRepository.GetRecipeDetail(id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Recipe with Id = {id}  not found");
                }

                return await barRepository.UpdateRecipe(recipeObject);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error updating data from the database");
            }
        }
    }
}
