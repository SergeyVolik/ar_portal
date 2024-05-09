using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype
{
    public class InsidePortalObject : MonoBehaviour
    {
        private Renderer[] m_rendrers;

        private void Awake()
        {
            m_rendrers = GetComponentsInChildren<Renderer>();
            ShowOnlyInPortal();
        }

        void SetMaterials(bool fullRender)
        {
            var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;

            foreach (var item in m_rendrers)
            {
                foreach (var material in item.sharedMaterials)
                {
                    material.SetInt("_StencilComp", (int)stencilTest);
                }
            }
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