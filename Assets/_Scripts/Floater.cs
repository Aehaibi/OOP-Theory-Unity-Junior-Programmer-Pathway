using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float degreesPerSecondZ;
    public float xAmplitude;
    public float xFrequency;
    public float yAmplitude;
    public float yFrequency;
    public float zAmplitude;
    public float zFrequency;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * degreesPerSecondZ), Space.World);

        tempPos = posOffset;
        tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * xFrequency) * xAmplitude;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * yFrequency) * yAmplitude;
        tempPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * zFrequency) * zAmplitude;

        transform.position = tempPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
