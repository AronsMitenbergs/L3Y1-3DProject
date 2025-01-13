using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    private Rigidbody theRB; // this is the RigidBody that control everything. Do not touch.

    [Header("Car Attributes")]
    [Range(0,20f)] public float forwardAccel = 8f; 
    [Range(0,20f)] public float reverseAccel = 4f;
    [Range(0,100f)] public float maxSpeed = 50f;
    [Range(0,360f)] public float turnStrength = 180f;
    [Range(0,60f)] public float jumpHeight = 30f;
    [Range(0,20f)] public float gravityForce = 10f;
    [Range(0,5f)] public float dragOnGround = 3f;
    private float speedInput, turnInput;

    [Header("Ground Check")]
    public LayerMask whatisGround;
    private bool grounded;
    public float groundRayLength = 0.5f;
    private Transform groundRayPoint;

    [Header("Wheels")]
    public float maxWheelTurn = 25f;
    public Transform leftFrontWheel, rightFrontWheel;
    

    [Header("Particles")]
    public bool useParticles;
    private ParticleSystem[] dustTrail;
    public float maxEmissionValue = 25f;
    private float emissionRate;
    private GameObject particleHolder;


    void Start()
    {
        theRB = gameObject.GetComponentInChildren<Rigidbody>(); //grabs the Rigidbody in the Sphere that is a child of the main gameObject.
        theRB.transform.parent = null; //moves the sphere out of the parent into the root. 
        groundRayPoint = GameObject.Find("ray point").transform; // finds the raycast point for grounded.
        particleHolder = GameObject.Find("Particle Holder"); // grabs the holder of the particles

        dustTrail = GetComponentsInChildren<ParticleSystem>();

        if (!useParticles)
        {
            particleHolder.SetActive(false);
        }
    }

    void Update()
    {

        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0) 
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0) 
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        if (Input.GetKeyDown("space") && grounded)
        {
            theRB.AddForce(Vector3.up * jumpHeight * 1000f);
        }


        turnInput = Input.GetAxis("Horizontal");
        if (grounded)
        {
            transform.rotation  = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);
        transform.position = theRB.transform.position;
    }

    private void FixedUpdate()
    {

        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatisGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        emissionRate = 0;


        if (grounded){
            theRB.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0) 
            {
                theRB.AddForce(transform.forward * speedInput);

                emissionRate = maxEmissionValue;
            }
        }
        else 
        {
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce * 100f);
        }

        if(useParticles)
        {
            foreach(ParticleSystem part in dustTrail)
            {
                var emissionModule = part.emission;
                emissionModule.rateOverTime = emissionRate;
            }
        }

    }

}
