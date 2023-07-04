using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Savi.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class EmailTemplate : BaseEntity
    {
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public EmailPurpose Purpose { get; set; }


    }

}
