using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRotation : MonoBehaviour
{

    public float degreesPerSecondX = 0;
    public float degreesPerSecondY = 0;
    public float degreesPerSecondZ = 0;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    //ABSTRACTION
    public virtual void Rotate()
    {
        rb.AddTorque(transform.up * degreesPerSecondY);
    }
}
