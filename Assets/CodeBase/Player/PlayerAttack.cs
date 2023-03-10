using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator animator;
        private bool _isAttacking;
        private void Update()
        {
            if(Input.GetMouseButtonDown(0) && !_isAttacking) 
                animator.Attack();
        }
        
        public void OnAttackEnded() => 
            _isAttacking = false;
    }
}