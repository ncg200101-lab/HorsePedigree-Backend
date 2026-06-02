namespace HorsePedigree_2026.Services;

public interface ITokenBlacklistService
{
    void Revoke(string jti, DateTime expiresAtUtc);

    bool IsRevoked(string jti);
}
