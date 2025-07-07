using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;
//using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] float jumpforce;
    [SerializeField] GameObject BulletPrefab;

    public InputAction playerControls;    
    public InputAction jumpAction;

    public Texture2D cursorTexture;
    private PlayerInput playerInput;
    float xDir;
    float bulletSpeed;
    Rigidbody2D rb;
    private bool onPlat;


    private Vector2 moveDirection = Vector2.zero;

    void Start()
    {
        //mouse code to set crosshair to my mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        Cursor.SetCursor(cursorTexture, mousePosition, CursorMode.Auto);
        //Debug.Log("test");


    }
    private void OnEnable()
    {
        //PlayerInput.ActivateInput;
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        jumpAction.Enable();
        playerControls.Enable();
    }
    private void OnDisable()
    {
        jumpAction.Disable();
        playerControls.Disable();
    }
    public void OnJump(InputAction.CallbackContext input)
    { 
        if (input.performed && onPlat == true)
        {

            //Debug.Log("Jump Is Pressed");
            //Debug.Log(moveDirection.y);
            rb.velocity = new Vector2(0.0f, jumpforce);
            onPlat = false;
            
        }

    }

    public void onMove(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            //Debug.Log("Move Is Pressed!");
            //Debug.Log(moveDirection.y);
        }

        xDir = input.ReadValue<Vector2>().x;
    }

    public void OnShoot(InputAction.CallbackContext input)
    {
        if (input.performed)
        {

            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 direction = mousePos - transform.position;
            direction.Normalize();




            /*
            Debug.Log("This is transform.position" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
            Debug.Log("This is mousepos" + mousePos.x + ", " + mousePos.y + ", " + mousePos.z);
            Debug.Log(direction);
            */

            //Quaternion qdirection = Quaternion.Euler(0, 0, direction.z);

            //Quaternion qdirection = Quaternion.Euler(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            //float angle = Mathf.Rad2Deg * (Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x));

            Quaternion qdirection = Quaternion.LookRotation(direction,Vector3.up);

            //Debug.Log("This is qdirection" + qdirection);

            Instantiate(BulletPrefab, transform.position,qdirection);  
            



            //Debug.Log(transform.position);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y);

    }
    private void FixedUpdate()
    {

        //rb.velocity = new Vector2(xDir * speed, rb.velocity.y);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TilemapCollider2D>())
        {
            //Debug.Log("Player collided with tilemap!");
            onPlat = true;
            
        }
        

    }
}
