using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    private DetectedPlane detectedPlane;

    public GameObject snakeHeadPrefab;
    private GameObject snakeInstance;


    public GameObject pointer;
    public Camera firstPersonCamera;
    // Speed to move.
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (snakeInstance == null || snakeInstance.activeSelf == false)
        {
            pointer.SetActive(false);
            return;
        }
        else
        {
            pointer.SetActive(true);
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinBounds;

        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {
            Vector3 pt = hit.Pose.position;
            //Set the Y to the Y of the snakeInstance
            pt.y = snakeInstance.transform.position.y;
            // Set the y position relative to the plane and attach the pointer to the plane
            Vector3 pos = pointer.transform.position;
            pos.y = pt.y;
            pointer.transform.position = pos;

            // Now lerp to the position                                         
            pointer.transform.position = Vector3.Lerp(pointer.transform.position, pt,
              Time.smoothDeltaTime * speed);
        }

        // Move towards the pointer, slow down if very close.                                                                                     
        float dist = Vector3.Distance(pointer.transform.position,
            snakeInstance.transform.position) - 0.05f;
        if (dist < 0)
        {
            dist = 0;
        }

        Rigidbody rb = snakeInstance.GetComponent<Rigidbody>();
        rb.transform.LookAt(pointer.transform.position);
        rb.velocity = snakeInstance.transform.localScale.x *
            snakeInstance.transform.forward * dist / .01f;

    }

    public void SetPlane(DetectedPlane plane)
    {
        detectedPlane = plane;
        // Spawn a new snake.
        SpawnSnake();
    }

    void SpawnSnake()
    {

        if (snakeInstance != null)
        {
            DestroyImmediate(snakeInstance);
        }

        Vector3 pos = detectedPlane.CenterPose.position;

        // Not anchored, it is rigidbody that is influenced by the physics engine.
        snakeInstance = Instantiate(snakeHeadPrefab, pos,
                Quaternion.identity, transform);

        snakeInstance.AddComponent<FoodConsumer>();
        // Pass the head to the slithering component to make movement work.
        GetComponent<Slithering>().Head = snakeInstance.transform;
    }

    public int GetLength()
    {
        return GetComponent<Slithering>().GetLength();
    }
}
