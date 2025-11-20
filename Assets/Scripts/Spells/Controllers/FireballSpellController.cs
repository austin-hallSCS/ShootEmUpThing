using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.Spells
{
    public class FireballSpellController : ProjectileSpellController
    {
        private ShuffleBag<Vector2> directionPicker = new ShuffleBag<Vector2>(WorldSenses.cardinalDirections);

        // Workspace
        private Vector2 nextDirection;
        private Vector3 spawnPos;
        private Quaternion rotation;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void SpellActiveBehavior()
        {
            base.SpellActiveBehavior();
        }

        protected override void FireProjectile()
        {
            // base.FireProjectile();

            // Get a random cardinal direction
            nextDirection = directionPicker.GetNext();

            // Move spawn point position
            spawnPos = transform.position + (Vector3)(nextDirection.normalized * spawnRadius);

            // Compute rotation
            rotation = Quaternion.FromToRotation(Vector3.right, nextDirection);

            // Visualization
            // Debug.DrawRay(transform.position, nextDirection.normalized * spawnRadius, Color.red, 2f);

            // Instantiate projectile
            var projectile = Instantiate(spellPrefab, spawnPos, rotation);
            var spellGO = projectile.GetComponent<SpellGO>();
            spellGO.Initialize(spellStats);
        }
    }

}