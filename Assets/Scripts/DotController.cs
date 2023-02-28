using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public DotColors dotColor;

    public Vector2 mouseStartPos;


    private void Start()
    {
        GetComponent<Canvas>().overrideSorting = true;
    }

    private void OnMouseDown()
    {
        mouseStartPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        var direction = (Vector2)Input.mousePosition - mouseStartPos;
        direction = direction.normalized;

        if (direction.x > .5f)
        {
            direction.x = 1;
        }
        else if (direction.x < -.5f)
        {
            direction.x = -1;
        }
        else
        {
            direction.x = 0;
        }

        if (direction.y > .5f)
        {
            direction.y = 1;
        }
        else if (direction.y < -.5f)
        {
            direction.y = -1;
        }
        else
        {
            direction.y = 0;
        }

        Debug.Log(direction);

        SwipeAction(direction);
    }

    public void SwipeAction(Vector2 direction)
    {
        SwipeDirections swipeDir;
        if (direction.x == 0 && direction.y == 1)
        {
            swipeDir = SwipeDirections.Up;
            transform.parent.GetComponent<TileController>().SwipeAction(swipeDir);

        }
        else if (direction.x == 1 && direction.y == 0)
        {
            swipeDir = SwipeDirections.Right;
            transform.parent.GetComponent<TileController>().SwipeAction(swipeDir);

        }
        else if (direction.x == -1 && direction.y == 0)
        {
            swipeDir = SwipeDirections.Left;
            transform.parent.GetComponent<TileController>().SwipeAction(swipeDir);

        }
        else if (direction.x == 0 && direction.y == -1)
        {
            swipeDir = SwipeDirections.Down;
            transform.parent.GetComponent<TileController>().SwipeAction(swipeDir);

        }
    }
}