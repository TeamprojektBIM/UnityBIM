using GoogleARCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif
public class PlacementMarker : MonoBehaviour
{


    // Camera used for raycasting screen point.
    public Camera firstPersonCamera;
    
    public GameObject markerPrefab;

    private SpawnRoom spanwRoom;
    private GameObject marker;

    void Start()
    {
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
        if (marker != null)
        {
            RaycastCenter();
        }


    }

    void ProcessTouch()
    {
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
            if (marker != null)
            {
                Debug.Log(spanwRoom);
                spanwRoom.spawnARoom(hit);
            }
            else
            {
                SpawnMarker(hit.Trackable as DetectedPlane);
            }

        }
    }

    private void SpawnMarker(DetectedPlane detectedPlane)
    {
        marker = Instantiate(markerPrefab, detectedPlane.CenterPose.position, Quaternion.identity);
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

        marker.transform.position = new Vector3(hitPoint.x, marker.transform.position.y, hitPoint.z);
    }

    private void RotateToCamera()
    {
        Vector3 cameraPlanePostion = new Vector3(firstPersonCamera.transform.position.x, marker.transform.position.y, firstPersonCamera.transform.position.z);
        marker.transform.LookAt(cameraPlanePostion);
    }


}
