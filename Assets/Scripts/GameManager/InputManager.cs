using UnityEngine;
using WizardGame.Input;

namespace WizardGame.Managers
{
    public class InputManager : ManagerBase
    {
        private GameControls controls;

        public Vector2 MoveInput => controls.Gameplay.WASD.ReadValue<Vector2>();
        public InputManager(GameManager manager) : base(manager)
        {
            controls = new GameControls();
            controls.Enable();

            SubscribeToEvents();
        }

        protected override void SubscribeToEvents()
        {
            controls.Gameplay.Pause.performed += _ => EventManager.PublishGamePaused();
        }

        protected override void UnsubscribeFromEvents()
        {
            controls.Gameplay.Pause.performed -= _ => EventManager.PublishGamePaused();
        }

        protected override void OnTearDown()
        {
            controls.Disable();
            controls.Dispose();
        }

    }
}
