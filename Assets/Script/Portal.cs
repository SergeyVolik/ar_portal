using UnityEngine;

namespace Prototype
{
    public class Portal : MonoBehaviour
    {
        private void Awake()
        {
            var insidePortalObjects = FindObjectOfType<InsidePortalObject>();
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
