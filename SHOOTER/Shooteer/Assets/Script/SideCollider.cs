using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider : MonoBehaviour
{


    public  bool onWall;
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onWall = true;
        //Debug.Log("This is on wall bool" + onWall);
        Player.wallSlide(onWall);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        onWall = false;
        Player.offWall(onWall);
    }
}
