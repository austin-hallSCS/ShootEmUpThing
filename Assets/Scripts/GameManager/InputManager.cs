using UnityEngine;
using WizardGame.Input;

namespace WizardGame.Managers
{
    public class InputManager : ManagerBase
    {
        private GameControls controls;

        public Vector2 MoveInput => controls.Gameplay.WASD.ReadValue<Vector2>();
        public InputManager()
        {
            controls = new GameControls();
            controls.Enable();

            controls.Gameplay.Pause.performed += _ => EventManager.PublishGamePaused();
        }

        protected override void SubscribeToEvents()
        {

        }

        protected override void UnsubscribeFromEvents()
        {

        }

        public override void TearDown()
        {
            controls.Disable();
            controls.Dispose();
        }

    }
}
