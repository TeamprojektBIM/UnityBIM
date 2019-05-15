using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const string GlobalDataContainer = "GlobalDataContainer";

    public static readonly GlobalDataContainer dataContainer = FindDataContainer();
    private static GlobalDataContainer FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(GlobalDataContainer);
        return containerObject.GetComponent<GlobalDataContainer>();
    }
}
