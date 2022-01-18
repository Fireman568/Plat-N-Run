using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public GameObject turret;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            turret.SendMessage("PlayerDetect", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            turret.SendMessage("PlayerDetect", false);
            //turret.SendMessage("ResetTurretOrientation", true);
        }
    }
}
