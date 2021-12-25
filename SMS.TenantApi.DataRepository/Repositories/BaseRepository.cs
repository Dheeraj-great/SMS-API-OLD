using SMS.TenantApi.BusinessModel.Config;
using SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces;
using SMS.TenantApi.CrossCuttingLayer.Logging.Model;
using SMS.TenantApi.DataRepository.Infrastructure;
using SMS.TenantApi.DataRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMS.TenantApi.DataRepository.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        private readonly IDataBaseConnection _db;
        private readonly IServiceLogger _logger;
        public CurrentUser CurrentUser { get; set; }
        protected BaseRepository(IDataBaseConnection db, IServiceLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        //public Task<TEntity> Insert(TEntity item, Guid currentUserGuid, Guid tenantGuid)
        //{
        //    _logger.Log(new LogInformation
        //    {
        //        Module = typeof(TEntity).Name,
        //        UserGuid = currentUserGuid,
        //        Message = ""//CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
        //    });

        //    if (item == null) throw new ArgumentNullException(typeof(TEntity).Name);

        //    using (var dbContext = _db.GetSMSDBContext(tenantGuid))
        //    {
        //        dbContext.Add((object)item);
        //        dbContext.SaveChanges();
        //        return Task.Run(() => item);
        //    }
        //}

        //public Task<int> Update(TEntity item, Guid currentUserGuid, Guid tenantGuid)
        //{
        //    _logger.Log(new LogInformation
        //    {
        //        Module = typeof(TEntity).Name,
        //        UserGuid = currentUserGuid,
        //        Message = ""//CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
        //    });

        //    if (item == null)
        //        throw new ArgumentNullException(typeof(TEntity).Name);

        //    using (var dbContext = _db.GetSMSDBContext(tenantGuid))
        //    {
        //        dbContext.Update((object)item);
        //        return dbContext.SaveChangesAsync();
        //    }
        //}
    }
}
