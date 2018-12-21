using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterScript : MonoBehaviour
{
    //Get the actual camera to later set the objects to render
    public Camera cam;

    //Get all layer with stuff on it
    public List<string> layers = new List<string>();

    //List filled with the layerIDs
    private List<int> layerIDs = new List<int>();

    //Activate layer
    private  int activeLayer = 0;

    //Start-Method, for every layer given from user is a id set in second List
    void Start()
    {
        foreach(string layer in layers)
        {
            layerIDs.Add(LayerMask.NameToLayer(layer));
        }

        activeLayer = layerIDs[0] - 1;

        onTap();
    }

    //Update and test for taps
    void Update()
    {
        if(Input.touchCount == 1)
        {
            onTap();
        }
    }

    //If players tap on Screen
    private void onTap()
    {
        closeAllMasks();
        activateNextLayer();
    }

    //Activate *one* layer
    private void activateNextLayer()
    {
        if (activeLayer != layerIDs[layerIDs.Count - 1])
        {
            cam.cullingMask |= 1 << activeLayer + 1;
            activeLayer += 1;
        } else
        {
            activeLayer = layerIDs[0] - 1;
            cam.cullingMask |= 1 << activeLayer + 1;
            activeLayer += 1;
        }
    }

    //Hides all Layers
    private void closeAllMasks()
    {
        foreach(int ID in layerIDs)
        {
            cam.cullingMask &= ~(1 << ID);
        }
    }
}
