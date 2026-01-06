using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class SwordBurstGO : SpellGO
    {
        private Transform centerPoint;
        private float currentAngle;
        private float radius;
        private float rotationSpeed;
        private float damage;

        public void Initialize(SpellStats stats, Transform player, float startAngle)
        {
            base.Initialize(stats);

            centerPoint = player;
            currentAngle = startAngle;
        }

        private void Update()
        {
            if (centerPoint == null) return;

            radius = spellStats.GetStat(StatType.Area).CurrentValue;
            rotationSpeed = spellStats.GetStat(StatType.Speed).CurrentValue;
            damage = spellStats.GetStat(StatType.Damage).CurrentValue;

            Orbit();
            RotateSelf();
        }

        private void Orbit()
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            currentAngle %= 360;

            float rad = currentAngle * Mathf.Deg2Rad;

            // Calculate offset from player
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;

            Vector3 finalPos = centerPoint.position + new Vector3(x, 0, z);
            transform.position = finalPos;
        }

        private void RotateSelf()
        {
            transform.rotation = Quaternion.Euler(0, -currentAngle, 0);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.Damage(damage);
            }

        }
    }
}
