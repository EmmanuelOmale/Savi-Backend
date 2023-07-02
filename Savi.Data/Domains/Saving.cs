using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Domains
{
    public class Saving : BaseEntity
    {

        public string UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal TotalContribution { get; set; }
        public string Purpose { get; set; }
        public string Avatar { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime TargetDate { get; set; }
    }
}