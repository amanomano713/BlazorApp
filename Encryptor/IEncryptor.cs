namespace BlazorApp.Encryptor
{
    public interface IEncryptor
    {
        string EnCryption(string? input);

        string Decryption(string? input);

    }
}
