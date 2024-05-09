using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Prototype
{
    public class InsidePortalObject : MonoBehaviour
    {
        List<Material> m_Materials = new List<Material>();
        private bool m_Inited;
        private static readonly int StencimComp = Shader.PropertyToID("_StencilComp");

        private void Awake()
        {         
            ShowOnlyInPortal();
            Init();
        }

        void Init()
        {
            if (m_Inited)
                return;

            m_Inited = true;
            var rendrers = GetComponentsInChildren<Renderer>();

            foreach (var item in rendrers)
            {
                m_Materials.AddRange(item.sharedMaterials);
            }
        }


        public void SetMaterials(bool fullRender)
        {
            Init();

            var stencilTest = fullRender ? CompareFunction.Equal : CompareFunction.NotEqual;

            foreach (var material in m_Materials)
            {
                material.SetInt(StencimComp, (int)stencilTest);
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