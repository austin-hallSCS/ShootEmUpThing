using UnityEngine;
using WizardGame.Stats;

namespace WizardGame.SpellSystem
{
    public class SpellController : MonoBehaviour
    {
        [SerializeField] protected LayerMask whatIsEnemy;
        [SerializeField] protected GameObject spellPrefab;
        [SerializeField] protected float spawnRadius;
        [SerializeField] protected SpellDataSO spellData;
        public Camera Cam;

        protected SpellStats runtimeStats;

        // Timers - need to figure this out later
        // protected Timer levelUpTimer = new Timer(5f);

        // Temp level up timer for testing
        // protected float currentLevelUpTimerAt;


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
            // enemyCheckSize = GetCameraSize();

            if (spellData == null)
            {
                Debug.LogError($"Spell Data not assigned on: {gameObject.name}");

            }

            Debug.Log($"SpellDataSO value: {spellData.DamageAmount.BaseValue}");
            Debug.Log($"SpellDataSO CurrentValue: {spellData.DamageAmount.CurrentValue}");
            runtimeStats = SpellStats.CopyFrom(spellData);
            Debug.Log($"runtimeStats value: {runtimeStats.DamageAmount.BaseValue}");
            Debug.Log($"runtimeStats CurrentValue: {runtimeStats.DamageAmount.CurrentValue}");
            
        }

        protected virtual void Start()
        {
            SpellDeactivate();
            // currentLevelUpTimerAt = 5.0f;
        }

        protected virtual void Update()
        {
            // currentLevelUpTimerAt -= Time.deltaTime;
            // if (currentLevelUpTimerAt <= 0)
            // {
            //     LevelUp();
            //     currentLevelUpTimerAt = 5.0f;
            // }
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
            runtimeStats.IncreaseLevel();
            spellData.ApplyLevelUp(runtimeStats, runtimeStats.Level);
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

        protected virtual void ResetCoolDown() => currentCoolDownTimeAt = runtimeStats.CooldownTime.CurrentValue;

        protected virtual void ResetDuration() => currentDurationTimeAt = runtimeStats.DurationTime.CurrentValue;

        // protected Vector2 GetCameraSize()
        // {
        //     float aspect = Screen.width / Screen.height;
        //     var orthoSize = Cam.orthographicSize;

        //     float width = 2.0f * orthoSize * aspect;
        //     float height = 2.0f * orthoSize;

        //     return new Vector2(width, height);

        // }

        // protected Vector3 GetNearestEnemyPosition()
        // {
        //     RaycastHit2D[] detectedEnemies = Physics2D.BoxCastAll(new Vector2(0, 0), enemyCheckSize, 0.0f, new Vector2(0, 0), 5f, whatIsEnemy);

        //     if (detectedEnemies.Length != 0)
        //     {
        //         nearestEnemy = detectedEnemies[0].transform;
        //     }
        //     else
        //     {
        //         nearestEnemy = null;
        //     }

        //     return nearestEnemy.position;
        // }
    }
}
