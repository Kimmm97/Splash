using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    float randomX;
    float randomY;

    public GameObject point;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        randomX = Random.Range(-28,28);
        randomY = Random.Range(10,15);

        this.transform.position = new Vector3(randomX, randomY, this.transform.position.z);
        rb2d = GetComponent<Rigidbody2D> ();   
    }

}


