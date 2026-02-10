using UnityEngine;
using WizardGame.Enemy;
using WizardGame.Stats;
using WizardGame.Utils;

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

        private bool isLaunched = false;

        public override void Initialize(SpellCastContext context)
        {
            base.Initialize(context);

            target = context.TargetEnemy;

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
            // Curvature calculation
            Vector3 nextPosition = VectorUtils.BezierCalculation(startPoint, controlPoint, lastKnownTargetPos, t);

            Vector3 direction = (nextPosition - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            transform.position = nextPosition;
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
