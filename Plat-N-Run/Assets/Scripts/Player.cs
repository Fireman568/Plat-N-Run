using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
public class Player : MonoBehaviour
{
    //This is not completely my own. I am getting help from a unity workshop runthrough of how their wall running works by going 
    //through the lines of code in their script. The link is: https://learn.unity.com/tutorial/creating-an-fps-wall-run-mechanic-workshop-video-1?projectId=5f84be45edbc2a0e68ba6062#5f84bf10edbc2a00223e0818
    //again this is not completely my own. I am just using it as a guide to help me figure out what I need to do to get the desired effect.
    //Im not sure who did it or made the script, so credit goes toward the people on the unity Youtube channel for making this mechanic 
    //outlined in the video.
    
    [Header("Movement")]
    [Tooltip("How fast the character will be able to move")]
    public float movementSpeed = 1f;
    [Tooltip("How fast the character will move in the air whenever we jump")]
    public float accelerationInAir;
    [Tooltip("This is how fast the character will move in the air at a maximum but only horizontally")]
    public float maxAirSpeed;
    [Tooltip("This is how fast the character lerps to the max movement speed from a stationary point and then back down to a stationary point ")]
    public float movementSharpnessOnGround = 15;
    [Tooltip("The distance at which the character will respawn if for some reason it ever falls off the map")]
    public float respawnDistance;
    [Tooltip("This is how fast the character can rotate when the player moves the mouse")]
    public float rotationSpeed = 200f;
    [Tooltip("Checks to see if the character is grounded or not based on conditions")]
    public bool isGrounded;
    [Tooltip("This designates whether or not we are sprinting when we press the sprint key")]
    public bool isSprinting;
    [Tooltip("This desgignates whether or not we are sliding when we press the c key")]
    public bool isSliding;
    [Tooltip("This is how much faster we'll move if we are sprinting, designated by the isSprinting boolean")]
    public float sprintingMultiplier;
    [Tooltip("This is the multiplier applied to the player when they are sprinting. will be applied to the jumping multiplier if sprinting, and the walljumpmultiplier if sprinting")]
    public float sprintingJumpMultiplier;
    [Tooltip("This is the multiplier for sliding, which can only be done if we are sprinting")]
    public float slidingMultiplier;
    [Tooltip("This is how long the player has been slidng")]
    public float slidingTime;
    [Tooltip("This is the max time I want the player to be sliding before going back to moving regularly")]
    public float maxSlidingTime;
    [Tooltip("This is the sliding cooldown after the player slides")]
    public float slidingCooldown;
    [Tooltip("This is the time since sliding")]
    public float timeSinceSlide;
    [Header("Falling variables")]
    [Tooltip("How much the downforce when in the air to bring the character back to the ground")]
    public float grav = -9.81f;
    [Tooltip("How fast the character is moving downwards")]
    public float currentVelY = 0f;
    [Tooltip("How fast we want the character to move downward over time. Goes with gravity")]
    public float fallingSpeed;
    public int health = 3;
    public TextMeshProUGUI healthText;

    [Header("Camera")]
    [Tooltip("Sensitivity of the camera moving horizontally")]
    public float sensX;
    [Tooltip("Sensitivity of the camera moving vertically")]
    public float sensY;
    [Tooltip("the angle of the camera vertically. Used in calculations for camera movement, dont change")]
    public float verticalCamAngle;
    [Tooltip("The initial position the camera attached to the player is at")]
    public Vector3 initialPos;
    [Tooltip("The position we want to lerp to when we slide")]
    public Vector3 slidingPos;
    [Tooltip("Determines if we need to lerp back to our original position after sliding")]
    public bool needToLerpBack;

    [Header("Walls")]
    [Tooltip("The cooldown for the vertical walls to be used, its also per wall")]
    public float verticalWallCD;
    [Tooltip("The cooldown for the horizontal walls to be used. Only has one at a time so isnt per wall.")]
    public float horizontalWallCD;
    [Tooltip("The time for the first vertical wall to keep the cooldown per wall")]
    public float verticalWall1Time = 4f;
    [Tooltip("The time for the second vertical wall to keep the cooldown per wall")]
    public float verticalWall2Time = 4f;
    [Tooltip("The time for the horizontal wall to keep track of how long its been since the last hor wall has been placed")]
    public float horWallTime = 4f;
    [Tooltip("The condition for the second wall to be placed based on the first wall being placed. Gets changed in code, dont change in editor")]
    public bool verticalWall2Placable;
    
    [Tooltip("Starting pos for the horizontal wall spawnpoint")]
    public Vector3 horizontalStartingPos;
    public bool wasPressedLeft = false;
    public bool wasPressedRight = false;
    
    [Header("Jumping")]
    [Tooltip("How much the jump multiplies our upward velocity. Controls how high we jump")]
    public float jumpingMultiplier = 10f;
    [Tooltip("How much jumping off of a wall multiplies our upward velocity. Controls how high and far we jump off of a wall")]
    public float wallJumpMultiplier;
    [Tooltip("For the upgraded hor wall. how much the character gets flown up when hitting a jumppad. Controls how much the character goes  up on a jumpPad")]
    public float jumpPadMultiplier = 10f;

    public bool notRespawning = true;
    

    

    
    
    [Header("References")]
    [Tooltip("the Character controller component on the player")]
    public CharacterController controller;
    [Tooltip("The reference to the vertical wall prefab")]
    public GameObject verticalWall;
    [Tooltip("The reference to the horizontal wall prefab")]
    public GameObject horizontalWall;
    [Tooltip("The reference to the gameManager object that handles the quantity of walls being placed in the world")]
    public GameObject gameManager;
    [Tooltip("The reference to the spawn point where the horizontal walls spawn when the player wants to place the horizontal wall")]
    public Transform HorizontalSpawnPoint;
    [Tooltip("The reference to the spawn point where the vertical walls spawns when the player wants to place the vertical wall")]
    public Transform VerticalSpawnPoint;
    [Tooltip("The reference to the camera attached to the player")]
    public Camera playerCamera;
    
    [Tooltip("The reference to the transform of the spawnPoint ")]
    public Transform groundDetect;

    public Volume sprintingVolume;
    public float cameraTransitionDuration = 1;
    public float timeSinceStartedSprinting;
    public float timeSinceStoppedSprinting;
    public float lastVolumeValue;

    public Vector3 characterMovement;
    public LayerMask groundMask;



    WallRun wallRunComp;
    
    public void Start()
    {
       
        controller = GetComponent<CharacterController>();
        wallRunComp = GetComponent<WallRun>();
        initialPos = playerCamera.transform.localPosition;
        slidingPos = new Vector3(initialPos.x, initialPos.y - 1, initialPos.z);
        if(sprintingVolume != null)
        {
            SetVolumeWeight(0);
        }
        horizontalStartingPos = new Vector3(HorizontalSpawnPoint.transform.localPosition.x, HorizontalSpawnPoint.transform.localPosition.y, HorizontalSpawnPoint.transform.localPosition.z);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        healthText.text = "Health: " + health;
    }


    public void Update()
    {
        
        Grounded();
        Sprinting();
        Sliding();
        //if (isGrounded == false)
        //{
        //    currentVelY += -grav * fallingSpeed * Time.deltaTime;
        //}
        if (isGrounded == true)
        {
            currentVelY = -5f;
        }
        if (isSprinting)
        {
            timeSinceStoppedSprinting = 0;
            if(timeSinceStartedSprinting == 0 && sprintingVolume != null)
            {
                lastVolumeValue = sprintingVolume.weight;
            }
            timeSinceStartedSprinting += Time.deltaTime;
        }
        else
        {
            timeSinceStartedSprinting = 0;
            if(timeSinceStoppedSprinting == 0 && sprintingVolume != null)
            {
                lastVolumeValue = sprintingVolume.weight;
            }
            timeSinceStoppedSprinting += Time.deltaTime;
        }
        if (sprintingVolume != null)
        {
            HandleVolume();
        }
        if (isSliding)
        {
            slidingTime += Time.deltaTime;
            if(slidingTime > maxSlidingTime)
            {
                timeSinceSlide = 0;
                isSliding = false;
            }
        }
        else
        {
            slidingTime = 0;
        }
        verticalWall1Time += Time.deltaTime;
        verticalWall2Time += Time.deltaTime;
        horWallTime += Time.deltaTime;
        timeSinceSlide += Time.deltaTime;

        playerMove();
        if (Input.GetMouseButtonDown(0))
        {
            wasPressedLeft = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            wasPressedRight = true;
        }

        healthText.text = "Health: " + health;
    }
    public void FixedUpdate()
    {
        SpawnVerticalWall();
        SpawnHorizontalWall();
    }


    public void SpawnVerticalWall()
    {
        if (verticalWall1Time > verticalWallCD)
        {
            if (wasPressedLeft)
            {
                GameObject goWall = Instantiate(verticalWall, new Vector3(VerticalSpawnPoint.position.x, VerticalSpawnPoint.position.y, VerticalSpawnPoint.position.z), gameObject.transform.rotation);
                goWall.transform.parent = VerticalSpawnPoint.transform;
                gameManager.SendMessage("AddVerticalWalls", goWall);

                verticalWall1Time = 0;
                verticalWall2Placable = true;
            }
        }
        else if (verticalWall2Time > verticalWallCD)
        {
            if (wasPressedLeft)
            {
                GameObject goWall = Instantiate(verticalWall, new Vector3(VerticalSpawnPoint.position.x, VerticalSpawnPoint.position.y, VerticalSpawnPoint.position.z), gameObject.transform.rotation);
                goWall.transform.parent = VerticalSpawnPoint.transform;
                gameManager.SendMessage("AddVerticalWalls", goWall);
                verticalWall2Time = 0;
            }
        }
        wasPressedLeft = false;
    }

    public void SpawnHorizontalWall()
    {
        if(horWallTime > horizontalWallCD)
        {
            if (wasPressedRight)
            {
                GameObject goWall = Instantiate(horizontalWall, new Vector3(HorizontalSpawnPoint.position.x, HorizontalSpawnPoint.position.y, HorizontalSpawnPoint.position.z), gameObject.transform.rotation);
                goWall.transform.parent = HorizontalSpawnPoint.transform;
                gameManager.SendMessage("AddHorizontalWalls", goWall);
                horWallTime = 0;
            }
        }
        wasPressedRight = false;
    }

    public void Grounded()
    {
        
        if (controller.isGrounded)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            
        }
        else
        {
            isSprinting = false;
            
        }
    }
    public void Sliding()
    {
        if (isSprinting)
        {
            if (Input.GetKeyDown(KeyCode.C) && timeSinceSlide > slidingCooldown)
            {
                isSliding = true;
            }
        }
        else
        {
            isSliding = false;
        }
    }
    public void playerMove()
    {
        if (notRespawning)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            Vector3 worldSpaceMoveInput = transform.TransformVector(move);

            float mouseY = Input.GetAxisRaw("Mouse Y");
            float mouseX = Input.GetAxisRaw("Mouse X");
            float speedModifier = isSprinting ? sprintingMultiplier : 1f;
            float slidingModifier = isSliding ? slidingMultiplier : 1f;
            //rotate Player horizontally
            {
                transform.Rotate(new Vector3(0f, (mouseX * sensX), 0f), Space.Self);
            }
            //rotate vertically
            {
                verticalCamAngle += mouseY * sensY;
                verticalCamAngle = Mathf.Clamp(verticalCamAngle, -89f, 89f);

                if (wallRunComp != null)
                {
                    playerCamera.transform.localEulerAngles = new Vector3(-verticalCamAngle, 0, wallRunComp.GetCameraRoll());
                }
                else
                {
                    playerCamera.transform.localEulerAngles = new Vector3(-verticalCamAngle, 0, 0);
                }
            }


            if (isGrounded || (wallRunComp != null && wallRunComp.IsWallRunning()))
            {
                if (isGrounded)
                {
                    if(isGrounded && isSliding)
                    {
                        Vector3 Target = worldSpaceMoveInput * movementSpeed * speedModifier * slidingModifier;
                        characterMovement = Vector3.Lerp(characterMovement, Target, movementSharpnessOnGround * Time.deltaTime);
                        playerCamera.transform.localPosition = Vector3.Lerp(initialPos, slidingPos, 1);
                        needToLerpBack = true;
                    }
                    else
                    {
                        Vector3 TargetVelocity = worldSpaceMoveInput * movementSpeed * speedModifier;
                        characterMovement = Vector3.Lerp(characterMovement, TargetVelocity, movementSharpnessOnGround * Time.deltaTime);
                        if (needToLerpBack)
                        {
                            playerCamera.transform.localPosition = Vector3.Lerp(slidingPos, initialPos, 1);
                            needToLerpBack = false;
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || (wallRunComp != null && wallRunComp.IsWallRunning())))
                {
                    if (isGrounded)
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += Vector3.up * jumpingMultiplier;
                    }
                    else
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += wallRunComp.GetWallJumpDirection() * wallJumpMultiplier;
                    }
                }
            }
            else
            {
                if (wallRunComp == null || (wallRunComp != null && !wallRunComp.IsWallRunning()))
                {
                    characterMovement += worldSpaceMoveInput * accelerationInAir * Time.deltaTime;
                    float verticalVelocity = characterMovement.y;
                    Vector3 horizontalVelocity = Vector3.ProjectOnPlane(characterMovement, Vector3.up);
                    horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxAirSpeed * speedModifier);
                    characterMovement = horizontalVelocity + (Vector3.up * verticalVelocity);
                    characterMovement += Vector3.down * grav * Time.deltaTime;
                }
            }
            controller.Move(characterMovement * Time.deltaTime);
        }
        
        

        


    }
    public void changeRespawn(bool respawn)
    {
        notRespawning = !respawn;
    }
    public void CheckHeight()
    {
        if(characterMovement.y <= respawnDistance)
        {

        }
    }
    void HandleVolume()
    {
        float w = 0;
        if (isSprinting)
        {
            w = Mathf.Lerp(lastVolumeValue, 1, timeSinceStartedSprinting / cameraTransitionDuration);
        }
        else
        {
            w = Mathf.Lerp(lastVolumeValue, 0, timeSinceStoppedSprinting / cameraTransitionDuration);
        }
        SetVolumeWeight(w);
    }
    void SetVolumeWeight(float weight)
    {
        sprintingVolume.weight = weight;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
