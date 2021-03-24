using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform: MonoBehaviour
{
    [Header("Movement Components")]
    [SerializeField]
    float platformSpeed = 2.5f;
    [SerializeField]
    Vector3 startPosition;
    [SerializeField]
    Vector3 endPosition;
    bool destinationReached = false;

    [Header("Falling Components")]
    [SerializeField]
    bool collapsable;
    [SerializeField]
    float hangTime = 2;
    [SerializeField]
    float resetTime = .5f;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    float returnInterval = 1;
    bool currentlyFalling;

    Vector3 contactPosition;
    Quaternion contactRotation;


    Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        currentlyFalling = false;
    }

    private void Update()
    {
        if (!currentlyFalling)
        {
            if (!destinationReached)
            {
                GoToDestination(startPosition, endPosition);
            }
            if (destinationReached)
            {
                GoToDestination(endPosition, startPosition);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (collapsable)
            {
                if (contactPosition == Vector3.zero)
                {
                    contactPosition = this.transform.position;
                    contactRotation = this.transform.rotation;
                    StartCoroutine(Wait());

                    rb.constraints = RigidbodyConstraints.None;
                    currentlyFalling = true;
                    rb.useGravity = true;
                    rb.isKinematic = false;
                }
            }
        }
    }


    void GoToDestination(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 destinationVector = endPoint - this.transform.position;

        if (destinationVector.magnitude > .1f)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(new Vector3(destinationVector.x, 0, destinationVector.z).normalized * Time.deltaTime * platformSpeed);
            if ((destinationVector).magnitude <= .2f)
            {
                this.transform.position = endPoint;
                destinationReached = !destinationReached;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(hangTime);
        collapsable = true;
        yield return new WaitForSeconds(resetTime);
        // Enable this to return floor to start position
        //this.transform.position = startPosition;
        StartCoroutine(ReturnToStart());
    }

    IEnumerator ReturnToStart()
    {
        Vector3 endPosition = this.transform.position;
        Quaternion endRotation = this.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < returnInterval)
        {
            this.transform.position = Vector3.Lerp(endPosition, contactPosition, curve.Evaluate(elapsedTime / returnInterval));
            this.transform.rotation = Quaternion.Lerp(endRotation, contactRotation, curve.Evaluate(elapsedTime / returnInterval));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        this.transform.position = contactPosition;
        this.transform.rotation = contactRotation;

        rb.constraints = RigidbodyConstraints.FreezePositionY;
        currentlyFalling = false;
        rb.useGravity = false;
        rb.isKinematic = true;
        contactPosition = Vector3.zero;
    }
}
