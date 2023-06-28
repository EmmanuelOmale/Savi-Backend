using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class UserTransaction : BaseEntity
    {

        public string TransactionType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reference { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}