using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public GameObject player;

    public Camera cam;

    float mouseX;
    float mouseY;

    float multiplier = .01f;

    float xRotation;
    float yRotation;


    WallRun wallRunComp;
    private void Start()
    {
        wallRunComp = GetComponent<WallRun>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInput();
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * multiplier * sensX;
        xRotation -= mouseY * multiplier * sensY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if(wallRunComp!= null)
        {
            cam.transform.localEulerAngles = new Vector3(xRotation, 0, wallRunComp.GetCameraRoll());
        }
        else
        {
            cam.transform.localEulerAngles = new Vector3(xRotation, 0, 0);
        }
    }
}
