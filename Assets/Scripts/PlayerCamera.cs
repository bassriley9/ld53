using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    [Min(0.1f)]
    private float dampening;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Vector3 clamp;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget == null) 
        {
            Debug.LogWarning("WARNING: No Follow Target Assigned.");
            return;
        }

        Vector3 target = followTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, (1 / dampening) * Time.deltaTime);
    }
}
