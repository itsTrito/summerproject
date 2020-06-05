using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public RigidBody body;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.getKeyDown("a"))
        {
            body.velocity.x = 1f;
        }

        if (Input.getKeyUp("a") || Input.getKeyUp("d"))
        {
            body.velocity.x = 0f
        }

        if (Input.getKeyDown("d"))
        {
            body.velocity.x = -1f;
        }
    }
}