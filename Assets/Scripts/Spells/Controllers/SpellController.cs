using UnityEngine;
using System.Collections;
using WizardGame.Stats;
using Unity.VisualScripting;

namespace WizardGame.SpellSystem
{
    public class SpellController : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;
        [SerializeField] protected GameObject spellPrefab;
        [SerializeField] protected float spawnRadius;

        [SerializeField] public SpellDataSO SpellData;
        protected SpellStats spellStats;
        
        public Camera Cam;
 


        // Status variables
        protected float currentCoolDownTimeAt;
        protected float coolDownTime;
        protected float duration;
        protected float currentDurationTimeAt;
        protected float projectileAmount;
        protected bool isActive;

        // Check transforms
        protected Vector2 enemyCheckSize;

        private Transform nearestEnemy;


        protected virtual void Awake()
        {
            enemyCheckSize = GetCameraSize();
            spellStats = GetComponentInChildren<SpellStats>();
        }

        protected virtual void Start()
        {
            SpellDeactivate();
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            CheckSpellActiveStatus();
            if (isActive)
            {
                SpellActiveBehavior();
            }
        }

        public virtual void LevelUp()
        {
            spellStats.Level.Increase(1);
            SpellDataSO.ApplyLevelUp(spellStats, (int)spellStats.Level.CurrentValue);
        }

        protected virtual void CheckSpellActiveStatus()
        {
            if (!isActive)
            {
                currentCoolDownTimeAt -= Time.deltaTime;
                if (currentCoolDownTimeAt <= 0)
                {
                    SpellActivate();
                }
            }
        }

        protected virtual void SpellActivate()
        {
            isActive = true;
        }

        protected virtual void SpellDeactivate()
        {
            isActive = false;
            ResetCoolDown();
        }

        protected virtual void SpellActiveBehavior() { }

        protected virtual void ResetCoolDown() => currentCoolDownTimeAt = spellStats.CoolDownTime.CurrentValue;

        protected virtual void ResetDuration() => currentDurationTimeAt = spellStats.DurationTime.CurrentValue;

        public virtual void IncreaseLevel()
        {
            spellStats.Level.Increase(1);
        }

        protected Vector2 GetCameraSize()
        {
            float aspect = Screen.width / Screen.height;
            var orthoSize = Cam.orthographicSize;

            float width = 2.0f * orthoSize * aspect;
            float height = 2.0f * orthoSize;

            return new Vector2(width, height);

        }

        protected Vector3 GetNearestEnemyPosition()
        {
            RaycastHit2D[] detectedEnemies = Physics2D.BoxCastAll(new Vector2(0, 0), enemyCheckSize, 0.0f, new Vector2(0, 0), 5f, whatIsEnemy);

            if (detectedEnemies.Length != 0)
            {
                nearestEnemy = detectedEnemies[0].transform;
            }
            else
            {
                nearestEnemy = null;
            }

            return nearestEnemy.position;
        }
    }
}
