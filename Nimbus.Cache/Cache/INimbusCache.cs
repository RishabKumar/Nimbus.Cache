using Nimbus.Cache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimbus.Cache
{
    /// <summary>
    /// Interface for implementing LFU based cache
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public interface INimbusCache<K, V> : IDisposable
    {
        /// <summary>
        /// Gets number of entries in Active NimbusCache
        /// </summary>
        int ActiveCount { get; }
        /// <summary>
        /// Gets total number of entries in NimbusCache
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Gets a previously calculated total number of entries in NimbusCache. Note: Should be considered if realtime values are not required.
        /// </summary>
        int PreviousCount { get; }
        /// <summary>
        /// Removes all entries from NimbusCache, including entries in dormant cache and raises EmptyCacheEvent.
        /// </summary>
        void Clear();
        /// <summary>
        /// Inputs entry in Active NimbusCache if its size is not exceeded else adds the entry in Dormant NimbusCache.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">Value</param>
        void Add(K key, V data);
        /// <summary>
        /// Searches the key in both Active and Dormant NimbusCache and if found then updates the value.
        /// </summary>
        /// <param name="key">Existing key</param>
        /// <param name="data">New value</param>
        /// <returns>True if value was updated else false</returns>
        bool Update(K key, V data);
        /// <summary>
        /// Checks if key is present in the Active NimbusCache.
        /// </summary>
        /// <param name="key">The key to find</param>
        /// <returns>True if key is found in Active NimbusCache, else false</returns>
        bool ActiveLookUp(K key);
        /// <summary>
        /// Checks if key is present in NimbusCache(Active+Dormant)
        /// </summary>
        /// <param name="key">The key to find</param>
        /// <returns>True if key is found, else false</returns>
        bool LookUp(K key);
        /// <summary>
        /// Removes the entry from Active NimbusCache corresponsing to the key.
        /// </summary>
        /// <param name="key">The key to corresponding value to remove</param>
        /// <returns>If removed then returns removed value as CacheData, else returns empty CacheData</returns>
        CacheData<V> ActiveRemove(K key);
        /// <summary>
        /// Removes the entry from NimbusCache(Active+Dormant) corresponsing to the key.
        /// </summary>
        /// <param name="key">The key to corresponding value to remove</param>
        /// <returns>If removed then returns removed value as CacheData, else returns empty CacheData</returns>
        CacheData<V> Remove(K key);
        /// <summary>
        /// Raised when NimbusCache is empty.Note: It is also periodically raised by Cleaner when NimbusCache is empty.
        /// </summary>
        event EmptyCacheHandler EmptyCacheEvent;
    }
    /// <summary>
    /// Handler to handle EmptyCacheEvent.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void EmptyCacheHandler(object sender, EventArgs args);
}
