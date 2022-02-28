namespace BlazorApp.Cache
{
    public interface ICacheBase
    {
        T Get<T>(string? key);
        void Set<T>(T o, string? key);
        void Remove(string? key);
    }
}
