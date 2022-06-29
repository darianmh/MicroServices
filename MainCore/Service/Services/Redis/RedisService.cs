using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.Helper;
using Infrastructure.Services.Redis;
using Newtonsoft.Json;
using StackExchange.Redis;
using EasyCaching.Core;

namespace Service.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _redis = RedisHelper.RedisCache;
        public void SetValue<T>(T value, string key)
        {
            var values = GetHashEntries(value);
            _redis.HashSet(new RedisKey($"{RedisConfiguration.PresetValue}_{key}"), values.ToArray());
        }

        private List<HashEntry> GetHashEntries<T>(T value)
        {
            var properties = typeof(T).GetProperties();
            var result = new List<HashEntry>();
            foreach (var propertyInfo in properties)
            {
                var val = propertyInfo.GetValue(value);
                var item = new HashEntry(propertyInfo.Name, val.ToString());
                result.Add(item);
            }

            return result;
        }

        public T GetValue<T>(string key) where T : new()
        {
            var fields = typeof(T).GetProperties();
            var val = _redis.HashGet(new RedisKey($"{RedisConfiguration.PresetValue}_{key}"), fields.Select(x => new RedisValue(x.Name)).ToArray());
            if (val?.Any() != true) return new T();
            return GetModel<T>(val, fields);
        }

        private T GetModel<T>(RedisValue[] val, PropertyInfo[] fieldInfos) where T : new()
        {
            var model = new T();
            for (var i = 0; i < fieldInfos.Length; i++)
            {
                var fieldInfo = fieldInfos[i];
                fieldInfo.SetValue(model, Convert.ChangeType(val[i], fieldInfo.PropertyType));
            }

            return model;
        }
    }
}
