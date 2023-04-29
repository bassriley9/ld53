using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSheep : Sheep
{


    #region wandering
    private bool isWandering = false;
    private bool isRotL = false;
    private bool isRotR = false;
    private bool isWalking = false;
    #endregion

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isWandering == false)
        {
            //StartCoroutine(Wander());
        }
        if (isRotR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotateSpeed);
        }
        if (isRotL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotateSpeed);
        }
        if (isWalking == true)
        {
            rb.AddForce(transform.forward * moveSpeed / 3);
        }
        if (incaged == true)
        {
            Debug.Log("HUZZAAH");
        }

        /*if (!insight)
        {
            //discardcheck;
            if (DiscartTimer > 0)
            {
                DiscartTimer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("delete");
                GameManager.instance.Discard(this);
            }
        }*/

    }
}
