﻿using System.Collections.Generic;

namespace SomeProducts.PresentationServices.Models.Create
{
    public static class ProductColors
    {
        static ProductColors()
        {
            Colors = new Dictionary<string, string>
            {
                {"#7bd148", "Green"},
                {"#5484ed", "Bold blue"},
                {"#a4bdfc", "Turquoise"},
                {"#7ae7bf", "Light green"},
                {"#51b749", "Bold green"},
                {"#fbd75b", "Yellow"},
                {"#ffb878", "Orange"},
                {"#ff887c", "Red"},
                {"#dc2127", "Bold Red"},
                {"#dbadff", "Purple"},
                {"#e1e1e1", "Gray"}
            };

        }

        public static Dictionary<string, string> Colors { get; }
    }
} 