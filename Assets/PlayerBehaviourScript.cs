using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sorry for the lack of comments

public class PlayerBehaviourScript : MonoBehaviour
{
    public bool jumpTrue = true;

    public Transform RightEdge;
    public Transform LeftEdge;
    public Transform Ground;

    public float moveSpeed;

    float horizontalInput;
    float verticalInput;
    float horizontalVelocity;
    float verticalVelocity;

    float boost = 2;
    public bool boosted;
    public GameObject boostBall;

    public float score;
    public GameObject point;
    
    private Rigidbody2D rb2d;

       // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        moveSpeed += score;

        horizontalVelocity = (horizontalVelocity + (horizontalInput * moveSpeed) - (horizontalVelocity / 4));
      
        if(jumpTrue == true)
            {
                verticalVelocity = (verticalVelocity + (verticalInput * moveSpeed) - (verticalVelocity / 4));
            }
            else{
                verticalVelocity = (verticalVelocity - (verticalVelocity/150));
                //Debug.Log("working");
            }
       
       
        transform.Translate(new Vector2(horizontalVelocity, verticalVelocity) * Time.deltaTime);
        //Debug.Log(verticalVelocity);

        if(LeftEdge.position.x > this.transform.position.x || this.transform.position.x > RightEdge.position.x || Ground.position.y > this.transform.position.y)
        {
            this.transform.position = new Vector3(0,0,this.transform.position.z);
        } 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    
    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.transform.name == "Water")
        {
           float duration = 0.5f;
            StartCoroutine(DelaySplash(duration));
           
           Debug.Log("Player is in water.");
        }

        if (collision2D.transform.name == "LeftEdge")
        {
            this.transform.position = new Vector2(RightEdge.position.x - 1, this.transform.position.y);
        }

        if (collision2D.transform.name == "RightEdge")
        {
            this.transform.position = new Vector2(LeftEdge.position.x + 1, this.transform.position.y);
        }
        
        if (collision2D.transform.name == "Boost")
        {
            moveSpeed = moveSpeed * boost;
            verticalVelocity = verticalVelocity * boost;
            horizontalVelocity = horizontalVelocity * boost;

            boosted = true;

            Instantiate(boostBall);
            Destroy(collision2D.gameObject);
            boostBall = GameObject.Find("Boost(Clone)");
            boostBall.name = "Boost";
            
            float duration = 2;
            StartCoroutine(Boosttimer(duration));
        } 

        if (collision2D.transform.name == "Point")
        {
            score += 1;
            
            Instantiate(point);
            Destroy(collision2D.gameObject);
            point = GameObject.Find("Point(Clone)");
            point.name = "Point";            

            Debug.Log(score);
   
        } 
    }

    void OnTriggerExit2D(Collider2D collision2D)
    {
       if (collision2D.transform.name == "Water")
        {
           jumpTrue = false;
           Debug.Log("Player is out the water.");
           
        }
 
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.name == "Ground")
        {
            verticalVelocity = - verticalVelocity;
            Debug.Log("touched ground");
            jumpTrue = true;
            
        }
    }

    IEnumerator DelaySplash(float duration)
    {
    yield return new WaitForSeconds(duration);
    jumpTrue = true;
    }

    IEnumerator Boosttimer(float duration)
    {
    yield return new WaitForSeconds(duration);
    moveSpeed = moveSpeed / boost;
    }
}
