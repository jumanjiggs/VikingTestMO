using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        
        [SerializeField] private PlayerAttack player;

        public Animator animator;

        public static readonly int DoAttack = Animator.StringToHash("Attack");
        public static readonly int Speed = Animator.StringToHash("Speed");

        public void PlayRun()
        {
            animator.SetFloat(Speed, 1f,0.1f, Time.deltaTime);
        }

        public void PlayRunBack()
        {
            animator.SetFloat(Speed, 0f,0.1f, Time.deltaTime);
        }

        public void PlayIdle()
        {
            animator.SetFloat(Speed, 0.5f, 0.1f, Time.deltaTime);
        }
        public void Attack()
        {
            animator.SetTrigger(DoAttack);
        }
        
        public void ResetAttack()
        {
            player.OnAttackEnded();
        }
    }
}