using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAI : MonoBehaviour
{
    public bool playerDetected = false;
    public bool needToSwitch= false;
    public bool goingToDestination = false;
    public float movementSpeed;
    public float timeSinceLastDetected;
    public float timeBeforeGoingBack;
    public float timeSinceLastHit;
    public float hitRate;
    public int lastDestinationGoneTo;
    public int destinationToGoTo;
    public Vector3 initialPosition;
    public GameObject playerObj;
    public GameObject destination1;
    public GameObject destination2;
    public GameObject destination3;
    public GameObject destination4;
    public GameObject destination5;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        needToSwitch = true;
        Debug.Log("Destination 1: " + destination1.transform.position);
        Debug.Log("Destination 2: " + destination2.transform.position);
        Debug.Log("Destination 3: " + destination3.transform.position);
        Debug.Log("Destination 4: " + destination4.transform.position);
        Debug.Log("Destination 5: " + destination5.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            timeSinceLastDetected = 0f;
            //playerObj = GameObject.Find("Player");
            transform.LookAt(playerObj.transform);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            //needToGoBack = true;
        }
        //else if(needToGoBack)
        //{
        //    timeSinceLastDetected += Time.deltaTime;
        //    if (timeSinceLastDetected >= timeBeforeGoingBack)
        //    {
        //        transform.LookAt(initialPosition);
        //        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        //        if (transform.position == initialPosition)
        //        {
        //            timeSinceLastDetected = 0f;
        //            needToGoBack = false;
        //        }
        //    }
        //}
        //else
        //{
        //    if (needToSwitch)
        //    {
        //        destinationToGoTo = Random.Range(1, 6);
        //    }
        //    if (destinationToGoTo != lastDestinationGoneTo)
        //    {
        //        needToSwitch = false;
        //        if(destinationToGoTo == 1)
        //        {
        //            transform.LookAt(destination1.transform);
        //            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //            //if(transform.position.x == destination1.transform.position.x && transform.position.y == destination1.transform.position.y && transform.position.z == destination1.transform.position.z)
        //            //{
        //            //    Debug.Log("I am equal to the destination i was sent to");
        //            //    lastDestinationGoneTo = 1;
        //            //    goingToDestination = false;
        //            //}
        //        }
        //        if(destinationToGoTo == 2)
        //        {
        //            transform.LookAt(destination2.transform);
        //            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //            //if (transform.position.x == destination2.transform.position.x && transform.position.y == destination2.transform.position.y && transform.position.z == destination2.transform.position.z)
        //            //{
        //            //    Debug.Log("I am equal to the destination i was sent to");

        //            //    lastDestinationGoneTo = 2;
        //            //        goingToDestination = false;
        //            //}
        //        }
        //        if (destinationToGoTo == 3)
        //        {
        //            transform.LookAt(destination3.transform);
        //            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //            //if (transform.position.x == destination3.transform.position.x && transform.position.y == destination3.transform.position.y && transform.position.z == destination3.transform.position.z)
        //            //{
        //            //    Debug.Log("I am equal to the destination i was sent to");

        //            //    lastDestinationGoneTo = 3;
        //            //    goingToDestination = false;
        //            //}
        //        }
        //        if (destinationToGoTo == 4)
        //        {
        //            transform.LookAt(destination4.transform);
        //            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //            //if (transform.position.x == destination4.transform.position.x && transform.position.y == destination4.transform.position.y && transform.position.z == destination4.transform.position.z)
        //            //{
        //            //    Debug.Log("I am equal to the destination i was sent to");

        //            //    lastDestinationGoneTo = 4;
        //            //    goingToDestination = false;
        //            //}
        //        }
        //        if(destinationToGoTo == 5) 
        //        {
        //            transform.LookAt(destination5.transform);
        //            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //            //if (transform.position.x == destination5.transform.position.x && transform.position.y == destination5.transform.position.y && transform.position.z == destination5.transform.position.z)
        //            //{
        //            //    Debug.Log("I am equal to the destination i was sent to");

        //            //    lastDestinationGoneTo = 5;
        //            //        goingToDestination = false;
        //            //}
        //        }
        //    }
        //}
    }
    public void PlayerDetect(bool detected)
    {
        playerDetected = detected;
    }
    //public void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player" && timeSinceLastHit >= hitRate)
    //    {
    //        playerObj.SendMessage("TakeDamage", 1);
    //    }
    //    if(other.tag == "destination1")
    //    {
    //        needToSwitch = true;
    //    }
    //    if(other.tag == "destination2")
    //    {
    //        needToSwitch = true;
    //    }
    //    if (other.tag == "destination3")
    //    {
    //        needToSwitch = true;
    //    }
    //    if (other.tag == "destination4")
    //    {
    //        needToSwitch = true;
    //    }
    //    if (other.tag == "destination5")
    //    {
    //        needToSwitch = true;
    //    }
    //}
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider == playerObj)
    //    {
    //        playerObj.SendMessage("TakeDamage", 1);
    //    }
    //    if(collision.collider == destination1)
    //    {
    //        needToSwitch = true;
    //    }
    //    if(collision.collider == destination2)
    //    {
    //        needToSwitch = true;
    //    }
    //    if(collision.collider == destination3)
    //    {
    //        needToSwitch = true;
    //    }
    //    if(collision.collider == destination4)
    //    {
    //        needToSwitch = true;
    //    }
    //    if(collision.collider == destination5)
    //    {
    //        needToSwitch = true;
    //    }
    //}
}
