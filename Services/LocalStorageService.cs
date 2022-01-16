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

        public async Task<String> GetItem<T>(string key)
        {
            string value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (value == null)
                return default;

            return value;
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}

