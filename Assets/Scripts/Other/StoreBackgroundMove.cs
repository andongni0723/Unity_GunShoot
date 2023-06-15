using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBackgroundMove : MonoBehaviour
{
    private RawImage rawImage => GetComponent<RawImage>();
    public float speed = 1;


    private void Update()
    {
        rawImage.uvRect = new Rect(new Vector2(0, rawImage.uvRect.y + speed * Time.deltaTime), rawImage.uvRect.size);
    }
}
