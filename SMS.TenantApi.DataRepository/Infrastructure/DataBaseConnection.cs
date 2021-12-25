using SMS.TenantApi.BusinessModel.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.DataRepository.Infrastructure
{
    public sealed class DataBaseConnection : IDataBaseConnection
    {
        private readonly AppSettingsModel _appsettings;

        public DataBaseConnection(AppSettingsModel appsettings)
        {
            _appsettings = appsettings;          
        }
    }
}
