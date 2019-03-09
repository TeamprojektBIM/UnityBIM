using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SpawnRoom : MonoBehaviour
{
    private float yOffset;
    private DataContainer cont;

    private bool ready = false;

    public void spawnARoom( Transform spawnPoint)
    {
        cont = GetComponent<DataContainer>();

        // The tracking state must be FrameTrackingState.Tracking
        // in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        createAnchor(spawnPoint);
    }

    private void createAnchor( Transform spawnPoint)
    {

        // Create the anchor at that point.
        if (cont.GetAnchor() != null)
        {
            DestroyObject(cont.GetAnchor());
        }
        cont.setAnchor(cont.GetDetectedPlane().CreateAnchor(
            new Pose(spawnPoint.position, Quaternion.identity)));

        Debug.Log("peace");
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

        cont.getSpawnedRoom().transform.position = spawnPoint.position;
        cont.getSpawnedRoom().transform.SetParent(cont.GetAnchor().transform);
    }
}
