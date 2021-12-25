using Microsoft.Extensions.DependencyInjection;
using SMS.TenantApi.BusinessModel.Config;
using SMS.TenantApi.BusinessService.Interfaces;
using SMS.TenantApi.CrossCuttingLayer.Logging;
using SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces;
using SMS.TenantApi.DataRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.BusinessService.Services
{
    public class Services<T> : IServices<T>
    {
        private readonly AppSettingsModel _appsettings;
        public Services(AppSettingsModel appsettings)
        {
            _appsettings = appsettings;
        }

        public T Service
        {
            get
            {
                var serviceCollections = new ServiceCollection()
                .AddSingleton(typeof(IDbRepository<>), typeof(DbRepository<>))
                .AddSingleton(typeof(IServiceLogger), typeof(ServiceLogger))
                //.AddSingleton(typeof(MicrosoftCacheProvider), typeof(MicrosoftCacheProvider))
                //.AddSingleton(typeof(RedisCacheProvider), typeof(RedisCacheProvider))
                //.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache))
                //.AddSingleton(typeof(ICacheService), typeof(CacheService))
                //.AddMemoryCache()
                .AddSingleton(_appsettings)
                .AddSingleton(typeof(T))
                //.AddAutoMapper()
                .BuildServiceProvider();
                return (T)Convert.ChangeType(serviceCollections.GetService<T>(), typeof(T));
                // default(T);
            }
        }
    }
}
