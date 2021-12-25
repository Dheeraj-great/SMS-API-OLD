using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.DataRepository.Infrastructure
{
    public interface IDbRepository<out T> : IDisposable
    {
        T Repository { get; }
    }
}
