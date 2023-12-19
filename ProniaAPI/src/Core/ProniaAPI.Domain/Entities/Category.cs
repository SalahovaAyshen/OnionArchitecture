using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        //relational prop
        public ICollection<Product>? Products { get; set; }
    }
}
