using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    GameObject lockedDoor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().coins >= 10)
            {
                Debug.Log("You have 10 sapphires!");
                lockedDoor.transform.position -= new Vector3(0, 2.5f, 0);
                other.GetComponent<PlayerController>().coins -= 10;
            }
            else
            {
                Debug.Log("You don't have enough sapphires!");
            }
        }
    }
}
