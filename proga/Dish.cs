using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proga
{
    public class Dish
    {
        public int id { get; set; }
        public string name { get; set; }
        public string src { get; set; }
        public string description { get; set; }
        public List<Ingridient> dish_Ingrids = new List<Ingridient>();
        public List<Size> sizes_dish = new List<Size>();
    }
}
