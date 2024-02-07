using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarManagement.Models
{
    public class Recipe
    {
        [Required]
        public int RecipeId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public Recipe()
        {
            Title = string.Empty;
            Description = string.Empty;
        }
    }
}
