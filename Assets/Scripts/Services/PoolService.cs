using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Services
{
    public static class PoolService
    {
        private static Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        // Parent object to keep hierarchy clean
        private static Transform poolContainer;

        private static void InitContainer()
        {
            if (poolContainer == null)
            {
                GameObject containerGO = new GameObject("--- POOL Service ---");
                poolContainer = containerGO.transform;
                Object.DontDestroyOnLoad(containerGO);
            }
        }

        public static GameObject Spawn(GameObject prefab, Pose pose)
        {
            InitContainer();

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
                    // TODO: Figure out how to return game object to original parent if needed (SpawnContext struct?)
                    instance.SetActive(true);
                    instance.transform.position = pose.position;
                    instance.transform.rotation = pose.rotation;
                    return instance;
                }
            }

            // If queue is empty (or all null), create a new one
            instance = Object.Instantiate(prefab, pose.position, pose.rotation);

            // Rename the instance so we know it's a clone
            instance.name = prefab.name;

            return instance;
        }

        public static void Despawn(GameObject instance, GameObject originalPrefab)
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
