using CodeBase.Enemies;
using CodeBase.Infrastructure.Helpers;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        
        [SerializeField] private PlayerAnimator animator;
        [SerializeField] private CollisionNotifier handCollNotifier;
        [SerializeField] private int damage;
        private bool _isAttacking;

        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void OnEnable()
        {
            EventsHolder.PlayerDie += DisableMovement;
            handCollNotifier.OnCustomTriggerEnter += HandCollTriggerEnter;
        }

        private void OnDisable()
        {
            if(EventsHolder)EventsHolder.PlayerDie -= DisableMovement; 
            handCollNotifier.OnCustomTriggerEnter -= HandCollTriggerEnter;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isAttacking)
            {
                _isAttacking = true;
                animator.PlayAttack();
            }
        }
        private void HandCollTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Enemy) && _isAttacking)
            {
                _isAttacking = false;
                other.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
        
        public void OnAttackEnded() => 
            _isAttacking = false;

        private void DisableMovement() => 
            enabled = false;
    }
}