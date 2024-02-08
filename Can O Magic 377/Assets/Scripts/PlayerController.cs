using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //How far the touch for dragging goes to the right
    private int rightDis = 5;
    //How far the touch for dragging goes to the left
    private int leftDis = -5;
    //Locks the height while dragging object
    private int heightDis = 1000;


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            if (touchedPos.x <= rightDis && touchedPos.x >= leftDis)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 lockedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, heightDis, 10));
                    transform.position = lockedPos;
                }
                if (touch.phase == TouchPhase.Ended && touchedPos.x <= rightDis && touchedPos.x >= leftDis)
                {
                    //Place something that triggers the object to fall here.
                }
            }
        }
    }
}
