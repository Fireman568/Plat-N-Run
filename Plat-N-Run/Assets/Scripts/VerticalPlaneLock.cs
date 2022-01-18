using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlaneLock : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Reference to the vertical plane prefab")]
    public Transform verticalPlane;
    [Tooltip("Reference to the player to grab its rotation")]
    public Transform playerRotation;
    [Tooltip("Reference to the spawn point so the platform can take its transform position")]
    public Transform spawnPoint;
    [Tooltip("Checks to see whether or not the wall we are spawning is colliding with something else")]
    public bool colliding;
    [Tooltip("Stores previous position to not collide into walls")]
    public Vector3 prevPos;

    bool placed;
    float mouseX;
    public float deathTime;


    public Quaternion startRotation;

    public void Start()
    {
        playerRotation = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
        startRotation = playerRotation.rotation;
        
    }
    public void Update()
    {
        verticalPlane.rotation = startRotation;
        //mouseX = Input.GetAxisRaw("Mouse X");
        if (Input.GetMouseButtonUp(0))
        {
            transform.parent = null;
            placed = true;
        }

        if (placed)
        {
            Destroy(gameObject, deathTime);
        }
        
        
    }
    //public void FixedUpdate()
    //{
    //    if (!colliding)
    //    {
    //        prevPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    }
    //    else
    //    {
    //        colliding = false;
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    transform.position = prevPos;
    //    colliding = true;
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    transform.position = prevPos;
    //    colliding = true;
    //}
}
