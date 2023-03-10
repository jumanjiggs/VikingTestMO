using CodeBase.Enemies;
using UnityEngine;

namespace CodeBase
{
    public abstract class GeneralAnimator : MonoBehaviour
    {
        public Animator animator;
       

        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int RunDir = Animator.StringToHash("RunDirection");
        private static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int DoAttack = Animator.StringToHash("Attack");

        // public void PlayRun()
        // {
        //     if (_currentState == State.RunForward)
        //         return;
        //     _currentState = State.RunForward;
        //     animator.SetInteger(RunDir, 1);
        //     animator.SetTrigger(Run);
        // }
        //
        // public void PlayRunBack()
        // {
        //     if (_currentState == State.RunBack)
        //         return;
        //     _currentState = State.RunBack;
        //     animator.SetInteger(RunDir, -1);
        //     animator.SetTrigger(Run);
        // }
        //
        // public void PlayIdle()
        // {
        //     if (_currentState == State.Idle || _currentState == State.Other)
        //         return;
        //     _currentState = State.Idle;
        //     animator.SetTrigger(Idle);
        // }
        
        
    }
}