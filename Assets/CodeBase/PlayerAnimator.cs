using UnityEngine;

namespace CodeBase
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public void PlayRun() => 
            animator.SetTrigger(Run);

        public void PlayIdle() => 
            animator.SetTrigger(Idle);

        public void ResetRun() => 
            animator.ResetTrigger(Run);

        public void ResetIdle() => 
            animator.ResetTrigger(Idle);
    }
    
}