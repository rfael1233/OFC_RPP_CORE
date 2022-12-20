using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       
        //Debug.Log(distance);
        
        
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }
    }
}
