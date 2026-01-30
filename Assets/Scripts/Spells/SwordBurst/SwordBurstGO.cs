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

        public void Initialize(SpellDataSO data, SpellStats stats, Transform player, float startAngle)
        {
            base.Initialize(data, stats);

            centerPoint = player;
            currentAngle = startAngle;
        }

        private void Update()
        {
            if (centerPoint == null) return;

            radius = spellStats.GetStat(StatType.Area).CurrentValue;

            Orbit();
            RotateSelf();
        }

        private void Orbit()
        {
            // Multiply speed stat value by 100 to make sure the sword spins fast enough
            float rotationSpeed = spellStats.GetStat(StatType.Speed).CurrentValue * 100;

            currentAngle += rotationSpeed * Time.deltaTime;
            currentAngle %= 360;

            float rad = currentAngle * Mathf.Deg2Rad;

            // Calculate offset from player
            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            Vector3 finalPos = centerPoint.position + new Vector3(x, y, 0);
            transform.position = finalPos;
        }

        private void RotateSelf()
        {
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);


            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                SendPayload(enemy);
            }

        }
    }
}
