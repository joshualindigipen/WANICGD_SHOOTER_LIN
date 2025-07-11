using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    public Transform player;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer playerSpriteRenderer;
    public float characterLocalScaleX;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = playerSpriteRenderer.sprite;

        spriteRenderer.sortingLayerName = playerSpriteRenderer.sortingLayerName;
        spriteRenderer.sortingOrder = playerSpriteRenderer.sortingOrder;

        transform.SetLocalPositionAndRotation(player.position, player.rotation);

        characterLocalScaleX = player.GetChild(0).localScale.x;

        transform.localScale = player.localScale * characterLocalScaleX;


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
