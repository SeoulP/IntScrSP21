using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // This function will be called from the playerController
    public void Pickup(Transform hand)
    {
            // Set the parent of the "weapon" to the hand.
            gameObject.transform.SetParent(hand);
            // Sets the location of the gun to the hand's vector.
            transform.localPosition = Vector3.zero;
            // Will rotate the weapon.
            transform.localRotation = Quaternion.identity;
            // Kinematic turns the physics off.
            GetComponent<Rigidbody>().isKinematic = true;
            // Turns off the collider for the weapon.
            GetComponent<Collider>().enabled = false;
    }

    public void Use()
    {
        Debug.Log("<color=red>Pow!</color>");
    }

    public void Drop()
    {
        Debug.Log("Dropping our item!");
        this.gameObject.transform.SetParent(null);
        transform.Translate(0,0,2);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        GetComponent<Collider>().enabled = true;
    }

}
