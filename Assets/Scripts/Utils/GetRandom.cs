using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Utils
{
    public static class GetRandom
    {
        public static T FromList<T>(IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("Attempted to get a value from an empty list.");
                return default(T);
            }

            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}
