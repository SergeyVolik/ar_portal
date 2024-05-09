using System;
using UnityEngine;

namespace Prototype
{
    public class Portal : MonoBehaviour
    {
        private InsidePortalObject[] m_InsidePortalObjects;
        public PhysicsCallbacks trigger;
        private Camera m_Camera;
        private bool m_InsideCollider = false;
        private bool m_WasInFront = true;
        private bool m_InOtherWorld;
        public float checkFrontMult = 1f;
        public float portalOffsetMult = 1f;
        private void Awake()
        {
            m_InsidePortalObjects = FindObjectsOfType<InsidePortalObject>(true);
            m_Camera = Camera.main;

            trigger.onTriggerEnter += Trigger_onTriggerEnter;
            trigger.onTriggerExit += Trigger_onTriggerExit;
        }

        private void Trigger_onTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
            {
                m_InsideCollider = false;
            }
        }

        private void Trigger_onTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
            {
                TryTeleport();

                m_WasInFront = GetIsInFront();
                m_InsideCollider = true;
            }
        }

        bool GetIsInFront()
        {
            Vector3 worldPos = m_Camera.transform.position + m_Camera.transform.forward * m_Camera.nearClipPlane * checkFrontMult;
            var camPostionInPortalSpace = trigger.transform.InverseTransformPoint(worldPos);
            return camPostionInPortalSpace.z >= 0 ? true : false;
        }

        private void Update()
        {
            if (!m_InsideCollider)
                return;

            TryTeleport();
        }

        private void TryTeleport()
        {
            bool isInFront = GetIsInFront();
            if (isInFront != m_WasInFront)
            {
                m_InOtherWorld = !m_InOtherWorld;
                SetMaterials(m_InOtherWorld);

                transform.position += transform.forward * portalOffsetMult * (m_InOtherWorld ? 1f : -1f);
                trigger.transform.position+= trigger.transform.forward * 0.2f * (m_InOtherWorld ? -1f : 1f);
            }

            m_WasInFront = isInFront;
        }

        private void SetMaterials(bool enabmeRender)
        {
            foreach (var item in m_InsidePortalObjects)
            {
                item.SetMaterials(enabmeRender);
            }
        }
    }
}
