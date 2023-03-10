using CodeBase.Infrastructure.Helpers;
using CodeBase.Player;
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
        
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private EnemyAgro enemyAgro;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private CollisionNotifier handCollNotifier;
        [SerializeField] private int damage;

        private State state;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _isDoneAttack;

        private EventsHolder EventsHolder => EventsHolder.Instance;
        
        private void OnEnable()
        {
            enemyAgro.PlayerEnter += StartFollow;
            enemyAgro.PlayerExit += StopFollow;
            animator.OnAttackEnded += OnAttackEnded;
            handCollNotifier.OnCustomTriggerEnter += HandCollTriggerEnter;
            EventsHolder.PlayerDie += DisableMovement;
        }

        private void OnDisable()
        {
            enemyAgro.PlayerEnter -= StartFollow;
            enemyAgro.PlayerExit -= StopFollow;
            animator.OnAttackEnded -= OnAttackEnded;
            handCollNotifier.OnCustomTriggerEnter += HandCollTriggerEnter;
            EventsHolder.PlayerDie -= DisableMovement;
        }
        private void Update()
        {
            UpdateCooldown();

            if (CanFollow()) 
                    StartFollow();
            else if(CanAttack())
                    StartAttack();

            IdentifyState();
        }

        private void HandCollTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Player) && !_isDoneAttack)
            {
                _isDoneAttack = true;
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }

        private void IdentifyState()
        {
            switch (state)
            {
                case State.Idle:
                    animator.PlayIdle();
                    break;
                case State.Follow:
                    MoveToTarget();
                    break;
                case State.Attack:
                    transform.LookAt(enemyAgro.player);
                    break;
            }
        }

        private void MoveToTarget()
        {
            _isAttacking = false;
            agent.SetDestination(enemyAgro.player.position);
            animator.PlayRun();
        }

        private bool CanAttack() => 
            enemyAgro.player && !_isAttacking && CooldownIsUp();

        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
            _isDoneAttack = false;
            state = State.Idle;
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

        private bool CanFollow() => 
            enemyAgro.player && Vector3.Distance(transform.position, enemyAgro.player.position) > 2f;

        private void StartFollow() => 
            state = State.Follow;

        private void StopFollow()
        {
            state = State.Idle;
            agent.isStopped = true;
            agent.ResetPath();
        }

        private void DisableMovement() => 
            enabled = false;
    }
}