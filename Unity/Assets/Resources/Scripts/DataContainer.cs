using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class DataContainer : MonoBehaviour
{
    public Camera firstPersonCamera;
    public GameObject roomPrefab;

    private DetectedPlane detectedPlane;
    private Anchor roomAnchor;
    private GameObject spawnedRoom;

    public void setDetectedPlane(DetectedPlane detectedPlane)
    {
        this.detectedPlane = detectedPlane;
    }

    public DetectedPlane GetDetectedPlane()
    {
        return detectedPlane;
    }

    public void setAnchor(Anchor anchor)
    {
        this.roomAnchor = anchor;
    }

    public Anchor GetAnchor()
    {
        return roomAnchor;
    }

    public void setSpawnedRoom(GameObject obj)
    {
        this.spawnedRoom = obj;
    }

    public GameObject getSpawnedRoom()
    {
        return spawnedRoom;
    }
}
