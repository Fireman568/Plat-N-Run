using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    //public GameObject spawnReference;
    public float movementSpeed;
    public float timeAlive;
    public GameObject player;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        //spawnReference = GameObject.Find("Spawnpoint");
        //transform.rotation = spawnReference.transform.rotation;
        player = GameObject.Find("Player");
        controller = player.GetComponent<CharacterController>();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }




    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.SendMessage("TakeDamage", 1);
            Destroy(gameObject);
        }
    }
}
