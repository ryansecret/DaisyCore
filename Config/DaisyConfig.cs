using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Daisy.Core.Config.Interface;

namespace Daisy.Core.Config
{
    public class DaisyConfig:IConfiguration
    {

        public static readonly DaisyConfig Instance=new DaisyConfig();
         
        private DaisyConfig()
        {
            
        }
        static DaisyConfig()
        {
             _configurationSources.Add(new AppSettingSourcem());
        }
        private static readonly List<IConfigurationSource>  _configurationSources=new List<IConfigurationSource>();
        public async Task<TValue> GetSettingAsync<TValue>(string key, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var configurationSource in _configurationSources)
            {
                var configResult=await configurationSource.GetSettingAsync<TValue>(key, cancellationToken).ConfigureAwait(false);
                if (configResult.KeyExist)
                {
                    return configResult.Value;
                }
               
            }
            var valueType = typeof(TValue);

            if (valueType.IsClass || valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return default(TValue);
            }
            throw new Exception("can not find the key value");
        }

        public T GetSetting<T>(string key)
        {
            return GetSettingAsync<T>(key).Result;
        }

        public void AddConfigSource(IConfigurationSource source)
        {
            _configurationSources.Add(source);
        }
    }
}