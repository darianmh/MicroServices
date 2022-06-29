namespace Infrastructure.Services.Redis;

public interface IRedisService
{
    void SetValue<T>(T value, string key);
    T GetValue<T>(string key) where T : new();
}