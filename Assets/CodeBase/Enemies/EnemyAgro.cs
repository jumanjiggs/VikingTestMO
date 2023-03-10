using CodeBase.Infrastructure.Helpers;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAgro : MonoBehaviour
    {
        public Transform player;
        public GeneralAnimator animator;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Player))
            {
                player = other.gameObject.transform;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Player))
            {
                player = null;
                animator.PlayIdle();
            }
        }

      
       
    }
}
