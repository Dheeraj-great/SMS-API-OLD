using Microsoft.Extensions.DependencyInjection;
using SMS.TenantApi.BusinessModel.Config;
using SMS.TenantApi.CrossCuttingLayer.Logging;
using SMS.TenantApi.CrossCuttingLayer.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.DataRepository.Infrastructure
{
    public class DbRepository<T> : IDbRepository<T> where T : IDisposable
    {
        private readonly AppSettingsModel _appsettings;

        public DbRepository(AppSettingsModel appSettings)
        {
            _appsettings = appSettings;
        }

        public T Repository
        {
            get
            {
                var serviceProvider = new ServiceCollection()
              .AddSingleton(typeof(T))
              .AddSingleton(typeof(IServiceLogger), typeof(ServiceLogger))
              .AddSingleton(typeof(IDataBaseConnection), typeof(DataBaseConnection))
              //.AddAutoMapper()
              .AddSingleton(_appsettings)
              .BuildServiceProvider();
                return serviceProvider.GetService<T>();
            }
        }


        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
