using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Logic
{
    public class HpBar : MonoBehaviour
    {
        public Image healthBarSprite;
        public void UpdateHpBar(float maxHealth, float currentHealth) => 
            healthBarSprite.fillAmount = currentHealth / maxHealth;
        
    }
}