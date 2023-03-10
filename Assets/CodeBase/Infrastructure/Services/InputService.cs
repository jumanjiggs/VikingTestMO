using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public  class InputService : MonoBehaviour,  IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector3 Axis
        {
            get
            {
                Vector3 axis = new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical));
                return axis;
            }
        }
    }
}