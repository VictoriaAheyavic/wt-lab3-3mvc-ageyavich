using System;
using System.Collections.Generic;
using System.Text;

namespace wt_lab3_3mvc_ageyavich.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }              // остаток на складе
        public bool IsAvailable { get; set; }       // доступен к продаже
        public string Description { get; set; } = "";
    }
}
