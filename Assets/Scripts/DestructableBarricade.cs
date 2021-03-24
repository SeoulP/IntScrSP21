using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBarricade : MonoBehaviour
{
    [SerializeField]
    int doorHealth = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            doorHealth--;

            Destroy(collision.gameObject);

            Destroy(this.gameObject.transform.GetChild(Random.Range(0, this.gameObject.transform.childCount)).gameObject);

            if (doorHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
