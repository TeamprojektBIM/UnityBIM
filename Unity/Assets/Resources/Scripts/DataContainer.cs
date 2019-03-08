using UnityEngine;
using GoogleARCore;

public class DataContainer : MonoBehaviour
{
    public Camera firstPersonCamera;
    public GameObject roomPrefab;
    public GameObject testObject;

    private DetectedPlane detectedPlane;
    private Anchor roomAnchor;
    private GameObject spawnedRoom;

    public void setDetectedPlane(DetectedPlane detectedPlane, TrackableHit hit)
    {
        this.detectedPlane = detectedPlane;
        Instantiate(testObject, hit.Pose.position, Quaternion.identity);
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
