using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (!GameController.youWin)
        {
            transform.Rotate(0, 0, 90f);
        }
    }
}
