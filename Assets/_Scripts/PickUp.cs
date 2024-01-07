using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private BoxCollider collectibleCollider;
    [SerializeField] private BoxCollider physicsCollider;
    private PlayerController player;
    private float counter;
    void Awake()
    {
        collectibleCollider.enabled = false;
    }

    void Start()
    {
        Physics.IgnoreCollision(physicsCollider, player.GetComponent<Collider>());
    }

    void Update()
    {
        if (collectibleCollider != null && counter > 0.33f)
        {
            collectibleCollider.enabled = true;
        }
        else 
        {
            counter += Time.deltaTime;
        }
    }
}
