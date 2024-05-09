using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    ARRaycastManager ARRaycastManager;
    private Vector2 touchPosition;

    public GameObject ScenePrefab;

    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    private void Awake()
    {
        ARRaycastManager = GetComponent<ARRaycastManager>();
        ScenePrefab.SetActive(false);
    }

    // Start is called before the fist frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;

            if (ARRaycastManager.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = Hits[0].pose;

                ScenePrefab.SetActive(true);
                ScenePrefab.transform.position = hitPose.position;
                LookAtPlayer(ScenePrefab.transform);
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
