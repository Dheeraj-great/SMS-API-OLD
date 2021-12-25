using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.BusinessModel.Config
{
    public class CurrentUser
    {
        public Guid UserGuid { get; set; }
        public Guid MemberGuid { get; set; }
        public Guid TenantGuid { get; set; }
        public string JwtToken { get; set; }
    }
}
