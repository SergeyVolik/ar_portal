using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype
{
    public class InsidePortalObject : MonoBehaviour
    {
        private Renderer[] Rendrers
        {
            get
            {
                if (m_Rendrers == null || m_Rendrers.Length == 0)
                    m_Rendrers = GetComponentsInChildren<Renderer>();
                return m_Rendrers;
            }
        }

        private Renderer[] m_Rendrers;

        private void Awake()
        {
            m_Rendrers = null;
            ShowOnlyInPortal();
        }

        public void SetMaterials(bool fullRender)
        {
            var stencilTest = fullRender ? CompareFunction.Equal : CompareFunction.NotEqual;

            foreach (var item in Rendrers)
            {
                foreach (var material in item.sharedMaterials)
                {
                    material.SetInt("_StencilComp", (int)stencilTest);
                }
            }
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                ShowAll();
        }

        public void ShowOnlyInPortal()
        {
            SetMaterials(false);
        }

        public void ShowAll()
        {
            SetMaterials(true);
        }
    }
}