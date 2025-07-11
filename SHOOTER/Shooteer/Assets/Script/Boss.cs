using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Boss : MonoBehaviour
{

    public Transform target;
    Rigidbody2D rb;
    public float within_range;
    [SerializeField] GameObject BulletPrefab;
    public float targetTime = .5f;
    public Score score;
    public int enemyHP = 3;

    // Start is called before the first frame update
    void Start()
    {
        Score score = new Score();
        rb = GetComponent<Rigidbody2D>();

    }

    public void shootBullet()
    {

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist <= within_range)
        {
            Debug.Log("Enemy is shooting");

            Camera cam = Camera.main;
            Vector3 direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0);
            direction.Normalize();
            Quaternion qdirection = Quaternion.LookRotation(direction, Vector3.forward);
            GameObject bullet = Instantiate(BulletPrefab, transform);
            bullet.transform.SetParent(null);
            qdirection *= Quaternion.Euler(90, 0, 0);
            bullet.transform.rotation = qdirection;
            bullet.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(direction * 600f);
            Destroy(bullet, 4f);

            targetTime = .5f;

        }


    }
    // Update is called once per frame
    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {

            shootBullet();

        }

    }
    private void FixedUpdate()
    {



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(enemyHP);
        if ((collision.gameObject.tag == "Projectile"))
        {
            enemyHP = enemyHP - 1;
            if (enemyHP == 0) 
            {
                score.AddScore(1);
                //Debug.Log("Bullet is destroying Enemy!");
                Destroy(gameObject);
                Destroy(collision.gameObject);

            }
        }
    }
}
