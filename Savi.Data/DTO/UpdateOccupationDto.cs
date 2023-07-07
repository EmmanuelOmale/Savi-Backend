using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.DTO
{
    public class UpdateOccupationDto
    {
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
