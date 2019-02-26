using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class GameController : MonoBehaviour
{
    public Camera firstPersonCamera;
    public bool playGame = false;
    public ScoreboardController scoreboard;
    public SnakeController snakeController;
    private DetectedPlane selectedPlane;

    void Start()
    {
        QuitOnConnectionErrors();
    }

    void QuitOnConnectionErrors()
    {
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

        ProcessTouches();

        if (playGame) { 
            scoreboard.SetScore(snakeController.GetLength());
        }
    }

    public DetectedPlane GetPlane()
    {
        return selectedPlane;
    }

    public void ProcessTouches()
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
            SetSelectedPlane(hit.Trackable as DetectedPlane);
            selectedPlane = hit.Trackable as DetectedPlane;
        }
    }

    public void SetSelectedPlane(DetectedPlane selectedPlane)
    {
        Debug.Log("Selected plane centered at " + selectedPlane.CenterPose.position);
        if (playGame)
        {
            scoreboard.SetSelectedPlane(selectedPlane);
            snakeController.SetPlane(selectedPlane);
            GetComponent<FoodController>().SetSelectedPlane(selectedPlane);
        }
    }
}