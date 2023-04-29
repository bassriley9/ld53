using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]

    [SerializeField]
    private InputActionAsset controls;

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
    [Min(0)]
    private float interactHoldTime;

    [SerializeField]
    [Min(0)]
    private float throwHoldTime;

    [SerializeField]
    [Min(0)]
    private float maxThrowForce;

    private Rigidbody rb;
    private CapsuleCollider col;
    private LayerMask groundLayerMask;
    private float collisionThreshold = 0.01f;

    private InputAction moveAction;
    private InputAction interactAction;

    private float normalAngle;
    private float throwForce;
    private float interactTime;
    private Vector2 moveInputs;
    private Vector3 normal;
    private Sheep sheepHeld;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        groundLayerMask = LayerMask.NameToLayer("Ground");

        Setup();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
        Move();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void Setup() 
    {
        InputActionMap input = controls.FindActionMap("Player");

        moveAction = input.FindAction("Move");
        interactAction = input.FindAction("Throw / Interact");

        moveAction.performed += OnMove; 
        moveAction.canceled += OnMove;
        interactAction.performed += OnInteractPress;
        interactAction.canceled += OnInteractRelease;

        Enable();
    }

    void Enable() 
    {
        moveAction.Enable();
        interactAction.Enable();
    }

    void Disable() 
    {
        moveAction.Disable();
        interactAction.Disable();
    }

    void Dispose() 
    {
        Disable();
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        interactAction.performed -= OnInteractPress;
        interactAction.canceled -= OnInteractRelease;

        moveAction.Dispose();
        interactAction.Dispose();
    }

    void CheckCollisions() 
    {
        // Ground
        RaycastHit hit;
        float distance = 2 * collisionThreshold;
        Vector3 origin = transform.position + (col.radius + collisionThreshold) * Vector3.up;
        if (Physics.SphereCast(origin, col.radius, Vector3.down, out hit, distance, groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            Vector3 contactPoint = origin + (Vector3.down * distance);
            Vector3 contactDirection = hit.point - origin;
            float contactOffset = contactDirection.magnitude + distance;

            // Slopes
            RaycastHit spherePoint;
            if (Physics.Raycast(contactPoint, contactDirection, out spherePoint, contactOffset, groundLayerMask))
            {
                normal = spherePoint.normal;
                normalAngle = Vector3.Angle(normal, Vector3.up);
            }
        }

        // Interactables
        Collider[] cols = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, transform.position.y + col.height, transform.position.z), col.radius);

        foreach (Collider col in cols) 
        {
            tag = col.tag;

            if (tag == "Sheep" && !sheepHeld) 
            {
                Pickup(col.GetComponent<Sheep>());
            } else if (tag == "Pen" && sheepHeld) 
            {
                Drop();
            }
        }
    }

    void OnMove(InputAction.CallbackContext context) 
    {
        moveInputs = context.ReadValue<Vector2>();
    }

    void OnInteractPress(InputAction.CallbackContext context) 
    {
        // throw
        if (sheepHeld) 
        {
            interactTime += Time.deltaTime;
            float throwProgress = (interactTime / throwHoldTime);
            throwForce = throwProgress * maxThrowForce;

            if (throwProgress >= 1f)
                Throw();
        }

        // sell / interact
        else if (true) 
        {
            interactTime += Time.deltaTime;
            float interactProgress = (interactTime / interactHoldTime);

            if (interactProgress >= 1f)
                Interact();
        }
    }

    void OnInteractRelease(InputAction.CallbackContext context) 
    {
        if (sheepHeld) 
        {
            Throw();
        }

        interactTime = 0;
    }

    void Move()
    {
        float a;
        Vector3 direction = new Vector3(moveInputs.x, 0, moveInputs.y);

        if (direction.magnitude > 0.1f)
        {
            a = acceleration;

            // rotate
            rb.MoveRotation(Quaternion.LookRotation(direction, transform.up));
        } else
        {
            a = decceleration;
        }

        // move
        Vector3 velocity = speed * Vector3.ProjectOnPlane(transform.forward, normal) * moveInputs.magnitude;
        rb.velocity = Vector3.Lerp(rb.velocity, velocity, a * 10 * Time.deltaTime);

        // vfx
        if (rb.velocity.magnitude > speed * 0.8f) 
        {
        
        }
    }

    void Interact() 
    {
    
    }

    void Pickup(Sheep sheep)
    {
        //sheep.SetActive(false);
        sheep.transform.parent = transform;
        sheepHeld = sheep;
    }

    void Throw() 
    {
        sheepHeld.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Drop();

        throwForce = 0;
    }

    void Drop() 
    {
        //sheepHeld.SetActive(true);
        sheepHeld = null;
    }
}
