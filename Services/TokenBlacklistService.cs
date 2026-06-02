using Microsoft.Extensions.Caching.Memory;

namespace HorsePedigree_2026.Services;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly IMemoryCache _cache;

    public TokenBlacklistService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Revoke(string jti, DateTime expiresAtUtc)
    {
        var ttl = expiresAtUtc - DateTime.UtcNow;
        if (ttl <= TimeSpan.Zero)
        {
            return;
        }

        _cache.Set(GetCacheKey(jti), true, ttl);
    }

    public bool IsRevoked(string jti)
    {
        return _cache.TryGetValue(GetCacheKey(jti), out _);
    }

    private static string GetCacheKey(string jti) => $"revoked-token:{jti}";
}
