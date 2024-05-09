using UnityEngine;

namespace Prototype
{
    public class Portal : MonoBehaviour
    {
        private InsidePortalObject[] m_InsidePortalObjects;
        private Camera m_Camera;
        private bool m_InsideCollider = false;
        private bool m_WasInFront;
        private bool m_InOtherWorld;

        private void Awake()
        {
            m_InsidePortalObjects = FindObjectsOfType<InsidePortalObject>(true);
            m_Camera = Camera.main;
        }

        private void Update()
        {
            if (!m_InsideCollider)
                return;

            var transForward = transform.forward;
            var vecotr = m_Camera.transform.position - transform.position;
            var plane1 = Vector3.ProjectOnPlane(transForward, Vector3.up);
            var plane2 = Vector3.ProjectOnPlane(vecotr, Vector3.up);

        
            bool isInFront = Vector3.Dot(plane1, plane2) <= 0;// GetIsInFront();
            Debug.Log(isInFront);
            if ((isInFront && !m_WasInFront) || (m_WasInFront && !isInFront))
            {
                m_InOtherWorld = !m_InOtherWorld;
                if (m_InsideCollider)
                {
                    foreach (var item in m_InsidePortalObjects)
                    {
                        item.SetMaterials(m_InOtherWorld);
                    }
                }
            }
            m_WasInFront = isInFront;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
            {               
                m_InsideCollider = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
            {
                m_InsideCollider = false;
            }
        }
    }
}
