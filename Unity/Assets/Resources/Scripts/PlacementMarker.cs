using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif
public class PlacementMarker : MonoBehaviour
{
    // Camera used for raycasting screen point.
    private Camera firstPersonCamera;

    [SerializeField]
    private GameObject markerPrefab;

    private SpawnRoom spanwRoom;
    private List<GameObject> marker;
    private GlobalDataContainer container;
    private Anchor anchor;

    void Start()
    {
        FindDataContainer();
        marker = new List<GameObject>();
        firstPersonCamera = container.FirstPersonCamera;
        spanwRoom = GetComponent<SpawnRoom>();

        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                "Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                "ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // The session status must be Tracking in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ProcessTouch();
        if (marker.Count > 0)
        {
            RaycastCenter();
        }

        if (marker.Count > 2)
        {
            marker[marker.Count - 1].SetActive(false);
        }
        //only for testing
        MeassureDistance();
    }

    private void MeassureDistance()
    {
        if (marker.Count > 2)
        {
            var distance = Vector3.Distance(marker[0].transform.position, marker[1].transform.position);
            Debug.Log("Distance: " + distance);

            GameObject.Find("MarkerDistanceText").GetComponent<Text>().text = "Distance: " + distance;
        }
    }
    private Quaternion GetRotation()
    {
        return Quaternion.FromToRotation(marker[0].transform.position, marker[1].transform.position);
    }


    void ProcessTouch()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Touch touch;
        if (Input.touchCount != 1 ||
            (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter =
            TrackableHitFlags.PlaneWithinBounds |
            TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if (marker.Count > 2)
            {
                spanwRoom.spawnARoom(hit, marker[0].transform.position, GetRotation());
            }
            else
            {
                SpawnMarker(hit.Trackable as DetectedPlane);
            }
        }
    }

    private void SpawnMarker(DetectedPlane detectedPlane)
    {
        if (marker.Count <= 2)
        {
            var localMarker = Instantiate(markerPrefab, detectedPlane.CenterPose.position, Quaternion.identity, this.transform);

            if (anchor == null)
            {
                anchor = detectedPlane.CreateAnchor(Pose.identity);
            }
            localMarker.transform.SetParent(anchor.transform);
            marker.Add(localMarker);
        }
    }

    private void RaycastCenter()
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;

        // If it hits an ARCore plane, move the pointer to that location.
        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {
            CenterMarkerOnScreen(hit);
            RotateToCamera();
        }
    }

    private void CenterMarkerOnScreen(TrackableHit hit)
    {
        Vector3 hitPoint = hit.Pose.position;
        var activeMarker = marker[marker.Count - 1];
        activeMarker.transform.position = new Vector3(hitPoint.x, activeMarker.transform.position.y, hitPoint.z);
    }

    private void RotateToCamera()
    {
        var activeMarker = marker[marker.Count - 1];
        Vector3 cameraPlanePostion = new Vector3(firstPersonCamera.transform.position.x, activeMarker.transform.position.y, firstPersonCamera.transform.position.z);

        activeMarker.transform.LookAt(cameraPlanePostion);
    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }

}
