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
    public void spawnARoom(TrackableHit spawnPoint, Transform markerTransform, Quaternion rotation)
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
            createAnchor(spawnPoint, markerTransform, rotation);
        }
    }

    private void createAnchor(TrackableHit spawnPoint, Transform markerTransform, Quaternion rotation)
    {
        anchor = (detectedPlane.CreateAnchor(
           new Pose(spawnPoint.Pose.position, markerTransform.rotation)));

        if (room != null)
        {
            DestroyImmediate(room);
        }

        Vector3 spawnPos = markerTransform.position;

        room = Instantiate(roomPrefab, spawnPos, rotation);

        var size2 = GameObject.Find("Bodenplatte 15.0 Stahlbeton [406048]").GetComponent<Renderer>().bounds.size;
        Debug.Log("Size of Bodenplatte 15.0 Stahlbeton [406048]" + size2.ToString());

var door =GameObject.Find("Drehflügel 1-flg - Stahlzarge 1.137 x 2.4083 [462807]");
   var sizeDoor = door.GetComponent<Renderer>().bounds.size;
    Vector3 offset = door.transform.up * (door.transform.localScale.y / 2f) * -1f;
     Vector3 posBot = door.transform.position + offset;
      Vector3 posUp = door.transform.position + (offset * -1f);
        Debug.Log("Drehflügel 1-flg - Stahlzarge 1.137 x 2.4083 [462807]" + sizeDoor.ToString());
        Debug.Log("offset: "+ Vector3.Distance(posBot,posUp));
   
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
