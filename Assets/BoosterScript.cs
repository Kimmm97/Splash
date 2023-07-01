using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterScript : MonoBehaviour
{
    float randomX;
    float randomY;

    public GameObject boost;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-28,28);
        randomY = Random.Range(-5,7);

        this.transform.position = new Vector3(randomX, randomY, this.transform.position.z);
        rb2d = GetComponent<Rigidbody2D> ();   
    }

   void OnCollisionEnter2D(Collision2D collision2D)
   {
        if (collision2D.transform.name == "Circle")
        {
            Instantiate(boost);
            Destroy(this);
            Debug.Log("DESTROY");
        }
   }
}
