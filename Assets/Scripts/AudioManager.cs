using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip gemPickup;

    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I hit " + other.gameObject.name);
        if(other.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("Ding!");
            aud.pitch = Random.Range(1.0f, 1.2f);
            aud.PlayOneShot(gemPickup);
        }
    }
}
