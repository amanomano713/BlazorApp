using Azure.Security.KeyVault.Secrets;
using BlazorApp.DataAcess.Infraestructure.Abstractions;

namespace BlazorApp.DataAcess.Infraestructure.KeyVaultManager
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;

        public KeyVaultManager(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecret(string secretName)
        {
            try
            {
                KeyVaultSecret keyValueSecret = await _secretClient.GetSecretAsync(secretName);
                return keyValueSecret.Value;
            }
            catch
            {
                throw;
            }
        }
    }
}

