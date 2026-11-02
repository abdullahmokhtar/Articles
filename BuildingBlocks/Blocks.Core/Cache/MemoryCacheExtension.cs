using Microsoft.Extensions.Caching.Memory;

namespace Blocks.Core.Cache;

public static class MemoryCacheExtension
{
    public static T GetOrCreateByType<T>(this IMemoryCache memoryCache, Func<ICacheEntry, T> factory)
        => memoryCache.GetOrCreate(typeof(T).FullName!, factory)!;

}
