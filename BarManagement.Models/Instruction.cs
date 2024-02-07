using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarManagement.Models
{
    public class Instruction
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [Required]
        public string Description { get; set; }

        public Instruction()
        {
            RecipeId = 0;
            StepNumber = 0;
            Description = "default";
        }
    }
}
