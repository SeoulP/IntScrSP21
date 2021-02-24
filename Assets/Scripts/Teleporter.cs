using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    Transform destination;

    static bool recharged = true;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (recharged)
            {
                other.transform.position = destination.position;
                other.transform.Translate(Vector3.up);
                recharged = false;
                StartCoroutine(Wait());
            }
            
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        recharged = true;
    }
}
