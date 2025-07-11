using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{

    Rigidbody rb;
    
    public static bool EnemyCanSpawn = true;
    public Transform target; 
    public float within_range;
    public float speed;
    public Score score;



    // Start is called before the first frame update
    void Start()
    {
        Score score = new Score();
        Health health = new Health();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        
        if (dist <= within_range)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

        }
    }
    void FixedUpdate()
    {

        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //rb.velocity = new Vector3(0.0f, 4f,0.0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {


            score.AddScore(1);
            //Debug.Log("Bullet is destroying Enemy!");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            

        }
    }
}
