using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private BoxCollider collectibleCollider;
    [SerializeField] private BoxCollider physicsCollider;
    private float counter = 0.33f;
    private float m_MaxCount;
    //ENCAPSULATION
    public float maxCount
    {
        get { return m_MaxCount; }
        set 
        {
            if (value < 0.0f)
            {
                Debug.Log("Negative time values not allowed.");
            }
            else
            {
                m_MaxCount = value;
            }
        }
    }
    void Awake()
    {
        collectibleCollider.enabled = false;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(physicsCollider, player.GetComponent<Collider>(), true);
    }

    void Update()
    {
        if (collectibleCollider != null && counter > maxCount)
        {
            collectibleCollider.enabled = true;
        }
        else 
        {
            counter += Time.deltaTime;
        }
    }
}
