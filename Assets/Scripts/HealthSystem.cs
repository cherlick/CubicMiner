namespace HealthCareSystem
{
    using UnityEngine.UI;
    using UnityEngine;

    public class HealthSystem : MonoBehaviour
    {
        private float _health;
        private float _maxHealth;
        private bool _isAlive;
        private Slider _healthSlider;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _healthSlider = GetComponentInChildren<Slider>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    

public void HealthSystemSetup(float health)
        {
            _health = health;
            _maxHealth = health;
            _isAlive = true;
            
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = health;
            //Debug.Log(_healthSlider.maxValue);

        }

        public float GetHealth => _health;
        public bool IsAlive => _isAlive;
        public float GetHealthPercentage => _health/_maxHealth;

        public void TakeDamage(float damageAmount)
        {
            _canvasGroup.alpha = 1;

            _health -= damageAmount;

            _healthSlider.value = _health;

            if (_health <= 0) _isAlive = false;

            Invoke("UiOffAlpha", 2f);

        }

         public bool HeallingUp(float healAmount)
        {
            if (_health >= _maxHealth) return false;
            
            _health += healAmount;
            _healthSlider.value = _health;
            return true;
        }

        public void UiOffAlpha() =>_canvasGroup.alpha = 0;
    }
}

