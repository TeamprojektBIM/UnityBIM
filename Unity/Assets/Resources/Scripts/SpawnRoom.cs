﻿using System.Collections;
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
    public void spawnARoom(TrackableHit spawnPoint, Vector3 position, Quaternion rotation)
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
            createAnchor(spawnPoint, position, rotation);
        }
    }

    private void createAnchor(TrackableHit spawnPoint, Vector3 position, Quaternion rotation)
    {
        anchor = (detectedPlane.CreateAnchor(
           new Pose(spawnPoint.Pose.position,rotation)));

        if (room != null)
        {
            DestroyImmediate(room);
        }

        Vector3 negativeAmount = roomPrefab.transform.Find("SpawnPoint").position;
        float distance = Vector3.Distance(roomPrefab.transform.Find("SpawnPoint").position, roomPrefab.transform.Find("FloorForMeasurement").position);
        Debug.Log(distance);

        room = Instantiate(roomPrefab, position-negativeAmount, rotation);
        room.transform.SetParent(anchor.transform);

        /*
        GameObject marker = GameObject.Find("PlacementMarkerController");
        marker.SetActive(false);
        */
        GameObject planeGenerator = GameObject.Find("Plane Generator");
        planeGenerator.SetActive(false);
    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }
}
