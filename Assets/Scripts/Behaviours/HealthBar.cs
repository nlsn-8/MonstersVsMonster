using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Behaviours
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image Fill;
        
        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            Fill.color = gradient.Evaluate(1f);
        }
        public void SetHealth(int health)
        {
            slider.value = health;
            Fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
