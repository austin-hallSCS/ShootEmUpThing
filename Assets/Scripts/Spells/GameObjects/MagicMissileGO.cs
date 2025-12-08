using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.Spells
{
    public class MagicMissileGO : SpellGO
    {
        [field: SerializeField] private float curveHeightRange;

        private Vector3 startPoint;
        private Vector3 controlPoint;


        private float timeElapsed = 0f;
        private float flightDuration = 1.0f;
        private float curveHeight;

        private bool isLaunched = false;

        public override void Initialize(SpellStats stats, Transform targetTransform)
        {
            base.Initialize(stats, targetTransform);
            curveHeight = Random.Range(-curveHeightRange, curveHeightRange);

            Launch();
        }

        public override void Launch()
        {
            startPoint = transform.position;
            isLaunched = true;

            float speed = spellStats.GetStat(StatType.Speed).CurrentValue;

            // Calculate flight time based on distance to target, default to 1 second if target is null.
            float distance = target != null ? Vector3.Distance(startPoint, target.position) : 10f;
            flightDuration = distance / speed;

            // Calculate control point
            Vector3 endPoint = target != null ? target.position : startPoint + (Vector3.up * 10);

            // Get direction vector
            Vector3 direction = (endPoint - startPoint).normalized;

            // Get perpendicular vector (-y, x) for 2D
            Vector3 perpindicular = new Vector3(-direction.y, direction.x, 0);

            // Midpoint
            Vector3 midPoint = startPoint + (direction * (distance / 2f));

            // Final control point
            controlPoint = midPoint + (perpindicular * curveHeight);
        }

        protected override void Update()
        {
            if (!isLaunched) return;

            float t = IncrementTime();
            Vector3 endPoint = GetEndPoint();

            if (t >= 1.0f)
            {
                //TODO: Add damage logic
                Destroy(gameObject);
                return;
            }

            Vector3 finalPosition = BezierCalculation(endPoint, t);

            // Rotation
            Vector3 direction = (finalPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = finalPosition;
        }

        private float IncrementTime()
        {
            timeElapsed += Time.deltaTime;
            return timeElapsed / flightDuration;
        }

        private Vector3 GetEndPoint()
        {
            // Target might die while missile is flying
            if (target != null)
            {
                return target.position;
            }
            else
            {
                // Destroying for now, may change this to continue wherever the target was last
                Destroy(gameObject);
                return Vector3.up; // This is temp, figure out what to return later.
            }
        }
        private Vector3 BezierCalculation(Vector3 endPoint, float currentTime)
        {
            //-- BEZIER FORMULA --
            // Position = (1-t)^2 * Start + 2(1-t)t * Control + t^2 * End

            Vector3 p1 = Vector3.Lerp(startPoint, controlPoint, currentTime);
            Vector3 p2 = Vector3.Lerp(controlPoint, endPoint, currentTime);
            return Vector3.Lerp(p1, p2, currentTime);
        }
    }
}
