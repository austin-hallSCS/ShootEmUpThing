using UnityEngine;
using UnityEngine.UI;

namespace WizardGame.PlayerSystem
{
    public class HealthBarController : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateHealthBar(float currentValue, float maxValue)
        {
            slider.value = currentValue / maxValue;
        }
    }
}

