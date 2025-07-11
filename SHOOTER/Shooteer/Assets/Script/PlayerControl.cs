using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
//using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] float jumpforce;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] GameObject Player1;

    public InputAction playerControls;    
    public InputAction jumpAction;

    public Texture2D cursorTexture;
    private PlayerInput playerInput;
    float xDir;
    float bulletSpeed;
    Rigidbody2D rb;
    [SerializeField] public bool onPlat;
    public bool canDash = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2.0f;
    CharacterController characterController;
    bool onWallThing = false;
    public float targetTime = .5f;
    public bool canShoot = true;
    public Health healths;

    private Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        Health healths = new Health();
        //mouse code to set crosshair to my mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Cursor.SetCursor(cursorTexture, mousePosition, CursorMode.Auto);
        characterController = GetComponent<CharacterController>();

    }
    private void OnEnable()
    {
        
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        jumpAction.Enable();
        playerControls.Enable();
    }
    private void OnDisable()
    {
        jumpAction.Disable();
        //playerControls.Disable();
    }
    public void OnJump(InputAction.CallbackContext input)
    {
        //Debug.Log(onPlat);
        if (input.performed && onPlat == true)
        {
            
            Jump();
            
        }

    }
    public void OnDash(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            Dash();
        }
    }
    public void OnClimb(InputAction.CallbackContext input)
    {
        if (input.performed && onWallThing == true)
        {
            //Debug.Log("ON CLIMB IS RUNNING");
            //rb.velocity = new Vector2(0.0f, .5f);

        }
    }

    public void onMove(InputAction.CallbackContext input)
    {

        xDir = input.ReadValue<Vector2>().x;

    }

    public void OnShoot(InputAction.CallbackContext input)
    {
        if (input.performed && canShoot == true)
        {

            Shoot();
            
        }
    }

    public void wallSlide(bool onWall)
    {
        //Debug.Log("WallSlide is occuring");
        if (!onPlat && rb.velocity.y < 0)
        {

            onWallThing = true;
            //rb.velocity = new Vector2(0.0f, -1F);

        }
        

    }
    public void offWall(bool onWall)
    {
        if (!onPlat && rb.velocity.y < 0)
        {
            onWallThing = false;
            //rb.velocity = new Vector2(0.0f, 0.0f);

        }
    }
    
    public void Jump()
    {
        //Debug.Log("Jump Is Pressed");
        //Debug.Log(moveDirection.y);
        //rb.velocity = new Vector2(rb.velocity.x, 0);
        //rb.velocity += Vector2.up * jumpforce;


        
        rb.velocity = new Vector2(0.0f, jumpforce);
        onPlat = false;
        
    }
    public void wallClimb()
    {



    }
    public void Shoot()
    {

        Camera cam = Camera.main;
        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0);
        direction.Normalize();
        Quaternion qdirection = Quaternion.LookRotation(direction, Vector3.forward);
        GameObject bullet = Instantiate(BulletPrefab, transform);
        qdirection *= Quaternion.Euler(90, 0, 0);
        bullet.transform.rotation = qdirection;
        bullet.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(direction * 500);
        canShoot = false;
        Destroy(bullet, 1.7f);
        //rb.AddForce(new Vector3(50f, 50f, 0f));
        //rb.AddForce(new Vector2(-qdirection.x, -qdirection.y) * 500f);

        //rb = GetComponent<Rigidbody2D>();
        //bullet.velocity = new Vector2(0.0f, 20f);
        //Vector3 mousePos = Mouse.current.position.ReadValue();
        //Vector3 direction = mousePos - transform.position;

        //Debug.Log("This is transform.position" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
        //Debug.Log("This is mousepos" + mousePos.x + ", " + mousePos.y + ", " + mousePos.z);
        //Debug.Log("This is direction" + direction);



        //Quaternion qdirection = Quaternion.Euler(0, 0, direction.z);

        //Quaternion qdirection = Quaternion.Euler(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        //float angle = Mathf.Rad2Deg * (Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x));

        //Quaternion qdirection = Quaternion.LookRotation(direction,Vector3.forward);

        //Quaternion qdirection = Quaternion.Euler(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x));
        //Debug.Log("This is qdirection" + qdirection);


    }

    public void Walk()
    {

        //rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

    }
    public void Dash()
    {

        //Debug.Log("left is being held");
        Debug.Log(rb.velocity.x);
        if (rb.velocity.x < 0)
        {

            rb.velocity = new Vector2(rb.velocity.x - 100f, rb.velocity.y + 0.0f);
            Debug.Log("velo is smaller than " + rb.velocity.x);
        }
        else 
        {

            rb.velocity = new Vector2(rb.velocity.x + 100f, rb.velocity.y + 0.0f);
            Debug.Log("velo is bigger than " + rb.velocity.x);

        }
        canDash = false;
    }
    public void applyKnockback()
    {





    }
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {

            canShoot = true;
            


        }
        
        if (rb.velocity.y < 0)
        {

            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;

        }
        
        Vector3 mousePos = Mouse.current.position.ReadValue();
        if (canDash == false) 
        {

            canDash = true;
            Debug.Log("This is the canDash Var: " + canDash);

        }
        else 
        { 
            rb.velocity = new Vector2(xDir * speed, rb.velocity.y);
        }
            


    }

    public void respawnPlayer()
    {

        transform.position = new Vector3(-8.6f, -1.7f,0);
        healths.UpdateHealth(3);

    }
    private void FixedUpdate()
    {

        //rb.velocity = new Vector2(xDir * speed, rb.velocity.y);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TilemapCollider2D>())
        {
            //Debug.Log("collision is running");
            
            //Debug.Log("Player collided with tilemap!");
            onPlat = true;
            
        }

        if (collision.gameObject.tag == "Enemy")
        {

            double holder = healths.takeDamage(1);

            if (holder <= 0)
            {

                respawnPlayer();

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.tag == "EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Player took damage");
            double holder = healths.takeDamage(1);
            
            if (holder <= 0)
            {

                respawnPlayer();

            }

        }
    }
}
