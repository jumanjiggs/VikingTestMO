using Cinemachine;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Camera
{
    public class CameraLogic : MonoBehaviour
    {
        public CinemachineVirtualCamera followCamera;

        [Inject]
        private void Construct(PlayerMovement player)
        {
            followCamera.Follow = player.transform;
            followCamera.LookAt = player.transform;
        }
    }
}