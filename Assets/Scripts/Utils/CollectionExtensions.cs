using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class CollectionExtensions
    {
        public static T GetRandomItem<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("Attempted to get a random item from an empty list.");
                return default(T);
            }

            return list[Random.Range(0, list.Count)];
        }
    }
}
