using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Daisy.Core.Config.Interface;

namespace Daisy.Core.Config
{
    public class AppSettingSourcem:IConfigurationSource
    {
        public Task<ConfigResult<T>> GetSettingAsync<T>(string key, CancellationToken cancellationToken = new CancellationToken())
        {
            var configValue = ConfigurationManager.AppSettings[key];
            if (configValue == null)
            {
                return Task.FromResult(new ConfigResult<T>().NoKey());
            }
            else
            {
                var valueType = typeof(T);
                valueType = Nullable.GetUnderlyingType(valueType) ?? valueType;

                var value = (T)Convert.ChangeType(configValue, valueType);
                return Task.FromResult(new ConfigResult<T>().HaveKey(value));
            }
        }
    }
}