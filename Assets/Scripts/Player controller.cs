using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3; // fixes ambiguity errors
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class PlayerController : MonoBehaviour
{
    //this is a variable for a rigidbody that is attached to the player.
    private Rigidbody2D playerRigidBody;
    public float movementSpeed;
    public float jumpForce;
    private float inputHorizontal;
    public int maxNumJumps;
    private int numJumps;
    private Vector3 mousepos;
    private Transform arm; //needed to store the arms transform
    private float angle;
    public SpringJoint2D springjoint;
    public float maxDistance = 20;
    public bool hasMaxDistance = true;
    private Vector3 grapplePoint;
    public Transform grappleOrigin;
    public Camera mainCam;
    public GameManager gm;
    public PlayerScore gmPs;
    public LineRenderer rope;
    public int precision;
   

    // Start is called before the first frame update
    void Start()
    {
        //I can only get this component using the following line of code
        //becuase the rigidbody2d is attached to the player and this script
        //is also attached to the player. 
        playerRigidBody = GetComponent<Rigidbody2D>();
        arm = this.gameObject.transform.GetChild(0); //gets the arm transform
        
        maxNumJumps = 2;
        numJumps = 2;
        springjoint.enabled = false;
        rope.positionCount = precision;
        rope.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
        GetMousePos(); // important to put any function that uses mousepos after this.
        RotateArm();
        Grapple();
        DrawRope();
    }

    private void movePlayerLateral()
    {
        //if the player presses a move left, d move right
        //"Horizontal" is defined in the input section of the project settings
        //the line below will return:
        //0  - no button pressed
        //1  - right arrow or d pressed
        //-1 - left arrow or a pressed
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        playerRigidBody.velocity = new Vector2(movementSpeed * inputHorizontal, playerRigidBody.velocity.y);

        if(inputHorizontal != 0)
        {
            flipPlayerSprite(inputHorizontal);
        }
    }

    private void flipPlayerSprite(float inputHorizontal)
    {
        if(inputHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(inputHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);

            numJumps++;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
        if(collision.gameObject.CompareTag("Ground"))
        {
            numJumps = maxNumJumps;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.gameObject.tag;
        if(collisionTag == "Score")
        {
            gmPs.setPlayerScore(3);
            Destroy(collision);
        }
        else if(collisionTag == "JumpRefresh")
        {
            numJumps = 1;
            Destroy(collision);
        }
        else if(collisionTag == "SpeedUp")
        {
            movementSpeed += 1;
            Destroy(collision);
        }
        else if(collisionTag == "SpeedDown")
        {
            movementSpeed += -1;
            Destroy(collision);
        }
        else if(collisionTag == "Death")
        {
            gm.gameOver();
        }
        
    }

    private void GetMousePos() //used to rotate the arm and for raycasting
    {
        
        
        mousepos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    private void RotateArm()
    {  
        var mp = mousepos;

        mp.x = mp.x - arm.transform.position.x; // gets the difference between the object we want to rotate and the mouse cursors position
        mp.y = mp.y - arm.transform.position.y;
        angle = Mathf.Atan2(mp.y, mp.x) * Mathf.Rad2Deg; //gets the angle in radians and turns it into a float
        arm.rotation = Quaternion.Euler(0, 0,angle); //rotates the arm using the hidden circle that acts as a rotation point
    }
    private void Grapple()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(GetGrapplePoint()){
                springjoint.connectedAnchor = grapplePoint;
                springjoint.enabled = true;
                rope.enabled = true;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            springjoint.enabled = false;
            rope.enabled = false;
        }
    }
    private bool GetGrapplePoint() //had a bug that made the raycast inaccurate turns out I forgot to normalize the direction vector
    {
        Vector2 mp = mousepos - grappleOrigin.position; //normalize the vector
        Vector3 origin = grappleOrigin.position; // take the position of the object the script is attached to
        var layermask = 1 << 9; // bitshift to get layermask for the raycast
        
        RaycastHit2D hit = Physics2D.Raycast(origin, mp,Mathf.Infinity,layermask); //theres no overload for a raycast without distance so using Mathf.Infinity to satisfy the overload
        if (hit.transform.gameObject.layer == 9) //this if statement is probably redundant but its from before i used a layermask and the code works so why change it
        {

            if(Vector2.Distance(hit.point,origin) <= maxDistance || !hasMaxDistance)
            {
                grapplePoint = hit.point;
                return true;
                 //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow); // debug code
                 //Debug.Log("Did Hit");
                
            }

        }
        return false;
    }
    private void DrawRope()
    {
        for (int i = 0; i < precision; i++)
        {
            rope.SetPosition(i, grapplePoint);
        }
        rope.SetPosition(0,this.transform.position);
        
    }
}
