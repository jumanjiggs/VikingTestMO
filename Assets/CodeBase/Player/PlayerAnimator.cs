using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        
        [SerializeField] private PlayerAttack player;

        public Animator animator;

        private static readonly int DoAttack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        public void PlayRun() => 
            animator.SetFloat(Speed, 1f,0.1f, Time.deltaTime);

        public void PlayRunBack() => 
            animator.SetFloat(Speed, 0f,0.1f, Time.deltaTime);

        public void PlayIdle() => 
            animator.SetFloat(Speed, 0.5f, 0.1f, Time.deltaTime);

        public void PlayHit() => 
            animator.SetTrigger(Hit);
        
        public void PlayDie() => 
            animator.SetTrigger(Die);

        public void PlayAttack() => 
            animator.SetTrigger(DoAttack);

        public void ResetAttack() => 
            player.OnAttackEnded();
    }
}