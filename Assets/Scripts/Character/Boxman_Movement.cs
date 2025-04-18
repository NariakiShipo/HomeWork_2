using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxman_Movement : MonoBehaviour
{
    public float speed=10;
    public float MaxSpeed=1000;
    public float AddSpeed=0;
    public int reverse=-1;
    private float DefaultSpeed;

    public bool flag;

    public Vector3 rotateDirection;
    private Transform trans;
    
    // Start is called before the first frame update
    void Start()
    {
        trans =GetComponent<Transform>();
        DefaultSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Determine the boxman can move
        if(Input.GetKeyDown("m"))
        {
            flag = !flag;
        }
        if(flag)
        Move();

        
    }

    void Move()
    {
        if(speed<=MaxSpeed)
        speed += AddSpeed;

        else
        speed = 0;
          
        if(Input.GetKeyDown("space"))
        reverse =reverse * -1;
        if(Input.GetKeyDown("b"))
        AddSpeed = AddSpeed * 10;
        else
        AddSpeed = DefaultSpeed;
        if(Input.GetKeyDown("n"))
        speed = 0;

        trans.Rotate(rotateDirection*speed*reverse*Time.deltaTime);   
    }
}
