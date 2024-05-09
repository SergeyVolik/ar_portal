using UnityEngine;

namespace Prototype
{
    public class Portal : MonoBehaviour
    {
        private InsidePortalObject[] m_InsidePortalObjects;

        private void Awake()
        {
            m_InsidePortalObjects = FindObjectsOfType<InsidePortalObject>();
        }

        bool showed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
            {
                if (showed)
                {
                    foreach (var item in m_InsidePortalObjects)
                    {
                        item.ShowAll();
                    }
                }
                else {
                    foreach (var item in m_InsidePortalObjects)
                    {
                        item.ShowOnlyInPortal();
                    }
                }

                showed = !showed; 
            }
        }
    }
}
