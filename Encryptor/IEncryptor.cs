namespace BlazorApp.Encryptor
{
    public interface IEncryptor
    {
        public string Encriptacion(string? input);

        public string DesEncriptacion(string? input);

    }
}
