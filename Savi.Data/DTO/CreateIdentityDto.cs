using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Savi.Data.DTO
{
    public class CreateIdentityDto
    {
        
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public IFormFile DocumentImage { get; set; }
        
        public string DocumentImageUrl { get; set; }
    }
}
