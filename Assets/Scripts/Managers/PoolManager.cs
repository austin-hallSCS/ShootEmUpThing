using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Managers
{
    public class PoolManager : ManagerBase
    {
        private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        // Parent object to keep hierarchy clean
        private Transform poolContainer;

        public PoolManager(GameManager manager) : base(manager)
        {
            GameObject container = new GameObject("--- POOL CONTAINER ---");
            poolContainer = container.transform;

            // May implement this later - keep container alive across scenes
            // Object.DontDestroyOnLoad(container);
        }

        protected override void SubscribeToEvents() { }

        protected override void UnsubscribeFromEvents() { }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null)
            {
                Debug.LogError("PoolManager: Trying to spawn null prefab!");
                return null;
            }

            // Check if the pool exists
            if (!poolDictionary.ContainsKey(prefab))
            {
                poolDictionary.Add(prefab, new Queue<GameObject>());
            }

            // Try to get an inactive object
            GameObject instance = null;

            while (poolDictionary[prefab].Count > 0)
            {
                // Dequeue - removes and returns the first object at the beginning of a Queue
                instance = poolDictionary[prefab].Dequeue();

                if (instance != null)
                {
                    instance.SetActive(true);
                    instance.transform.position = position;
                    instance.transform.rotation = rotation;
                    return instance;
                }
            }

            // If queue is empty (or all null), create a new one
            instance = Object.Instantiate(prefab, position, rotation);

            // Rename the instance so we know it's a clone
            instance.name = prefab.name;

            return instance;
        }

        public void Despawn(GameObject instance, GameObject originalPrefab)
        {
            if (instance == null) return;

            instance.SetActive(false);

            // Move object to the container so it doesn't clutter the scene
            instance.transform.SetParent(poolContainer);

            // Double-check that dictionary entry exists
            if (!poolDictionary.ContainsKey(originalPrefab))
            {
                poolDictionary.Add(originalPrefab, new Queue<GameObject>());
            }

            // Add back to the queue
            poolDictionary[originalPrefab].Enqueue(instance);
        }
    }
}
