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

    // Update is called once per frame
    void Update()
    {
        if (followTarget == null) 
        {
            Debug.LogWarning("WARNING: No Follow Target Assigned.");
            return;
        }

        Vector3 target = followTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, dampening * Time.deltaTime);
    }
}
