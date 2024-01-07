using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private ParticleSystem crateBreakFX;
    public GameObject pickUp;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            crateBreakFX.Play();
            crateBreakFX.transform.parent = null;
            Destroy(gameObject);
            Instantiate(pickUp, transform.position, transform.rotation);
        }
    }

}
