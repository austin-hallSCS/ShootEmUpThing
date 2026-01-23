using UnityEngine;

namespace WizardGame.Managers
{
    /// <summary>
    /// Foundation for all non-monobehaviour POCO managers.
    /// </summary>
    public abstract class ManagerBase
    {
        protected GameManager gameManager;

        public ManagerBase(GameManager manager)
        {
            gameManager = manager;
        }

        // -- Contract Methods --
        protected abstract void SubscribeToEvents();
        protected abstract void UnsubscribeFromEvents();

        // Cleans up subscriptions and resources before the manager is destroyed.
        public void TearDown()
        {
            UnsubscribeFromEvents();

            // For child-specific cleanup.
            OnTearDown();

            Debug.Log($"{this.GetType().Name} TearDown complete");
        }

        protected virtual void OnTearDown() { }
    }
}
