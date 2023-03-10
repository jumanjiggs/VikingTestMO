using System;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        public event Action OnAttackEnded;
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        public Animator anim;

        public void PlayIdle() => 
            anim.SetFloat(Speed, 0f, 0.1f, Time.deltaTime);

        public void PlayRun() => 
            anim.SetFloat(Speed, 1f,0.1f, Time.deltaTime);

        public void PlayAttack() => 
            anim.SetTrigger(Attack);

        public void PlayHit() => 
            anim.SetTrigger(Hit);

        public void PlayDie() => 
            anim.SetTrigger(Die);

        public void ResetAttack()
        {
            PlayIdle();
            OnAttackEnded?.Invoke();
        }
    }
}