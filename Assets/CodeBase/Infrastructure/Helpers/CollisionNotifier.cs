using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Helpers
{
    public class CollisionNotifier : MonoBehaviour
    {
        #region 3D

        public event Action<Collision> OnCustomCollisionEnter;
        public event Action<Collision> OnCustomCollisionExit;

        public event Action<Collider> OnCustomTriggerEnter;
        public event Action<Collider> OnCustomTriggerExit;


        private void OnCollisionEnter(Collision collision)
        {
            OnCustomCollisionEnter?.Invoke(collision);
        }
        
        private void OnCollisionExit(Collision collision)
        {
            OnCustomCollisionExit?.Invoke(collision);
        }
        
        private void OnTriggerEnter(Collider collision)
        {
            OnCustomTriggerEnter?.Invoke(collision);
        }
        
        private void OnTriggerExit(Collider collision)
        {
            OnCustomTriggerExit?.Invoke(collision);
        }

        #endregion
    }
}
