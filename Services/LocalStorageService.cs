using BlazorApp.Encryptor;
using Microsoft.JSInterop;

namespace BlazorApp.Services
{

    public interface ILocalStorageService
    {
        Task<string> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
        Task SetItemToken<T>(string key, T value);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;
        private readonly IEncryptor _IEncryptor;

        public LocalStorageService(IJSRuntime jsRuntime, IEncryptor IEncryptor)
        {
            _jsRuntime = jsRuntime;
            _IEncryptor = IEncryptor;
        }

        public async Task<string> GetItem<T>(string key)
        {

            string value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (value == null)
                return default;

            var result = _IEncryptor.Decryption(value);

            return result;
        }

        public async Task SetItem<T>(string key, T value)
        {
            string? Keyvalue = value.ToString();

            Keyvalue = _IEncryptor.EnCryption(Keyvalue);

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, Keyvalue);
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItemToken<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
    
    }
}

