using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Data.Enums
{
    public enum EmailPurpose
    {
        [Description("Registration")]
        Registration,

        [Description("PasswordReset")]
        PasswordReset,

        [Description("Newsletter")]
        Newsletter
    }


}
