using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube currentCube {  get; private set; }
    public static MovingCube LastCube {  get; private set; }

    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        currentCube = this;
        
    }
    public void Stop()
    {
        moveSpeed = 0f;

        float breakZ = transform.position.z - LastCube.transform.position.z;

        float direction = breakZ > 0 ? 1f : -1f;
        SplitCubeOnZ(breakZ, direction);
    }

    private void SplitCubeOnZ(float breakZ, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Mathf.Abs(breakZ);
        float fallingBlockSize = transform.localScale.z - newSize;
        float newPosition = LastCube.transform.localPosition.z + (breakZ / 2);
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);

        float cubeEgde = transform.position.z + (newSize / 2 * direction);
        float fallingBlockZPos = cubeEgde + fallingBlockSize / 2 * direction;

        DropCube(fallingBlockZPos, fallingBlockSize);
    }

    private void DropCube(float fallingBlockZPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
        
        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 2);
    
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
