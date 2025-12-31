using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam_stub_skeleton
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"Product[Id={Id}, Name={Name}, Price={Price: C}]";
        }

        public string ToText()
        {
            return $"{Id},{Name},{Price}";
        }

        public static Product FromText(string text)
        {
            string[] parts = text.Split(',');
            return new Product
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Price = decimal.Parse(parts[2])
            };
        }
    }
}
