using Cinemachine;
using UnityEngine;
using Zenject;

namespace CodeBase
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