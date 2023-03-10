using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAnimator : GeneralAnimator
    {
        [SerializeField] private EnemyController enemy;
        public void Attack()
        {
            _currentState = State.Idle;
            if (_currentState == State.Idle)
            {
                _currentState = State.Other;
                animator.SetTrigger(DoAttack);
            }
        }
        
        public void ResetAttack()
        {
            enemy.OnAttackEnded();
            PlayIdle();
        }
    }
}