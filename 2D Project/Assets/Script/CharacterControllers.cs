using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    public GameObject Player1, Player2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {

            if (VirtualCamera.Follow == Player1.transform)
            {
                VirtualCamera.Follow = Player2.transform;
            }
            else if (VirtualCamera.Follow == Player2.transform){
                VirtualCamera.Follow = Player1.transform;
            }
        }        
    }
}
