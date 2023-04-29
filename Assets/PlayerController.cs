using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField]
    [Min(0)]
    private float speed;
    
    [SerializeField]
    [Min(0)]
    private float acceleration;

    [SerializeField]
    [Min(0)]
    private float decceleration;

    [Header("Interaction")]
    [SerializeField]
    private float detectionRange;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
