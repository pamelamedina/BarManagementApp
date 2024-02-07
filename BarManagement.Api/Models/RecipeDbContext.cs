using BarManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace BarManagement.Api.Models
{
   
        public class RecipeDbContext : DbContext
        {
            public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
            {
            }

            public DbSet<Recipe> Recipes { get; set; }
            public DbSet<Ingredient> Ingredients { get; set; }
            public DbSet<Instruction> Instructions { get; set; }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("server=(localdb)\\ProjectsV13;database=BarDB;Trusted_Connection=true");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }


    }

  
