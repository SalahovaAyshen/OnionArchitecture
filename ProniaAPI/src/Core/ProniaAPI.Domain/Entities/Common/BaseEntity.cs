using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool isDeleted { get; set; }
        public BaseEntity()
        {
            CreatedBy = "ayshen.salahova";
            CreatedAt = DateTime.Now;
        }
    }
}
