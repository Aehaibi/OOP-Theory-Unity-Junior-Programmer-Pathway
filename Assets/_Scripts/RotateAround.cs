using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject target;
    public float speed = 20;
    public Vector3 axis;
    void Update()
    {
        transform.RotateAround(target.transform.position, axis, speed * Time.deltaTime);
    }
}
