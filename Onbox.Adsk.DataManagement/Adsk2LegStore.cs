using Onbox.Adsk.DataManagement.Core;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public interface IAdsk2LegStore
    {
        Task<AdskAuth> GetExternalAuthorization();
        Task<AdskAuth> GetInternalAuthorization();
        Task SetExternalAuthorization(AdskAuth adskAuth);
        Task SetInternalAuthorization(AdskAuth adskAuth);
    }

    public class Adsk2LegStore : IAdsk2LegStore
    {
        private AdskAuth internalAuth;
        private AdskAuth externalAuth;

        public Task<AdskAuth> GetExternalAuthorization()
        {
            return Task.FromResult(externalAuth);
        }

        public Task<AdskAuth> GetInternalAuthorization()
        {
            return Task.FromResult(internalAuth);
        }

        public Task SetExternalAuthorization(AdskAuth adskAuth)
        {
            this.externalAuth = adskAuth;
            return Task.CompletedTask;
        }

        public Task SetInternalAuthorization(AdskAuth adskAuth)
        {
            this.internalAuth = adskAuth;
            return Task.CompletedTask;
        }
    }


}
