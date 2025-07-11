using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BulletDetector : MonoBehaviour
{
    Rigidbody rb;

   
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Projectile") || (collision.gameObject.tag == "EnemyProjectile"))
        {
            
            //Debug.Log("Bullet is destroying Enemy!");
            //Destroy(gameObject);
            Destroy(collision.gameObject);


        }
    }

}
