using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalWallLock : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the horizontal plane prefab")]
    public Transform horPlaneRotation;
    [Tooltip("Reference to the player to grab its rotation")]
    public Transform playerRotation;
    [Tooltip("Reference to the spawn point")]
    public Transform spawnPoint;
    [Tooltip("Checks to see whether or not the wall we are spawning is colliding with something else")]
    public bool colliding;
    [Tooltip("Stores previous position to not collide into walls")]
    public Vector3 prevPos;

    public bool placed = false;
    Quaternion startRotation;
    public float deathTime;
    // Start is called before the first frame update
    void Start()
    {

        playerRotation = GameObject.FindGameObjectWithTag("Player").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn").transform;
        startRotation = playerRotation.rotation;
        //horPlaneRotation.rotation = startRotation;
         
    }

    // Update is called once per frame
    void Update()
    {
        horPlaneRotation.rotation = playerRotation.rotation;
        
        if (Input.GetMouseButtonUp(1))
        {
            transform.parent = null;
            placed = true;
        }
        if(transform.parent == null)
        {
            
            horPlaneRotation.rotation =  Quaternion.Euler(horPlaneRotation.rotation.x, startRotation.eulerAngles.y, horPlaneRotation.rotation.z);
        }
        else
        {
            startRotation = playerRotation.rotation;
            
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
