using UnityEngine;
using UnityEngine.UI;

namespace Scenes.ATH
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _slider;

        void Start()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetMaxHealth(int maxhealth)
        {
            _slider.maxValue = maxhealth;
            _slider.value = maxhealth;
        }
    
        public void SetHealth(int health)
        {
            _slider.value = health;
        }
    }
}
