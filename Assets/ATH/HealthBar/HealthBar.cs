using UnityEngine;
using UnityEngine.UI;

namespace ATH.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        Slider _slider;

        void Start()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxHealth)
        {
            _slider.maxValue = maxHealth;
            _slider.value = maxHealth;
        }
    
        public void SetHealth(int health)
        {
            _slider.value = health;
        }
    }
}
