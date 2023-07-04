using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Savi.Data.Domains
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<UserTransaction> UserTransactions { get; set; }
        public ICollection<GroupTransaction> GroupTransactions { get; set; }
        public ICollection<Saving> Savings { get; set; }
        public ICollection<CardDetail> CardDetails { get; set; }
        public OTP OTP { get; set; }
    }
}
