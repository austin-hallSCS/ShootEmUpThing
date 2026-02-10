using UnityEngine;

namespace WizardGame.Managers
{
    public class EnvironmentManager : ManagerBase
    {
        private Collider2D floorCollider;
        public EnvironmentManager(GameManager manager, GameObject floor) : base(manager)
        {
            floorCollider = floor.GetComponent<CompositeCollider2D>();
        }

        public Vector3 GetRandomValidSpawnPoint()
        {
            Bounds bounds = floorCollider.bounds;
            Vector3 candidatePos;
            int attempts = 0;
            int maxAttempts = 30;

            while (attempts < maxAttempts)
            {
                // Pick a random point
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float y = Random.Range(bounds.min.y, bounds.max.y);
                candidatePos = new Vector3(x, y, 0);

                // Check if point is actually inside the floor shape
                if (floorCollider.OverlapPoint(candidatePos))
                {
                    // TODO: add logic to check for decorations/obstacles
                    return candidatePos;
                }
                attempts++;
            }

            Debug.LogWarning("Could not find valid spawn point after 30 tries. Returning cener.");
            return bounds.center;
        }

        public bool CheckForValidSpawnPoint(Vector2 point)
        {
            return floorCollider.OverlapPoint(point);
        }

        protected override void SubscribeToEvents()
        {

        }

        protected override void UnsubscribeFromEvents()
        {

        }
    }
}
