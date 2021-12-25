using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.BusinessService.Interfaces
{
    public interface IServices<out T>
    {
        T Service { get; }
    }
}
