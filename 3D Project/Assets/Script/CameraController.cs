using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras;
    void Start()
    {
        if(cameras == null || cameras.Count == 0)
        {
            Debug.LogError("Kamera bulunamadý!");
            return;
        }
        ActiveCamera(0);
    }

    void Update()
    {
        for (int i = 0; i < cameras.Count; i++) {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ActiveCamera(i);
            }
        }
    }

    private void ActiveCamera(int activeCamera)
    {
        if(activeCamera < 0 || activeCamera >= cameras.Count)
        {
            Debug.LogError("Validasyon hatasý!");
            return;
        }

        for (int i =0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == activeCamera);
        }
    }
}
