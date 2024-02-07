using BarManagement.Models;

namespace BarManagement.Api.Models
{
    public class RecipeObject
    {

        public Recipe _recipe { get; set; }
        public  List<Ingredient> _ingredientList { get; set; }
        public  List<Instruction> _instructionList { get; set; }

        public RecipeObject()
        {
          Recipe _recipe = new Recipe();
          List<Ingredient> _ingredientList = new List<Ingredient>();
          List<Instruction> _instructionList = new List<Instruction>(); 
          _ingredientList.Add(new Ingredient());
          _instructionList.Add(new Instruction());

        }

    }
     
  
}
