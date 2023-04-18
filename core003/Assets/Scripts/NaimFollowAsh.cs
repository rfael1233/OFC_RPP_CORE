using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaimFollowAsh : MonoBehaviour
{
    public float speed;
    public float StoppingDistance;
    private Transform Target; //seguir o player
    void Start()
    {
        
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, Target.position) > StoppingDistance)
        {
            transform.position = Vector2.Lerp(transform.position, Target.position, speed * Time.fixedDeltaTime);
           
        }
       
    }
}
