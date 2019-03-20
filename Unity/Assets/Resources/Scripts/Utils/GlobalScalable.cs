//-----------------------------------------------------------------------
//// <copyright file="GlobalScalable.cs" company="Google">
/////
///// Copyright 2017 Google Inc. All Rights Reserved.
/////
///// Licensed under the Apache License, Version 2.0 (the "License");
///// you may not use this file except in compliance with the License.
///// You may obtain a copy of the License at
/////
///// http://www.apache.org/licenses/LICENSE-2.0
/////
///// Unless required by applicable law or agreed to in writing, software
///// distributed under the License is distributed on an "AS IS" BASIS,
///// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
///// See the License for the specific language governing permissions and
///// limitations under the License.
/////
///// </copyright>
/////-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component used to allow stretching or pinching to scale objects.
/// </summary>
/// <remarks>
/// This class has a static scale that affects all objects that
/// have this component enabled and the <code>adjustScale</code>
/// property is true.
/// <para>
/// The <code>handleScaleInput</code> property indicates that this instance
/// of the component should handle the touch events to update the scale factor.
/// Typically this is only enabled for on object per scene.
/// </para>
/// </remarks>
public class GlobalScalable : MonoBehaviour
{

    private Vector3 originalScale;
    static private float scaleFactor = 1.0f;
    public bool handleScaleInput = false;
    public bool adjustScale = true;
    private float speed = 1f;

    // Use this for initialization
    void Start()
    {
        // Keep track of the original scale of the object.
        originalScale = transform.localScale;
    }

    void Update()
    {
        // If handling input, look for the two finger stretch/pinch gesture.
        if (handleScaleInput)
        {
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos =
                    touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos =
                    touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance)
                // between the touches in each frame.
                float prevTouchDeltaMag =
                    (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag =
                    (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // range is 1 to 1000.
                // The scale factor is v/100f meaning scale from 0.01 to 10.
                float v = scaleFactor * 100f;

                // negative delta is stretching, positive is pinching
                v -= deltaMagnitudeDiff * speed;

                scaleFactor = Mathf.Clamp(v, 1f, 1000f) / 100f;
            }
        }

        // Adjust the scale of the object.
        if (adjustScale)
        {
            transform.localScale = originalScale * scaleFactor;
        }
    }
}
