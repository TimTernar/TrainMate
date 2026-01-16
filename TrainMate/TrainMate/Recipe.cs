using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    public  class Recipe
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }
        public string Ingredients { get; set; } 
        public string Method { get; set; }      

        public int Kcal { get; set; }
        public int Protein { get; set; } // grams
        public int Carbs { get; set; }   // grams
        public int Fat { get; set; }     // grams

    }
}
