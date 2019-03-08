using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SpawnRoom : MonoBehaviour
{
    public GameObject testObject;

    private float yOffset;
    private DataContainer cont;
    private bool planeSet = false;

    IEnumerator testForChangeOfPlane()
    {
        while(cont.GetDetectedPlane().Equals(null)){ }
        yield return null;
        planeSet = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<DataContainer>();
        StartCoroutine(testForChangeOfPlane());
        Instantiate(testObject, new Vector3(),
                Quaternion.identity, transform);
    }

    void Update()
    {
        // The tracking state must be FrameTrackingState.Tracking
        // in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // If there is no plane, then return
        if (cont.GetDetectedPlane() == null)
        {
            return;
        }

        // Check for the plane being subsumed.
        // If the plane has been subsumed switch attachment to the subsuming plane.
        while (cont.GetDetectedPlane().SubsumedBy != null)
        {
            DetectedPlane plane = cont.GetDetectedPlane();
            cont.setDetectedPlane(plane.SubsumedBy);
        }

        if (cont.getSpawnedRoom() != null)
        {
            // Move the position to stay consistent with the plane.
            cont.getSpawnedRoom().transform.position = new Vector3(transform.position.x,
                        cont.GetDetectedPlane().CenterPose.position.y + yOffset, transform.position.z);
        }

        if (planeSet)
        {
            createAnchor();
        }
    }

    private void createAnchor()
    {
        // Create the position of the anchor by raycasting a point towards the screen.
        Vector2 pos = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = cont.firstPersonCamera.ScreenPointToRay(pos);
        Vector3 anchorPosition = ray.GetPoint(5f);

        // Create the anchor at that point.
        if (cont.GetAnchor() != null)
        {
            DestroyObject(cont.GetAnchor());
        }
        cont.setAnchor(cont.GetDetectedPlane().CreateAnchor(
            new Pose(anchorPosition, Quaternion.identity)));

        // Record the y offset from the plane.
        yOffset = transform.position.y - cont.GetDetectedPlane().CenterPose.position.y;

        if (cont.getSpawnedRoom() != null)
        {
            DestroyImmediate(cont.getSpawnedRoom());
        }

        Vector3 spawnPos = cont.GetDetectedPlane().CenterPose.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        GameObject room = Instantiate(cont.roomPrefab, spawnPos,
                Quaternion.identity, transform);
        cont.setSpawnedRoom(room);
        
        cont.getSpawnedRoom().transform.position = anchorPosition;
        cont.getSpawnedRoom().transform.SetParent(cont.GetAnchor().transform);
    }


}
