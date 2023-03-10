using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public PlayerAnimator animator;
        public HpBar hpBar;
        public float currentHp;
        public float maxHp;

        private EventsHolder EventsHolder => EventsHolder.Instance;
        private void Start()
        {
            SetCurrentHp();
            hpBar.UpdateHpBar(maxHp, currentHp);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Loot))
            {
                Destroy(other.gameObject);
                if (currentHp < maxHp)
                {
                    currentHp++;
                    hpBar.UpdateHpBar(maxHp,currentHp);
                }
            }
        }
        public void TakeDamage(int damage)
        {
            if(currentHp <= 0)
                return;
            currentHp -= damage;
            animator.PlayHit();
            hpBar.UpdateHpBar(maxHp, currentHp);
            CheckIfDead();
        }
        
        private void CheckIfDead()
        {
            if (currentHp == 0)
            {
                animator.PlayDie();
                EventsHolder.OnPlayerDie();
            } 
            
        }
        private void SetCurrentHp() =>
            currentHp = maxHp;
    }
}