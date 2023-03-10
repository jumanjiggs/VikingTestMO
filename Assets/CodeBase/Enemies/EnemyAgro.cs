using System;
using CodeBase.Infrastructure.Helpers;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAgro : MonoBehaviour
    {
        public event Action PlayerEnter;
        public event Action PlayerExit;
        
        public Transform player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Player))
            {
                player = other.gameObject.transform;
                PlayerEnter?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Player))
            {
                player = null;
                PlayerExit?.Invoke();
            }
        }

      
       
    }
}
