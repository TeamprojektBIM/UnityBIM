using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SetAnchorOnItemPosition : MonoBehaviour
{
    private DetectedPlane detectedPlane;
    private Anchor anchor;
    public SceneController controller;
    public Camera firstPersonCamera;
    public GameObject prefab;
    private GameObject spawnInstance;
    private float yOffset;

    public void Update()
    {
        detectedPlane = controller.GetPlane();
        CreateAnchor();
        SpawnItem();

        if(detectedPlane == null)
        {
            return;
        }
    }

    private void CreateAnchor()
    {
        // Create the position of the anchor by raycasting a point towards
        // the top of the screen.
        Vector2 pos = new Vector2(Screen.width * .5f, Screen.height * .90f);
        Ray ray = firstPersonCamera.ScreenPointToRay(pos);
        Vector3 anchorPosition = ray.GetPoint(5f);

        // Create the anchor at that point.
        if (anchor != null)
        {
            DestroyObject(anchor);
        }
        anchor = detectedPlane.CreateAnchor(
            new Pose(anchorPosition, Quaternion.identity));

        // Attach the scoreboard to the anchor.
        transform.position = anchorPosition;
        transform.SetParent(anchor.transform);

        // Record the y offset from the plane.
        yOffset = transform.position.y - detectedPlane.CenterPose.position.y;

        // Finally, enable the renderers.
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    public void SpawnItem()
    {
        if (spawnInstance != null)
        {
            DestroyImmediate(spawnInstance);
        }

        Vector3 pos = detectedPlane.CenterPose.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        spawnInstance = Instantiate(prefab, pos,
                Quaternion.identity, transform);
    }
}
