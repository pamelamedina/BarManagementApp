
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarManagement.Api.Models
{
    public interface IRecipeBuilder
    {
        public Task BuildRecipe(int recipeId);
        //public void DisplayRecipe();
    }
}