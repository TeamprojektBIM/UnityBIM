using UnityEngine;
using GoogleARCore;

public class DanielsSceneController : MonoBehaviour
{

    //<----- Error Updates ----->//
    // void Start()
    // {
    //     if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
    //     {
    //         StartCoroutine(CodelabUtils.ToastAndExit(
    //             "Camera permission is needed to run this application.", 5));
    //     }
    //     else if (Session.Status.IsError())
    //     {
    //         StartCoroutine(CodelabUtils.ToastAndExit(
    //             "ARCore encountered a problem connecting. Please restart the app.", 5));
    //     }
    // }

    // //<-----     ----->//

    // public void Update()
    // {
    //     // The session status must be Tracking in order to access the Frame.
    //     if (Session.Status != SessionStatus.Tracking)
    //     {
    //         int lostTrackingSleepTimeout = 15;
    //         Screen.sleepTimeout = lostTrackingSleepTimeout;
    //         return;
    //     }
    //     Screen.sleepTimeout = SleepTimeout.NeverSleep;

    //     processTouch();
    // }

    // //If User touches screen, set the detected Plane in DataContainer
    // private void processTouch()
    // {
    //     Touch touch;
    //     if (Input.touchCount != 1 ||
    //         (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
    //     {
    //         return;
    //     }

    //     TrackableHit hit;
    //     TrackableHitFlags raycastFilter =
    //         TrackableHitFlags.PlaneWithinBounds |
    //         TrackableHitFlags.PlaneWithinPolygon;

    //     if (Frame.Raycast(Screen.width/2, Screen.height/2, raycastFilter, out hit))
    //     {

    //     }
    // }

}
