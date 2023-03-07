using UnityEngine;
using Zenject;

namespace CodeBase
{
    public class SetupSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject playerPrefab;
        public override void InstallBindings()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            PlayerMovement player = 
                Container.InstantiatePrefabForComponent<PlayerMovement>(playerPrefab, spawnPoint.position, Quaternion.identity, null);
            Container.Bind<PlayerMovement>().FromInstance(player).AsSingle();
        }
    }
}