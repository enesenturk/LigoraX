using StackExchange.Redis;
using System.Text.Json;

namespace Base.Caching.Cachers
{
	public class RedisCacher : IExternalCache
	{

		#region CTOR

		private readonly IDatabase _cache;
		private readonly ConnectionMultiplexer _redis;
		private readonly string _productIdentifier;
		private readonly string _envIdentifier;

		public RedisCacher(string connectionString, string productIdentifier, string envIdentifier)
		{
			_redis = ConnectionMultiplexer.Connect(connectionString);

			_cache = _redis.GetDatabase();

			_productIdentifier = productIdentifier;
			_envIdentifier = envIdentifier;
		}

		#endregion

		#region async

		public async Task SetAsync<T>(string key, T data, TimeSpan? absoluteExpireTime = null)
		{
			string identifiedKey = $"{_envIdentifier}-{_productIdentifier}{key}";

			DateTimeOffset absoluteExpiration = absoluteExpireTime != null
				? DateTime.Now.Add(absoluteExpireTime.Value)
				: DateTime.Now.AddSeconds(60);

			string jsonData = JsonSerializer.Serialize(data);
			await _cache.StringSetAsync(identifiedKey, jsonData, absoluteExpireTime);
		}

		public async Task<T> GetAsync<T>(string key)
		{
			string identifiedKey = $"{_envIdentifier}-{_productIdentifier}{key}";

			var value = await _cache.StringGetAsync(identifiedKey);
			T response = JsonSerializer.Deserialize<T>(value);

			return response;
		}

		public async Task RemoveAsync(string key)
		{
			/*
			As part of the product separation process, the CMS project will be divided into multiple new products. 
			Until this separation is complete, both the CMS and nsarc structures will share a common Redis cache. 
			However, since the cache DTOs used in nsarc and CMS have different structures, the two systems cannot utilize the same cache objects.
			
			When entities are updated, fresh data needs to be retrieved from the database, which is achieved by removing the corresponding cache key. 
			Since nsarc and CMS cannot share common keys, a key-specific removal approach is not feasible. 
			Therefore, any remove operation must clear the entire cache to ensure that new queries fetch up-to-date data from the database.
			
			Once the product separation is finalized, a unified caching structure will be implemented, and only nsarc will be used. 
			At that point, the remove function can serve its intended purpose efficiently.
			 */

			//string identifiedKey = $"{_productIdentifier}{key}";

			//_cache.KeyDelete(identifiedKey);

			await ClearAsync();
		}

		public async Task<bool> ContainsAsync(string key)
		{
			string identifiedKey = $"{_envIdentifier}-{_productIdentifier}{key}";

			return await _cache.KeyExistsAsync(identifiedKey);
		}

		public async Task ClearAsync()
		{
			foreach (var endpoint in _redis.GetEndPoints())
			{
				await _redis.GetServer(endpoint).FlushDatabaseAsync();
			}
		}

		#endregion

	}
}
