using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Input;
using WizardGame.Services;

namespace WizardGame.Managers
{
    public class InputManager : ManagerBase
    {
        private GameControls controls;
        private InputActionMap activeMap;

        public Vector2 MoveInput => controls.Gameplay.WASD.ReadValue<Vector2>();
        public InputManager(GameManager manager) : base(manager)
        {
            controls = new GameControls();
            activeMap = controls.Gameplay;
            activeMap.Enable();

            SubscribeToEvents();
        }

        public void EnableNewActionMap(InputActionMap newMap)
        {
            activeMap.Disable();
            newMap.Enable();
            activeMap = newMap;
        }

        protected override void SubscribeToEvents()
        {
            controls.Gameplay.Pause.performed += _ => EventBus.PublishGamePaused();
            EventBus.OnPlayerLevelUp += _ => EnableNewActionMap(controls.UI);
            EventBus.OnGameResumed += () => EnableNewActionMap(controls.Gameplay);
        }

        protected override void UnsubscribeFromEvents()
        {
            controls.Gameplay.Pause.performed -= _ => EventBus.PublishGamePaused();
            EventBus.OnPlayerLevelUp -= _ => EnableNewActionMap(controls.UI);
            EventBus.OnGameResumed -= () => EnableNewActionMap(controls.Gameplay);
        }

        protected override void OnTearDown()
        {
            controls.Disable();
            controls.Dispose();
        }

    }
}
