using bb_demo_github.Components.Pages;
using BootstrapBlazor.Components;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace bb_demo_github.Data
{
    public class UserService(ICacheManager cacheManager) : IDisposable
    {
        private readonly ICacheManager _cacheManager = cacheManager;

        private CancellationTokenSource DefaultCancellationTokenSource { get; set; } = new(TimeSpan.FromMinutes(2));

        /// <summary>
        /// 清除缓存
        /// </summary>
        public void ClearCache()
        {
            ClearUserCache();
        }

        /// <summary>
        /// 清除User缓存
        /// </summary>
        public void ClearUserCache()
        {
            DefaultCancellationTokenSource.Cancel();
        }

        public async Task<List<Resource>> GetResourcesByUserIdAsync(int userId) => await _cacheManager.GetOrCreateAsync($"{nameof(UserService)}-{nameof(GetResourcesByUserIdAsync)}-{userId}", async entry =>
        {
            entry.ExpirationTokens.Add(new CancellationChangeToken(DefaultCancellationTokenSource.Token));
            var result = await Task.Run(() => new List<Resource> {
                new() { Value="/"},
                new() { Value="/counter"},
                new() { Value="/table"},
                new() { Value="/users"}
            });
            return result;
        });

        public List<Resource> GetResourcesByUserId(int userId) => _cacheManager.GetOrCreate($"{nameof(UserService)}-{nameof(GetResourcesByUserIdAsync)}-{userId}", entry =>
        {
            entry.ExpirationTokens.Add(new CancellationChangeToken(DefaultCancellationTokenSource.Token));
            return new List<Resource> {
                new() { Value="/"},
                new() { Value="/counter"},
                new() { Value="/table"},
                new() { Value="/users"}
            };
        });

        #region Dispose
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                DefaultCancellationTokenSource.Cancel();
                DefaultCancellationTokenSource.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Resource
    {
        [NotNull]
        public string? Value { get; set; }
    }
}


