using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 8, -16);
    }
    
    void Update()
    {
        transform.position = target.position + offset;

    }
}
