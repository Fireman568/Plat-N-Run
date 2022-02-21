using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Player player;
    public bool respawn = false;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y <= -50)
        {
            respawn = true;
            player.deathAmount += 1;
            //player.SendMessage("changeRespawn", true);
        }
        if(player.health == 0)
        {
            respawn = true;
            player.health = 3;
        }
        if(respawn == true)
        {
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(new Vector3(0, spawnPoint.rotation.y, 0));
            player.SendMessage("changeRespawn", respawn);
            StartCoroutine("StopMoving");
        }
        //if(respawn == false)
        //{
        //    player.SendMessage("changeRespawn", true);
        //}
    }
    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(.1f);
        Debug.Log("I should be stopping the movement");
        respawn = false;
        player.SendMessage("changeRespawn", respawn);
    }
    
}
