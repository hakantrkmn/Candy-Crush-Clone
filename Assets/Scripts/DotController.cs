using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public DotColors dotColor;

    public Vector3 mouseStartPos;
    
    
 
    private void OnMouseDown()
    {
        Debug.Log(GetComponent<BoxCollider2D>().size.x);
        mouseStartPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        var direction = mouseStartPos - Input.mousePosition;
        Debug.Log(direction.normalized);
    }
}
