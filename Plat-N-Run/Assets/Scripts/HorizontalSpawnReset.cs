using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSpawnReset : MonoBehaviour
{
    [Tooltip("Starting pos for the vertical wall spawnpoint")]
    public Vector3 horStartingPos;
    [Tooltip("Checks to see whether or not the wall we are spawning is colliding with something else")]
    public bool colliding;
    [Tooltip("Stores previous position to not collide into walls")]
    public Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        horStartingPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

    }

    public void FixedUpdate()
    {
        if (!colliding)
        {
            prevPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = horStartingPos;
        }
        else
        {
            colliding = false;
            
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "VerticalWall" 
            &&other.tag != "HorWall")
        {
            transform.localPosition = prevPos;
            colliding = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag != "VerticalWall" && other.tag != "HorWall")
        {
            transform.localPosition = prevPos;
            colliding = true;
        }
    }
}
