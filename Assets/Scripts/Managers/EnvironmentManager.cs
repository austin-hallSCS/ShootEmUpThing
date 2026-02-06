using UnityEngine;
using UnityEngine.Tilemaps;

namespace WizardGame.Managers
{
    public class EnvironmentManager : ManagerBase
    {
        private Renderer floor;
        public EnvironmentManager(GameManager manager, GameObject floor) : base(manager)
        {
            this.floor = floor.GetComponent<TilemapRenderer>();
        }

        // TODO: Figure out best way to get valid spawn point (on the floor)
        // public Vector3 GetRandomValidSpawnPoint()
        // {
        //     Bounds bounds = floor.bounds;
        //     Vector3 candidatePos;
        //     int attempts = 0;
        //     int maxAttempts = 30;

        //     while (attempts < maxAttempts)
        //     {
        //         float x = Random.Range(bounds.min.x, bounds.max.x);
        //         float y = Random.Range(bounds.min.y, bounds.max.y);
        //         candidatePos = new Vector3(x, y, 0);


        //     }

        // }

        protected override void SubscribeToEvents()
        {

        }

        protected override void UnsubscribeFromEvents()
        {

        }
    }
}
