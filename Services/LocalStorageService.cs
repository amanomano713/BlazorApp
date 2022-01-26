using BlazorApp.Models.Account;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApp.Services
{

    public interface ILocalStorageService
    {
        Task<String> GetItem<T>(string key);
        Task SetItem<T>(string key, T value);
        Task RemoveItem(string key);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetItem<T>(string key)
        {
            Encryptor.encriptador clave = new Encryptor.encriptador();

            string value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (value == null)
                return default;

            var result = clave.DesEncriptacion(value);

            return result;
        }

        public async Task SetItem<T>(string key, T value)
        {
            Encryptor.encriptador clave = new BlazorApp.Encryptor.encriptador();

            string? Keyvalue = value.ToString();

            Keyvalue = clave.Encriptacion(Keyvalue);

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, Keyvalue);
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

