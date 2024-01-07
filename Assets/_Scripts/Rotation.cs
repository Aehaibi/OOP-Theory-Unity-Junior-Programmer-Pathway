using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float degreesPerSecondX = 0;
    public float degreesPerSecondY = 0;
    public float degreesPerSecondZ = 0;

    private void FixedUpdate()
    {
        Rotate();
    }

    //ABSTRACTION
    public virtual void Rotate() 
    {
        transform.Rotate(new Vector3(degreesPerSecondX, degreesPerSecondY, degreesPerSecondZ) * Time.deltaTime);
    }
}
