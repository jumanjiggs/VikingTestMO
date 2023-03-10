using System.Collections;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Infrastructure.Logic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public EnemyAnimator animator;
        public EnemyController enemyController;
        public GameObject prefabLoot;
        public HpBar hpBar;
        public float currentHp;
        public float maxHp;
        
        [SerializeField] private Collider col;
        [SerializeField] private NavMeshAgent navmesh;
        [SerializeField] private SkinnedMeshRenderer skin;
        [SerializeField] private GameObject hpBarCanvas;
        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void Start()
        {
            SetCurrentHp();
            hpBar.UpdateHpBar(maxHp, currentHp);
        }

        public void TakeDamage(float damage)
        {
            if (currentHp <= 0)
                return;
            currentHp -= damage;
            animator.PlayHit();
            hpBar.UpdateHpBar(maxHp, currentHp);

            if (currentHp == 0) 
                StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            EventsHolder.OnEnemyDie();
            enemyController.enabled = false;
            animator.PlayDie();
            yield return new WaitForSeconds(3f);
            SpawnLoot();
            SwitchComponentsState(false);
            yield return new WaitForSeconds(2f);
            Respawn();

        }

        private void SwitchComponentsState(bool activate)
        {
            col.enabled = activate;
            navmesh.enabled = activate;
            skin.enabled = activate;
            hpBarCanvas.SetActive(activate);
        }
        

        private void Respawn()
        {
            SwitchComponentsState(true);
            enemyController.enabled = true;
            SetRandomPosition();
            animator.PlayIdle();
            IncreaseHealth();
            SetCurrentHp();
            hpBar.UpdateHpBar(maxHp,currentHp);
        }

        private void SpawnLoot()
        {
            var lootPosition = transform.position;
            lootPosition.y += Constants.OffsetLootY; 
            lootPosition.z += Constants.OffsetLootZ;
            Instantiate(prefabLoot, lootPosition, Quaternion.identity);
        }

        private void IncreaseHealth() => 
            maxHp++;
        private void SetRandomPosition() => 
            transform.position += new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * 10f;

        private void SetCurrentHp() => 
            currentHp = maxHp;
    }
}