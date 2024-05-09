using System;
using UnityEngine;

namespace Prototype
{
    public class PhysicsCallbacks : MonoBehaviour
    {
        public event Action<Collider> onTriggerEnter = delegate { };
        public event Action<Collider> onTriggerExit = delegate { };

        public void OnTriggerEnter(Collider other)
        {
            onTriggerEnter.Invoke(other);
        }

        public void OnTriggerExit(Collider other)
        {
            onTriggerExit.Invoke(other);
        }
    }
}
