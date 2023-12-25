using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Application.DTOs.Products
{
    public record ProductUpdateDto(string Name, decimal Price, string SKU, string? Description, int CategoryId, ICollection<int>? ColorIds, ICollection<int>? TagIds);

}
