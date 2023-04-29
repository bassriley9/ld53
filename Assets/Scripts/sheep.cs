using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public bool incaged;
    public bool sold;
    bool insight = false;

    [SerializeField]
    public int value = 5;

    public GameObject Player;
  
    protected float moveSpeed = 10f;
    protected float rotateSpeed = 100f;

    [SerializeField]
    float DiscartTimer = 2f;

    #region wandering
    private bool isWandering = false;
    private bool isRotL = false;
    private bool isRotR = false;
    private bool isWalking = false;
    #endregion


    private bool startled = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (name == "Rare")
        {
            startled = true;
            value = 10;

        }

        if (startled == true)
        {
            moveSpeed = (moveSpeed / 2) + moveSpeed;
        }

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isWandering == false)
        {
            StartCoroutine(Wander());
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
        if(incaged == true)
        {
            Debug.Log("HUZZAAH");
        }

        if(!insight)
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
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "obstacle")
        {
            Debug.Log("HitWall");
            int rotateTime = Random.Range(1, 2);
            isRotL = true;
            new WaitForSeconds(rotateTime);
            isRotL = false;
        }

        if (collision.collider.tag == "pen")
        {
            incaged = true;
        }


    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int RotateLorR = Random.Range(1, 2);
        int WalkWait = Random.Range(1, 3);
        int walkTime;
        if (startled == true)
        {
            walkTime = Random.Range(1, 7);
        }
        else
        {
            walkTime = Random.Range(1, 3);
        }


        isWandering = true;
        yield return new WaitForSeconds(WalkWait);
        isWalking = true;

        yield return new WaitForSeconds(walkTime);
        isWalking = false;

        yield return new WaitForSeconds(rotateWait);
        if (RotateLorR == 1)
        {
            isRotL = true;
            yield return new WaitForSeconds(rotationTime);
            isRotL = false;
        }
        if (RotateLorR == 2)
        {
            isRotR = true;
            yield return new WaitForSeconds(rotationTime);
            isRotR = false;
        }

        isWandering = false;
    }

    private void OnBecameInvisible()
    {
        insight = true;
        DiscartTimer = 15f;
    }
    private void OnBecameVisible()
    {
        insight = false;
    }
}
