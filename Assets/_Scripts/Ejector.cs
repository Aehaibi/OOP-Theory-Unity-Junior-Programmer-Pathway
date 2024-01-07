using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ejector : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 launchPower = new Vector3(0, 250, 0);
    public bool launchOnStart;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch(launchPower);
    }
    public void Launch(Vector2 launchPower)
    {
        //Launch collectable after box explosion at the specificied launch power, multiplied by a random range.
        rb.AddForce(new Vector3(0, launchPower.y * Random.Range(1f, 1.5f)), 0);
    }
}
