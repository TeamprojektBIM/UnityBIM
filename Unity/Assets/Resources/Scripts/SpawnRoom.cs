using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SpawnRoom : MonoBehaviour
{

    private DetectedPlane detectedPlane;
    private GameObject room;
    private Anchor anchor;
    public GameObject roomPrefab;
    private GlobalDataContainer container;


    public void Start()
    {
        FindDataContainer();
    }
    public void spawnARoom(TrackableHit spawnPoint, Transform markerTransform)
    {
        // The tracking state must be FrameTrackingState.Tracking
        // in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }


        detectedPlane = spawnPoint.Trackable as DetectedPlane;
        if (anchor == null)
        {
            createAnchor(spawnPoint, markerTransform);
        }
    }

    private void createAnchor(TrackableHit spawnPoint, Transform markerTransform)
    {


        anchor = (detectedPlane.CreateAnchor(
           new Pose(spawnPoint.Pose.position, markerTransform.rotation)));

        if (room != null)
        {
            DestroyImmediate(room);
        }

        Vector3 spawnPos = markerTransform.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.

        room = Instantiate(roomPrefab, spawnPos, markerTransform.rotation);

        // Vector3 scale = room.transform.localScale;
        // scale += new Vector3(1000, 1000, 1000);
        // room.transform.localScale = scale;
        room.transform.position = markerTransform.position;
        room.transform.SetParent(anchor.transform);

        GameObject marker = GameObject.Find("PlacementMarkerController");
        marker.SetActive(false);
        GameObject planeGenerator = GameObject.Find("Plane Generator");
        planeGenerator.SetActive(false);

    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }
}
