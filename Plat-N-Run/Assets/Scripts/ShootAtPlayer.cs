using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public bool playerDetected = false;
    public bool resetOrientation = false;
    public Quaternion initialRotation;
    public float rateOfFire;
    public float timeSinceLastShot;
    public GameObject turretHead;
    public GameObject bullet;
    public GameObject spawnReference;
    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = Quaternion.Euler(new Vector3(turretHead.transform.rotation.x,turretHead.transform.rotation.y,turretHead.transform.rotation.z));
        timeSinceLastShot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (playerDetected)
        {
            playerObj = GameObject.Find("Player");
            turretHead.transform.LookAt(playerObj.transform);
            if(timeSinceLastShot > rateOfFire)
            {
                Instantiate(bullet, spawnReference.transform.position, spawnReference.transform.rotation);
                timeSinceLastShot = 0;
            }
        }
        if (resetOrientation)
        {
            turretHead.transform.rotation = initialRotation;
            resetOrientation = false;
        }
    }

    public void PlayerDetect(bool detected)
    {
        playerDetected = detected;
    }
    public void ResetTurretOrientation(bool reset)
    {
        resetOrientation = reset;
    }
}
