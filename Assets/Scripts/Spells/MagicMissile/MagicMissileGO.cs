using System;
using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class MagicMissileGO : ProjectileGO
    {
        [Header("Bezier Settings")]
        [field: SerializeField] private float curveHeightRange;

        //-- State --
        private Vector3 startPoint;
        private Vector3 controlPoint;
        private Vector3 lastKnownTargetPos;

        private Transform target;




        private float timeElapsed = 0f;
        private float flightDuration = 1.0f;
        private float t;
        private float curveHeight;

        private bool isLaunched = false;

        public void Initialize(SpellController parentController, Transform targetTransform)
        {
            base.Initialize(parentController);

            target = targetTransform;

            if (target != null) lastKnownTargetPos = target.position;
            else lastKnownTargetPos = transform.position + transform.forward * 5f;

            SetupBezierCurve();
            isLaunched = true;
        }

        public void SetupBezierCurve()
        {
            startPoint = transform.position;
            float speed = spellStats.GetStat(StatType.Speed).CurrentValue;

            float distance = Vector3.Distance(startPoint, lastKnownTargetPos);

            // Prevent divide by 0 errors
            if (speed <= 0) speed = 10f;

            flightDuration = distance / speed;

            //-- Control Point Calculation
            Vector3 endPoint = lastKnownTargetPos;
            Vector3 direction = (endPoint - startPoint).normalized;

            // Get perpendicular vector (-y, x) for 2D
            Vector3 perpindicular = new Vector3(-direction.y, direction.x, 0);

            float randomHeight = UnityEngine.Random.Range(-curveHeightRange, curveHeightRange);

            Vector3 midPoint = startPoint + (direction * (distance / 2f));
            controlPoint = midPoint + (perpindicular * randomHeight);
        }

        protected void Update()
        {
            if (!isLaunched) return;

            if (target != null)
            {
                lastKnownTargetPos = target.position;
            }

            timeElapsed += Time.deltaTime;
            t = timeElapsed / flightDuration;

            if (t >= 1.0f)
            {
                transform.position = lastKnownTargetPos;
                OnImpact();
                return;
            }

            Move();
        }

        protected override void Move()
        {
            Vector3 nextPosition = BezierCalculation(startPoint, controlPoint, lastKnownTargetPos, t);

            Vector3 direction = (nextPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            transform.position = nextPosition;
        }

        private Vector3 BezierCalculation(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            //-- BEZIER FORMULA --
            // Position = (1-t)^2 * Start + 2(1-t)t * Control + t^2 * End

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
            return p;
        }

        private void OnImpact()
        {
            if (target != null)
            {
                EnemyController enemy = target.gameObject.GetComponent<EnemyController>();
                if (enemy != null) SendPayload(enemy);
            }

            DestroySelf();
        }
    }
}
