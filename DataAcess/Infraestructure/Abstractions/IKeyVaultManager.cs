namespace BlazorApp.DataAcess.Infraestructure.Abstractions
{
    public interface IKeyVaultManager
    {
        public Task<string> GetSecret(string secretName);
    }
}
