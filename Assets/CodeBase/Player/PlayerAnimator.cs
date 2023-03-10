using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : GeneralAnimator
    {
        [SerializeField] private PlayerAttack player;
        
        public void Attack()
        {
            if (_currentState == State.Idle)
            {
                _currentState = State.Other;
                animator.SetTrigger(DoAttack);
            }
        }
        
        public void ResetAttack()
        {
            player.OnAttackEnded();
            _currentState = State.None;
            PlayIdle();
        }
    }
}