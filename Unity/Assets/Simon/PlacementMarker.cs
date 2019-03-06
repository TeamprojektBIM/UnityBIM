using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementMarker : MonoBehaviour
{
    private DetectedPlane detectedPlane;

    // Camera used for raycasting screen point.
    public Camera firstPersonCamera;


    // Update is called once per frame
    void Update()
    {
        RaycastCenter();


    }

    private void RaycastCenter()
    {
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;

        // If it hits an ARCore plane, move the pointer to that location.
        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {
            PlaceOnPlane(hit);

            RotateToCamera();

        }
    }

    private void PlaceOnPlane(TrackableHit hit)
    {
        Vector3 pt = hit.Pose.position;
        Vector3 hitPoint = hit.Pose.position;

        transform.position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
    }

    private void RotateToCamera()
    {
        Vector3 cameraPlanePostion = new Vector3(firstPersonCamera.transform.position.x, transform.position.y, firstPersonCamera.transform.position.z);
        transform.LookAt(cameraPlanePostion);
        transform.Rotate(new Vector3(1,0,0),90);
    }

    public void SetPlane(DetectedPlane plane)
    {
        detectedPlane = plane;
        //initialize at plane
        transform.position = plane.CenterPose.position;
        //make plane Parent ?
    }


}
