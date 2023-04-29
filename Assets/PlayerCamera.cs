using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    private float dampening;

    [SerializeField]
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget == null) 
        {
            Debug.LogWarning("WARNING: No Follow Target Assigned.");
        }
    }
}
