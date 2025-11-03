using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utils;

namespace WizardGame.SpellSystem
{
    public class FireballSpellController : ProjectileSpellController
    {
        [SerializeField] private Transform spawnPoint;

        private ShuffleBag<Vector2> directionPicker = new ShuffleBag<Vector2>(WorldSenses.cardinalDirections);

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

            // Quaternion rotation = WorldSenses.VectorDirectionToRotation(direction);

            // spellGameObject = Instantiate(spellData.SpellPrefab, (Vector2)transform.position + Vector2.up * 0.5f, rotation, transform);
            // spellGameObject.GetComponent<SpellGO>().Launch(direction, force);
        }

        protected override void SpellDeactivate()
        {
            base.SpellDeactivate();
            directionPicker.Refill();
        }

        protected override void FireProjectile()
        {
            base.FireProjectile();
            
            HandleDirection();
            Instantiate(spellPrefab, spawnPoint.position, transform.rotation, transform);
        }

        private void HandleDirection()
        {
            Vector2 nextDirection = directionPicker.GetNext();
            transform.right = nextDirection;
        }
    }

}