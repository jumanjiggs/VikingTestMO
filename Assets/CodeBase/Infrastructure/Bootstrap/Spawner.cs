using CodeBase.Enemies;
using CodeBase.Infrastructure.Logic;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class Spawner : MonoInstaller
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject playerPrefab;
        
        [SerializeField] private GameObject prefabEnemy;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private HpBar hpBar;
        public override void InstallBindings()
        {
            SpawnPlayer();
            SpawnEnemies();
        }

        private void SpawnPlayer()
        {
            PlayerMovement player = 
                Container.InstantiatePrefabForComponent<PlayerMovement>(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            Container.
                Bind<PlayerMovement>().
                FromInstance(player).
                AsSingle().
                NonLazy();
            player.GetComponent<PlayerHealth>().hpBar = hpBar;
        }
        private void SpawnEnemies()
        {
            foreach (var spawn in spawnPoints)
            {
                EnemyController enemy = 
                    Container.InstantiatePrefabForComponent<EnemyController>(prefabEnemy, spawn.position, Quaternion.identity,null);
                Container.
                    Bind<EnemyController>().
                    FromInstance(enemy);
            }
        }
        
    }
}