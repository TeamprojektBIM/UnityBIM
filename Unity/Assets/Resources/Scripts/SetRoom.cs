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

    private GameObject marker;

    //just for präs
    public GameObject ui;
    // public GameObject camera;

    public GameObject markerPrefab;



    private bool roomSet = false;
    private bool markerSet = false;

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
        if (!markerSet)
        {
            RaycastCenter();
        }
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
                if (marker == null)
                {
                    SpawnMarker(hit.Trackable as DetectedPlane);
                }
                else
                {
                    markerSet = true;
                }

                if (!roomSet && markerSet == true)
                {

                    if(touch.position.x > Screen.width/2 && touch.position.y > Screen.height/4){
                        rotateMarker(2);
                    }
                    else if(touch.position.x < Screen.width/2 && touch.position.y > Screen.height/4){
                        rotateMarker(-2);
                    } else {
                        ui.SetActive(false);
                        spawnRoom.spawnARoom(hit, marker.transform.position, marker.transform.rotation);
                        roomSet = true;
                    }

                    
                }
            }
        }


        private void SpawnMarker(DetectedPlane detectedPlane)
        {
            marker = Instantiate(markerPrefab, detectedPlane.CenterPose.position, Quaternion.identity, this.transform);

            if (anchor == null)
            {
                anchor = detectedPlane.CreateAnchor(Pose.identity);
            }
            marker.transform.SetParent(anchor.transform);
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

        private void rotateMarker(float angle)
        {
            marker.transform.Rotate(0, angle, 0);
        }


        private void FindDataContainer()
        {
            GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
            container = containerObject.GetComponent<GlobalDataContainer>();
        }

    }
