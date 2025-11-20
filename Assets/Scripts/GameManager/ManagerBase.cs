using UnityEngine;

namespace WizardGame.Managers
{
    /// <summary>
    /// Foundation for all non-monobehaviour POCO managers.
    /// </summary>
    public abstract class ManagerBase
    {
        // Subscribes to global events as defined in the EventManager
        protected abstract void SubscribeToEvents();

        // Unsubscribes from global events. Called automatically during Teardown.
        protected abstract void UnsubscribeFromEvents();

        // Cleans up subscriptions and resources before the manager is destroyed.
        public abstract void TearDown();
    }
}
