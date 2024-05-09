using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Prototype
{
    public class PlaceOnPlane : MonoBehaviour
    {
        private ARRaycastManager m_ARRaycastManager;
        private Vector2 m_TouchPosition;

        public GameObject SceneObject;

        static List<ARRaycastHit> Hits = new List<ARRaycastHit>();

        private void Awake()
        {
            m_ARRaycastManager = GetComponent<ARRaycastManager>();
            SceneObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                m_TouchPosition = Input.GetTouch(0).position;

                if (m_ARRaycastManager.Raycast(m_TouchPosition, Hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = Hits[0].pose;

                    SceneObject.SetActive(true);
                    SceneObject.transform.position = hitPose.position;
                    LookAtPlayer(SceneObject.transform);
                }
            }
        }

        void LookAtPlayer(Transform scene)
        {
            var LookDirection = Camera.main.transform.position - scene.position;
            LookDirection.y = 0;
            scene.rotation = Quaternion.LookRotation(LookDirection);
        }
    }
}
