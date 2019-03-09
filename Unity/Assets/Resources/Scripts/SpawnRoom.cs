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
    public void spawnARoom(TrackableHit spawnPoint)
    {
        // cont = GetComponent<DataContainer>();



        // The tracking state must be FrameTrackingState.Tracking
        // in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }


        detectedPlane = spawnPoint.Trackable as DetectedPlane;
        createAnchor(spawnPoint);
    }

    private void createAnchor(TrackableHit spawnPoint)
    {


        anchor = (detectedPlane.CreateAnchor(
           new Pose(spawnPoint.Pose.position, Quaternion.identity)));

        // Record the y offset from the plane.
        // yOffset = transform.position.y - detectedPlane.CenterPose.position.y;

        if (room != null)
        {
            DestroyImmediate(room);
        }

        Vector3 spawnPos = spawnPoint.Pose.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        room = Instantiate(roomPrefab, spawnPos,
               Quaternion.identity);


        room.transform.position = spawnPoint.Pose.position;
        room.transform.SetParent(anchor.transform);
    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }
}
