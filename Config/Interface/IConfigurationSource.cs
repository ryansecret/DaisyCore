using System.Threading;
using System.Threading.Tasks;

namespace Daisy.Core.Config.Interface
{
    public interface IConfigurationSource
    {
        Task<ConfigResult<T>> GetSettingAsync<T>(string key, CancellationToken cancellationToken = default(CancellationToken));
    }
}