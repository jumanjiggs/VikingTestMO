using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        public event Action OnAttackEnded;
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        public Animator anim;

        public void PlayIdle()
        {
            anim.SetFloat(Speed, 0f, 0.1f, Time.deltaTime);
        }

        public void PlayRun()
        {
            anim.SetFloat(Speed, 1f,0.1f, Time.deltaTime);
        }

        public void PlayAttack()
        {
            anim.SetTrigger(Attack);
        }
        
        public void ResetAttack()
        {
            PlayIdle();
            OnAttackEnded?.Invoke();
            Debug.Log("Finished Attack");
        }
    }
}