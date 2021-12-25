using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMS.TenantApi.DataRepository.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        //Task<TEntity> Insert(TEntity item, Guid currentUserGuid, Guid tenantGuid);
        //Task<int> Update(TEntity item, Guid currentUserGuid, Guid tenantGuid);
    }
}
