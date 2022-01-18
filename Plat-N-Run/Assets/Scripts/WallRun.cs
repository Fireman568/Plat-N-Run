using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallRun : MonoBehaviour
{

    //This is not completely my own. I am getting help from a unity workshop runthrough of how their wall running works by going 
    //through the lines of code in their script. The link is: https://learn.unity.com/tutorial/creating-an-fps-wall-run-mechanic-workshop-video-1?projectId=5f84be45edbc2a0e68ba6062#5f84bf10edbc2a00223e0818
    //again this is not completely my own. I am just using it as a guide to help me figure out what I need to do to get the desired effect.
    //Im not sure who did it or made the script, so credit goes toward the people on the unity Youtube channel for making this mechanic 
    //outlined in the video.

    //max distance from the wall I can be in order to wall run
    [Header("Coditions")]
    [Tooltip("The distance from the character has to be within in order to wall run")]
    public float wallDistance = 1;
    //the minimum distance from the ground i have to be in order to be able to wall run
    [Tooltip("The minimum distance from the ground i need to be in order to be able to wall run")]
    public float height = 1.2f;
    [Tooltip("The threshold of the angle needed to attach to a wall")]
    public float angleThreshold = .1f;

    [Header("Wall Running Variables")]
    [Tooltip("The extra speed the character will have when on a wall")]
    //the extra speed i will have while im on a wall
    public float wallSpeed = 1.2f;
    [Tooltip("The angle at which the camera will turn while on the wall")]
    //this will be how much roll the camera will have while on the wall, to emphasize being on the wall
    public float cameraRoll = 15;
    [Tooltip("Prevents the player from jumping too many times on the same wall")]
    //to prevent the player from jumping too quickly between walls and jumping on the same walls multiple times
    public float jumpDuration = 1;
    [Tooltip("goes into calculations when jumping off a wall")]
    //this variable will help calculate the angle at which we jump off of the wall depending on the angle we are at or looking at when 
    //the player jumps
    public float wallBounce = 3;
    [Tooltip("How long it takes to get to the desired angle")]
    public float cameraTransitionDuration = 1;
    [Tooltip("The force that is applied to the player when on a wall to make them move up or down depending on the angle of the wall as they run on the wall to prevent infinitely running on the wall")]
    //this is going to pull the player down while they are wallrunning so they cant go completely straight with no
    //hinderance
    public float wallForce = 10;
    [Tooltip("This is the upforce applied to the player if the x rotation of the wall is negative so the player can go up with the wall")]
    public float wallUpForce = 10;
    [Tooltip("This is the downforce applied to the player if the x rotation of the wall is positive so the player can go down a wall")]
    public float wallDownForce = 10;
    [Tooltip("This will be the direction that the character will go based on the upforce and rotation of the wall the player is trying to run on")]
    public Vector3 direction;

    public float multiplier;

    

    Player playerController;
    GameObject player;

    //this will store the directions we want to check so we can look for walls in those directions
    Vector3[] directions;

    //This will store raycast info on where the hits were and what direction so we can grab the right wall when we try 
    //to wall run
    RaycastHit[] hit;

    bool isWallRunning = false;

    //This is to keep track of the last known position of the wall we were on and the normal of it.
    Vector3 lastWallPosition;
    Vector3 lastWallNormal;

    //These keep track of the time since the player last jumped, attached, and detached from a wall
    float elapsedTimeSinceJump = 0;
    float timeSinceWallAttach = 0;
    float timeSinceWallDetach = 0;

    //keeps track of whether or not we are jumping
    bool jumping;

    //This is a new way yo assign a boolean value from a different script. with this one we cant a boolean method to 
    //hold the value of whether or not the player is grounded
    bool isPlayerGrounded() => playerController.isGrounded;

    //This is gonna assign the same way but assign it from the same script for whether we are wallrunning or not;
    public bool IsWallRunning() => isWallRunning;

    //This is to check to see if we can wallrun or not based on whether or not we are in the air, and not grounded
    bool CanWallRun()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical");
        return !isPlayerGrounded() && verticalAxis > 0 && verticalCheck();
    }

    //This checks if we are high enough in the air using a raycast to see the distance of how high we are when we jump
    bool verticalCheck()
    {
        return !Physics.Raycast(transform.position, Vector3.down, height);
    }





    // Start is called before the first frame update
    void Start()
    {
        //grab the reference for the player and its player script
        playerController = GetComponent<Player>();

        //populate the directions array with the directions we want to check for walls
        //in this case we only need to look at the forward facing angles to determine if theres a wall in front of us and 
        //dont need to worry about the backside of us

        directions = new Vector3[]
        {
            Vector3.right , //for straight right
            Vector3.right + Vector3.forward, //for diagonal right
            Vector3.forward , //for straight forward
            Vector3.forward + Vector3.left, // for diagonal left
            Vector3.left  //for straight left
            //Vector3.back + Vector3.left,
            //Vector3.back + Vector3.right
        };


    }

    // Update is called once per frame
    void LateUpdate()
    {
        //say at the top that wallrunning is equal to false
        isWallRunning = false;
        //say that we're jumping if we are jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }

        //here were going to see what walls are near us and see where they are based on the raycasts we are shooting out in the 
        //directions that we put it into our directions array previously
        //for this we cycle through the directions and shoot out a ray cast for those directions
        if (CanAttach())
        {
            hit = new RaycastHit[directions.Length];
            for (int i = 0; i < directions.Length; i++)
            {
                //declare a vector3 here for the specific direction that we are currently on in the iteration
                Vector3 dir = transform.TransformDirection(directions[i]);

                //shoot out a raycast from our current position, to the specified direction, putting that hit into the raycasthit array
                //and only shoot out the raycast as far as the wallDistance specified in the editor
                Physics.Raycast(transform.position, dir, out hit[i], wallDistance);
                if (hit[i].collider != null)
                {
                    //if we hit something on that specific collider, turn the ray green to say that we hit something
                    Debug.DrawRay(transform.position, dir * hit[i].distance, Color.green);
                }
                else
                {
                    //otherwise keep the ray red since we havent hit anything yet
                    Debug.DrawRay(transform.position, dir * wallDistance, Color.red);
                }
            }
            //if we can wall run, order the hit array by the distance they are from the player, so the closest hit is in array slot 0
            //so we know at all times which wall and direction is closest to the player.
            //from that we can assign the last wall position to the point of the closest raycast hit, and do the same with the last
            //wall normal
            if (CanWallRun())
            {
                hit = hit.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
                if(hit.Length > 0)
                {
                    OnWall(hit[0]);
                    lastWallPosition = hit[0].point;
                    lastWallNormal = hit[0].normal;
                }
            }
        }

        //if we are wall running then we are no longer detached from a wall and can set that value to 0
        //conversely, we are now attached to a wall so we can increment that value by time.deltaTime.
        //then we apply the downforce of being on a wall to the character movement variable so the player goes down the 
        //as they are running on it
        if (isWallRunning)
        {
            timeSinceWallDetach = 0;
            timeSinceWallAttach += Time.deltaTime;
            playerController.characterMovement += Vector3.down * wallDownForce * Time.deltaTime;
            
        }
        //we wanna do the opposite if we arent wallrunning. dont apply a force at all and set the correct values to what they need to
        //be
        else
        {
            timeSinceWallAttach = 0;
            timeSinceWallDetach += Time.deltaTime;
        }
    }

    

    //if we can attach, see if we are jumping and if we are, count how long weve been in the air, and then if we have been in the air 
    //longer than the set time to be in the air in order to wall run, set the time since jump back to 0
    //and then set jumping to false since we are running on the wall and no longer jumping
    bool CanAttach()
    {
        if (jumping)
        {
            elapsedTimeSinceJump += Time.deltaTime;
            if (elapsedTimeSinceJump > jumpDuration)
            {
                elapsedTimeSinceJump = 0;
                jumping = false;
            }
            return false;
        }
        return true;
    }

    /* here we are going to see if the threshold is met by taking the dot product of the raycast hit thats being passed to
     * the method. if it does then we get the raw vertical axis input, and the forward of our direction in world space
     * then we draw out the rays to see them 
     * then we apply the alongwall value multiplied by the vertical value mutliplied by the wall speed to move along the wall
     * and lastly set wallrunning to true
    */
    void OnWall(RaycastHit hit)
    {
        float d = Vector3.Dot(hit.normal, Vector3.up);
        float speedModifier = playerController.isSprinting ? playerController.sprintingMultiplier : 1f;
        if(d >= -angleThreshold && d <= angleThreshold && hit.collider.tag == "RunWall" || hit.collider.tag == "VerticalWall")
        {
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 alongWall = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, alongWall.normalized * 10, Color.green);
            Debug.DrawRay(transform.position, lastWallNormal * 10, Color.red);
            playerController.characterMovement = alongWall.normalized * vertical * wallSpeed * speedModifier ;
            isWallRunning = true;
        }
    }

    //here we check to see what side the wall is on. 
    float CalculateSide()
    {
        if (isWallRunning)
        {
            Vector3 heading = lastWallPosition - transform.position;
            Vector3 perp = Vector3.Cross(transform.forward, heading);
            float dir = Vector3.Dot(perp, transform.up);
            return dir;
        }
        return 0;
    }

    //This takes in the value that we get on the CalculateSide method to determine which way we want to the camera to tilt

    public float GetCameraRoll()
    {
        float dir = CalculateSide();
        float cameraAngle = playerController.playerCamera.transform.eulerAngles.z;
        float targetAngle = 0;

        if(dir != 0)
        {
            targetAngle = Mathf.Sign(dir) * cameraRoll;
        }
        return Mathf.LerpAngle(cameraAngle, targetAngle, Mathf.Max(timeSinceWallAttach, timeSinceWallDetach) / cameraTransitionDuration);
    }

    //This gets the direction we should go if we jump and we're wallrunning
    public Vector3 GetWallJumpDirection()
    {
        if (isWallRunning)
        {
            return lastWallNormal * wallBounce + Vector3.up;
        }
        return Vector3.zero;
    }


}
