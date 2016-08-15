using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SomeProducts.Models.ProductModels
{
    public class ProductColors
    {
        public ProductColors()
        {
            Colors = new Dictionary<string, string>();
            Colors.Add("#7bd148", "Green");
            Colors.Add("#5484ed", "Bold blue");
            Colors.Add("#a4bdfc", "Turquoise");
            Colors.Add("#7ae7bf", "Light green");
            Colors.Add("#51b749", "Bold green");
            Colors.Add("#fbd75b", "Yellow");
            Colors.Add("#ffb878", "Orange");
            Colors.Add("#ff887c", "Red");
            Colors.Add("#dc2127", "Bold Red");
            Colors.Add("#dbadff", "Purple");
            Colors.Add("#e1e1e1", "Gray");          
        }
        public Dictionary<string, string> Colors {get;set;}
    }
}