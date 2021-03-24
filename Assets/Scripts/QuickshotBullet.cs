using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickshotBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().heldItem != null)
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
