using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejector : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 launchPower = new Vector3(0, 250, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch(launchPower);
        Physics.IgnoreLayerCollision(0,1);
    }
    public void Launch(Vector2 launchPower)
    {
        rb.AddForce(new Vector3(0, launchPower.y * Random.Range(1f, 1.25f)), 0);
    }
}
