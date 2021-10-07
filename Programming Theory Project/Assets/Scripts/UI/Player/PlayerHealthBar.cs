using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player
{
    public class PlayerHealthBar : MonoBehaviour
    {
        public Image healthFillImage;

        private Health _playerHealth;

        private void Start()
        {
            _playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            if(!_playerHealth)
                Debug.LogError(Constants.PlayerTagNotFound);
        }

        private void Update()
        {
            healthFillImage.fillAmount = _playerHealth.CurrentHealth / _playerHealth.maxHealth;
        }
    }
}
