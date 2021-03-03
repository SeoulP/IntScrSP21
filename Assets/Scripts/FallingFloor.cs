using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingFloor : MonoBehaviour
{
    [SerializeField]
    float hangTime = 2;
    [SerializeField]
    float resetTime = .5f;

    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    float returnInterval = 1;

    Vector3 startPosition;
    Quaternion startRotation; 

    Rigidbody rb;
    Renderer rend;


    bool hasActivated = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rend = this.GetComponent<Renderer>();
        rb.isKinematic = true;  // Freezes the platform

        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Wait());
            if(hasActivated)
            {
                rb.isKinematic = false;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(hangTime);
        hasActivated = true;

        rend.material.color += new Color(.1f, 0, .01f);
        yield return new WaitForSeconds(resetTime);
        // Enable this to return floor to start position
        //this.transform.position = startPosition;
        StartCoroutine(ReturnToStart());
        rb.isKinematic = true;
    }

    IEnumerator ReturnToStart()
    {
        Vector3 endPosition = this.transform.position;
        Quaternion endRotation = this.transform.rotation;
        float elapsedTime = 0f;

        while(elapsedTime < returnInterval)
        {
            this.transform.position = Vector3.Lerp(endPosition, startPosition, curve.Evaluate(elapsedTime / returnInterval));
            this.transform.rotation = Quaternion.Lerp(endRotation, startRotation, curve.Evaluate(elapsedTime / returnInterval));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
}
