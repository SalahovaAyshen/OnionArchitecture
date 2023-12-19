using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Domain.Entities
{
    public class Color:BaseNameableEntity
    {
        //relational prop
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
