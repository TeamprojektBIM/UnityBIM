using GoogleARCore;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class SetRoom : MonoBehaviour
{
    // Camera used for raycasting screen point.
    private Camera firstPersonCamera;

    private SpawnRoom spawnRoom;
    private GlobalDataContainer container;
    private Anchor anchor;

    //just for präs
    public GameObject ui;

    private bool roomSet = false;

    void Start()
    {
        FindDataContainer();
        firstPersonCamera = container.FirstPersonCamera;
        spawnRoom = GetComponent<SpawnRoom>();

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
        //RaycastCenter();
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
            if (!roomSet)
            {
                ui.SetActive(false);
                spawnRoom.spawnARoom(hit,new Vector3(), new Quaternion());
                roomSet = true;
            }
        }
    }

    /*
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
    */

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }

}
