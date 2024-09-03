using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Dokunma baþladý!");
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Parmak Hareket Ettiriliyor.");
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("Dokunma sabit!");
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Dokunma Bitti!");
                    break;
            }

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            if(touch.position.x < screenWidth / 2)
            {
                Debug.Log("Ekranýn sol tarafýna dokunuyorsun");
            }
            else
            {
                Debug.Log("Ekranýn sað tarafýna dokunuyorsun");
            }

            if(touch.position.y < screenHeight / 2)
            {
                Debug.Log("Ekranýn alt tarafýna dokunuyorsun");
            }
            else
            {
                Debug.Log("Ekranýn üst tarafýna dokunuyorsun");
            }
        }
    }
}
