using System;
using System.Collections.Generic;
using System.Text;

namespace SMS.TenantApi.BusinessModel.Config
{
    public class AppSettingsModel
    {
        public ConnectionInfoModel ConnectionInfo { get; set; }
        public SettingModel Settings { get; set; }
    }
}
