using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RepeatBackgroundMovement : MonoBehaviour
{
    public float speed;

    private Vector3 startPos;
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        StartData();
    }

    // Update is called once per frame
    void Update()
    {
        RepeatBackground();
    }

    //ABSTRACTION
    public void RepeatBackground()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }

    public void StartData() 
    {

        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x * 20;
    }
}
