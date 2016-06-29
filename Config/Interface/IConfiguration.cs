using System.Threading;
using System.Threading.Tasks;

namespace Daisy.Core.Config.Interface
{
    public interface IConfiguration
    { 
        Task<TValue> GetSettingAsync<TValue>(string key, CancellationToken cancellationToken = default(CancellationToken));

        T GetSetting<T>(string key);

        void AddConfigSource(IConfigurationSource source);  
    }
}