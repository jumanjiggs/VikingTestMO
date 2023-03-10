using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public float attackCooldown = 3f;
        
        [SerializeField] private EnemyAgro enemyAgro;
        [SerializeField] private float speed;
        [SerializeField] private EnemyAnimator animator;
        
        private float _attackCooldown;
        private bool _isAttacking;

        private void Update()
        {
            UpdateCooldown();
            
            if (enemyAgro.player)
            {
                if(Vector3.Distance(transform.position, enemyAgro.player.position) > 2f)
                    MoveToTarget();
                else if(CanAttack())
                    StartAttack();
            }
        }
        private void MoveToTarget()
        {
            _isAttacking = false;
            transform.position = Vector3.MoveTowards(transform.position, enemyAgro.player.position, speed * Time.deltaTime);
            transform.LookAt(enemyAgro.player);
            animator.PlayRun();
        }
        private void StartAttack()
        {
            animator.Attack();
            _isAttacking = true;
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
            return !_isAttacking && CooldownIsUp();
        }
        
        public void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }
        
    }
}