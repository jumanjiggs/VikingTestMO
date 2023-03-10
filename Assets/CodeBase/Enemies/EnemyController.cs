using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Follow,
            Attack
        }
        
        public float attackCooldown = 3f;
        
        [SerializeField] private EnemyAgro enemyAgro;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private NavMeshAgent agent;

        private float _attackCooldown;
        private bool _isAttacking;
        private State state;
        
        private void Update()
        {
            UpdateCooldown();

            if (CanFollow()) 
                    StartFollow();
            else if(CanAttack())
                    StartAttack();

            switch (state)
            {
                case State.Idle:
                {
                    animator.PlayIdle();
                    break;
                }
                case State.Follow:
                {
                    MoveToTarget();
                    break;
                }
                case State.Attack:
                {
                    transform.LookAt(enemyAgro.player);
                    break;
                }
            }
        }

        private bool CanFollow()
        {
            return enemyAgro.player && Vector3.Distance(transform.position, enemyAgro.player.position) > 2f;
        }
        private void MoveToTarget()
        {
            _isAttacking = false;
            agent.SetDestination(enemyAgro.player.position);
            animator.PlayRun();
        }
        private void StartAttack()
        {
            animator.PlayAttack();
            _isAttacking = true;
            state = State.Attack;
        }
        
        private bool CooldownIsUp() => 
            _attackCooldown <= 0f;
        
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }
        
        private bool CanAttack()
        {
            return enemyAgro.player && !_isAttacking && CooldownIsUp();
        }

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
            state = State.Idle;
        }

        private void StartFollow()
        {
            state = State.Follow;
        }

        private void StopFollow()
        {
            state = State.Idle;
            agent.isStopped = true;
            agent.ResetPath();
        }

        private void OnEnable()
        {
            enemyAgro.PlayerEnter += StartFollow;
            enemyAgro.PlayerExit += StopFollow;
            animator.OnAttackEnded += OnAttackEnded;
        }

        private void OnDisable()
        {
            enemyAgro.PlayerEnter -= StartFollow;
            enemyAgro.PlayerExit -= StopFollow;
            animator.OnAttackEnded -= OnAttackEnded;
        }
    }
}