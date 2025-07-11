using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CF : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 startingPosition;
    public Transform followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    public int camfloat = 0;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (followTarget != null)
        {
            targetPos = new Vector3(followTarget.position.x, followTarget.position.y + 3, transform.position.z);
            Vector3 velocity = (targetPos - transform.position) * moveSpeed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
        }
    }
}
