namespace Onbox.Adsk.DataManagement
{
    public interface IAdskForgeConfigService
    {
        string GetClientId();
        string GetClientSecret();
        string GetBaseUrl();
        string GetCallbackUrl();
    }
}
