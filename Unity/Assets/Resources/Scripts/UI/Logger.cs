using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{

    private static GameObject textField;
    // Start is called before the first frame update
    void Start()
    {
        textField = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void log(string text){
        if (textField != null){
            textField.GetComponent<Text>().text = text;
        }
    }
}
