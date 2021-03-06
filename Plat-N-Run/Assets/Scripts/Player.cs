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
    [Tooltip("This is the multiplier for the dash that parkour man will have")]
    public float dashMultiplier;
    [Tooltip("This is how long the player has been slidng")]
    public float slidingTime;
    [Tooltip("This is the max time I want the player to be sliding before going back to moving regularly")]
    public float maxSlidingTime;
    [Tooltip("This is the sliding cooldown after the player slides")]
    public float slidingCooldown;
    [Tooltip("This is the time since sliding")]
    public float timeSinceSlide;
    [Tooltip("Whether or not the player is dashing")]
    public bool dashing;
    [Tooltip("the threshhold at which the dashing times for both dashes will be checked against")]
    public float dashTimeThreshhold;
    [Tooltip("The current time of the level")]
    public float levelTime;
    [Tooltip("The amount of deaths the player currently has")]
    public int deathAmount;
    [Tooltip("How many times the player has jumped")]
    public int jumpAmount;
    //[Tooltip("Total time for the current run")]
    //public float totalTime;
    [Tooltip("How many collectibles the player has collected currently. Will be used for the collectible achievement")]
    public int collectiblesCollected;
    

    [Header("Falling variables")]
    [Tooltip("How much the downforce when in the air to bring the character back to the ground")]
    public float grav = -9.81f;
    [Tooltip("How fast the character is moving downwards")]
    public float currentVelY = 0f;
    [Tooltip("How fast we want the character to move downward over time. Goes with gravity")]
    public float fallingSpeed;
    [Tooltip("The time since the player has fallen off a platform or been in the air")]
    public float timeSinceFallenOff;
    [Tooltip("The threshhold at which the player is allowed to still jump shortly after falling off of a platform to try and make it seem like they didnt miss a jumps")]
    public float fallOffThreshhold;
    public int health = 3;
    public TextMeshProUGUI timeText;

    [Header("Character Choice")]
    [Tooltip("Default character check")]
    public bool defaultGuy;
    [Tooltip("Big Bulky Man")]
    public bool bigBulkyMan;
    [Tooltip("Agile girl")]
    public bool agileGirl;
    [Tooltip("Jumper man")]
    public bool parkourMan;

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
    [Tooltip("The time for the third wall for specifically the agile character to keep the cooldown per wall")]
    public float verticalWall3Time = 4f;
    [Tooltip("The time for the horizontal wall to keep track of how long its been since the last hor wall has been placed")]
    public float horWallTime = 4f;
    [Tooltip("The condition for the second wall to be placed based on the first wall being placed. Gets changed in code, dont change in editor")]
    public bool verticalWall2Placable;
    [Tooltip("The time threshhold of how long the character can dash. once it hits the threshhold dashing is turned to false")]
    public float dashingTime;
    [Tooltip("The threshhold of how long the player can dash in the air until the gravity starts kicking in and pulling the character down again")]
    public float dashingTimeThreshhold;
    [Tooltip("cooldown for one dash that the parkour guy will have")]
    public float dash1Time;
    [Tooltip("cooldown for 2nd dash that the parkour guy will have")]
    public float dash2Time;
    [Tooltip("Whether or not the first dash has been used")]
    public bool dash1Used;
    [Tooltip("Whether or not the second dash has been used")]
    public bool dash2Used;
    
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
    [Tooltip("Used specifically for the parkour man to have him be able to jump twice")]
    public bool canJump;
    [Tooltip("Used to keep tracj of how many times parkour man has jumped to be able to change canJump to false or true")]
    public int numTimesJumped;

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
    public GameObject agileGirlCooldown;

    

    //UI text for everyone except agile girl
    [Tooltip("The UI text for the cooldown of the first vertical wall")]
    public TextMeshProUGUI vWall1CD;
    public TextMeshProUGUI v1CDText;
    [Tooltip("The UI text for the cooldown of the second vertical wall")]
    public TextMeshProUGUI vWall2CD;
    public TextMeshProUGUI v2CDText;
    
    [Tooltip("The UI text for the cooldown of the horizontal wall")]
    public TextMeshProUGUI hWall;
    public TextMeshProUGUI hWText;
    [Tooltip("The UI text for parkour man when played")]
    public TextMeshProUGUI dash1;
    public TextMeshProUGUI d1Text;
    [Tooltip("The UI text for the parkour man when played")]
    public TextMeshProUGUI dash2;
    public TextMeshProUGUI d2Text;

    // UI text for the agile girl
    [Tooltip("The UI text for the cooldown of the first vertical wall")]
    public TextMeshProUGUI agilevWall1CD;
    public TextMeshProUGUI agilev1CDText;
    [Tooltip("The UI text for the cooldown of the second vertical wall")]
    public TextMeshProUGUI agilevWall2CD;
    public TextMeshProUGUI agilev2CDText;
    [Tooltip("The UI text for the cooldown of the third vertical wall (this is only in the case of the agile girl)")]
    public TextMeshProUGUI agilevWall3CD;
    public TextMeshProUGUI agilev3CDText;
    [Tooltip("The UI text for the cooldown of the horizontal wall")]
    public TextMeshProUGUI agilehWall;
    public TextMeshProUGUI agilehWText;
    

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

    public int characterPicked;
    
    public void Start()
    {
        // in anticipation of the objective system and character selection, the bool for which character has been selected and which objectives need to be displayed will be set in here at the start of 
        //each scene so the character character is chosen and its subsequent specific movement variables, as well as the specific objectives it needs
        characterPicked = PlayerPrefs.GetInt("characterPick");
        controller = GetComponent<CharacterController>();
        wallRunComp = GetComponent<WallRun>();
        initialPos = playerCamera.transform.localPosition;
        slidingPos = new Vector3(initialPos.x, initialPos.y - 1, initialPos.z);
        defaultGuy = false;
        bigBulkyMan = false;
        agileGirl = false;
        parkourMan = false;
        collectiblesCollected = 0;
        agileGirlCooldown.SetActive(false);

        vWall1CD.text = "";
        v1CDText.text = "";
        vWall2CD.text = "";
        v2CDText.text = "";
        
        hWall.text = "";
        hWText.text = "";

        agilevWall1CD.text = "";
        agilev1CDText.text = "";
        agilevWall2CD.text = "";
        agilev2CDText.text = "";
        agilevWall3CD.text = "";
        agilev3CDText.text = "";
        agilehWall.text = "";
        agilehWText.text = "";

        dash1.text = "";
        d1Text.text = "";
        dash2.text = "";
        d2Text.text = "";
        if(characterPicked == 1)
        {
            
        }
        if(gameObject.name == "Player")
        {
            vWall1CD.text = "v wall 1";
            vWall2CD.text = "v wall 2";
            hWall.text = "h wall";
            defaultGuy = true;
        }
        else if(gameObject.name == "BigBulkyMan")
        {
            vWall1CD.text = "v wall 1";
            vWall2CD.text = "v wall 2";
            hWall.text = "h wall";
            bigBulkyMan = true;
        }
        else if(gameObject.name == "AgileQuickGirl")
        {
            agileGirlCooldown.SetActive(true);
            agilevWall1CD.text = "v wall 1";
            agilevWall2CD.text = "v wall 2";
            agilevWall3CD.text = "V Wall 3";
            agilehWall.text = "h wall";
            agileGirl = true;
        }
        else
        {
            Debug.Log("Parkour man is being read");
            dash1.text = "dash 1";
            dash2.text = "dash 2";
            parkourMan = true;
        }
        if(sprintingVolume != null)
        {
            SetVolumeWeight(0);
        }
        horizontalStartingPos = new Vector3(HorizontalSpawnPoint.transform.localPosition.x, HorizontalSpawnPoint.transform.localPosition.y, HorizontalSpawnPoint.transform.localPosition.z);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //timeText.text = "Time: " + totalTime;
    }


    public void Update()
    {
        levelTime += Time.deltaTime;
        //totalTime += Time.deltaTime;
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
            if (timeSinceStartedSprinting == 0 && sprintingVolume != null)
            {
                lastVolumeValue = sprintingVolume.weight;
            }
            timeSinceStartedSprinting += Time.deltaTime;
        }
        else
        {
            timeSinceStartedSprinting = 0;
            if (timeSinceStoppedSprinting == 0 && sprintingVolume != null)
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
            if (slidingTime > maxSlidingTime)
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
        verticalWall3Time += Time.deltaTime;
        horWallTime += Time.deltaTime;
        timeSinceSlide += Time.deltaTime;
        if (dashing)
        {
            dashingTime += Time.deltaTime;
        }
        if (dashingTime >= dashingTimeThreshhold)
        {
            dashing = false;
            dashingTime = 0;
        }

        if (dash1Used)
        {
            dash1Time += Time.deltaTime;
        }
        if (dash2Used)
        {
            dash2Time += Time.deltaTime;
        }
        if (dash1Time >= dashTimeThreshhold)
        {
            dash1Time = 0;
            dash1Used = false;
        }
        if (dash2Time >= dashTimeThreshhold)
        {
            dash2Time = 0;
            dash2Used = false;
        }
        if (!parkourMan)
        {
            playerMove();
        }
        else
        {
            parkourPlayerMove();
        }
        if (Input.GetMouseButtonDown(0))
        {
            wasPressedLeft = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            wasPressedRight = true;
        }
        if (numTimesJumped == 2)
        {
            canJump = false;
        }
        //timeText.text = "Time: " + totalTime;
        //Debug.Log(levelTime);

        if (defaultGuy || bigBulkyMan)
        {
            if (verticalWall1Time > verticalWallCD)
            {
                v1CDText.text = "Ready!";
            }
            else
            {
                float v = verticalWallCD - verticalWall1Time;
                v1CDText.text = v.ToString("F2");
            }
            if (verticalWall2Time > verticalWallCD)
            {
                v2CDText.text = "Ready!";
            }
            else
            {
                float v = verticalWallCD - verticalWall2Time;
                v2CDText.text = v.ToString("F2");
            }
            if (horWallTime > horizontalWallCD)
            {
                hWText.text = "Ready!";
            }
            else
            {
                float v = horizontalWallCD - horWallTime;
                
                hWText.text = v.ToString("F2");
            }
        }
        else if (agileGirl)
        {
            if (verticalWall1Time > verticalWallCD)
            {
                agilev1CDText.text = "Ready!";
            }
            else
            {
                float v = verticalWallCD - verticalWall1Time;
                agilev1CDText.text = v.ToString("F2");
            }
            if (verticalWall2Time > verticalWallCD)
            {
                agilev2CDText.text = "Ready!";
            }
            else
            {
                float v = verticalWallCD - verticalWall2Time;
                agilev2CDText.text = v.ToString("F2");
            }
            if (verticalWall3Time > verticalWallCD)
            {
                agilev3CDText.text = "Ready!";
            }
            else
            {
                float v = verticalWallCD - verticalWall3Time;
                agilev3CDText.text = v.ToString("F2");
            }
            if (horWallTime > horizontalWallCD)
            {
                agilehWText.text = "Ready!";
            }
            else
            {
                float v = horizontalWallCD - horWallTime;
                agilehWText.text = v.ToString("F2");
            }
        }
        else
        {
            if(dash1Time > dashTimeThreshhold)
            {
                d1Text.text = "Ready!";
            }
            else
            {
                float v = dashTimeThreshhold - dash1Time;
                d1Text.text = v.ToString("F2");
            }
            if (dash2Time > dashTimeThreshhold)
            {
                d2Text.text = "Ready!";
            }
            else
            {
                float v = dashTimeThreshhold - dash2Time;
                d2Text.text = v.ToString("F2");
            }
        }
        
    }
    public void FixedUpdate()
    {
        if (defaultGuy || bigBulkyMan)
        {
            SpawnVerticalWall();
            SpawnHorizontalWall();
        }
        else if (agileGirl)
        {
            SpawnVerticalWallAgile();
            SpawnHorizontalWall();
        }
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

    public void SpawnVerticalWallAgile()
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
        else if(verticalWall3Time > verticalWallCD)
        {
            if (wasPressedLeft)
            {
                GameObject goWall = Instantiate(verticalWall, new Vector3(VerticalSpawnPoint.position.x, VerticalSpawnPoint.position.y, VerticalSpawnPoint.position.z), gameObject.transform.rotation);
                goWall.transform.parent = VerticalSpawnPoint.transform;
                gameManager.SendMessage("AddVerticalWalls", goWall);
                verticalWall3Time = 0;
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
            timeSinceFallenOff = 0;
            numTimesJumped = 0;
            canJump = true;
        }
        else
        {
            isGrounded = false;
            timeSinceFallenOff += Time.deltaTime;
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
                    Debug.Log("I have jumped");
                    if (isGrounded)
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += Vector3.up * jumpingMultiplier;
                        jumpAmount += 1;
                    }
                    else
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += wallRunComp.GetWallJumpDirection() * wallJumpMultiplier;
                        jumpAmount += 1;
                    }
                }
                //else if (!isGrounded && !wallRunComp.IsWallRunning() && timeSinceFallenOff <= fallOffThreshhold)
                //{
                //    Debug.Log("I have jumped within the threshhold");
                //    characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                //    characterMovement += Vector3.up * jumpingMultiplier;
                //}

            }
            else if(!isGrounded && Input.GetKeyDown(KeyCode.Space) && !wallRunComp.IsWallRunning() && timeSinceFallenOff <= fallOffThreshhold)
            {
                Debug.Log("I have jumped within the threshhold");
                characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                characterMovement += Vector3.up * jumpingMultiplier;
                jumpAmount += 1;
            }

            else
            {
                Debug.Log("Should be pulling character down");
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

    public void parkourPlayerMove()
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
                    if (isGrounded && isSliding)
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
                    Debug.Log("I have jumped");
                    if (isGrounded)
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += Vector3.up * jumpingMultiplier;
                        jumpAmount += 1;
                    }
                    else
                    {
                        characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                        characterMovement += wallRunComp.GetWallJumpDirection() * wallJumpMultiplier;
                        jumpAmount += 1;
                    }
                    numTimesJumped += 1;
                }
                //else if (!isGrounded && !wallRunComp.IsWallRunning() && timeSinceFallenOff <= fallOffThreshhold)
                //{
                //    Debug.Log("I have jumped within the threshhold");
                //    characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                //    characterMovement += Vector3.up * jumpingMultiplier;
                //}

            }
            else if (!isGrounded && Input.GetKeyDown(KeyCode.Space) && !wallRunComp.IsWallRunning() && timeSinceFallenOff <= fallOffThreshhold)
            {
                Debug.Log("I have jumped within the threshhold");
                characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                characterMovement += Vector3.up * jumpingMultiplier;
                numTimesJumped += 1;
            }
            else if(!isGrounded && Input.GetKeyDown(KeyCode.Space) && !wallRunComp.IsWallRunning() && timeSinceFallenOff >= fallOffThreshhold && canJump)
            {
                characterMovement = new Vector3(characterMovement.x, 0, characterMovement.z);
                characterMovement += Vector3.up * jumpingMultiplier;
                numTimesJumped += 1;
            }

            else if(!dashing)
            {
                Debug.Log("Should be pulling character down");
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
            if(Input.GetMouseButtonDown(0) && !isGrounded && !wallRunComp.IsWallRunning() && (!dash1Used || !dash2Used) )
            {
                dashing = true;
                if (!dash1Used)
                {
                    dash1Used = true;
                }
                else
                {
                    dash2Used = true;
                }
                characterMovement.y = 0;
                Vector3 TargetVelocity = worldSpaceMoveInput * movementSpeed * speedModifier * dashMultiplier;
                characterMovement = Vector3.Lerp(characterMovement, TargetVelocity, movementSharpnessOnGround * Time.deltaTime);
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
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Collectible 1" || other.tag == "Collectible 2" || other.tag == "Collectible 3" || other.tag == "Collectible 4" || other.tag == "Collectible 5" || other.tag == "Collectible 6")
        {
            collectiblesCollected += 1;
            Debug.Log(collectiblesCollected);
        }
    }
}
