using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{

    private bool down = true;
    private RectTransform scanner = null;

    // Start is called before the first frame update
    void Start()
    {
        scanner = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {


        var minY = scanner.anchorMin.y;
        var maxY = scanner.anchorMax.y;

        Debug.Log("miny" + minY);
        Debug.Log("maxy" + maxY);

        if (minY <= 0)
        {
            down = false;
        }
        else if (maxY >= 1)
        {
            down = true;
        }

        float newMinY = minY;
        float newMaxY = maxY;

        if (down)
        {
            newMinY -= 0.01f;
            newMaxY -= 0.01f;
        }
        else
        {
            newMinY += 0.01f;
            newMaxY += 0.01f;
        }

        scanner.anchorMin = new Vector2(0, newMinY);
        scanner.anchorMax = new Vector2(1, newMaxY);

        scanner.offsetMin = new Vector2(0.0f, 0.0f);
        scanner.offsetMax = new Vector2(0.0f, 0.0f);


    }
}
