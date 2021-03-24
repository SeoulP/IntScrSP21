using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPistol: MonoBehaviour, IItem
{
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    float bulletForce;
    [SerializeField]
    float fireInterval = 1;

    [SerializeField]
    GameObject glowstick;
    [SerializeField]
    float throwForce;
    [SerializeField]
    float throwInterval = 1;

    List<GameObject> sticks = new List<GameObject>();
    GameObject stick;

    bool canShoot = true;
    bool canThrow = true;
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
        if(canShoot)
        {
            GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation, null);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
            canShoot = false;
            StartCoroutine(NextShot());
        }
    }

    public void SecondaryUse()
    {
        if (canThrow)
        {
            int numSticks = sticks.Count;
            if (numSticks == 2)
            {
                Destroy(sticks[0]);
                sticks.RemoveAt(0);
            }
            stick = Instantiate(glowstick, firePoint.position, firePoint.rotation, null);
            stick.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
            sticks.Add(stick);
            canThrow = false;
            StartCoroutine(NextThrow());
        }
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

    IEnumerator NextShot()
    {
        yield return new WaitForSeconds(fireInterval);
        canShoot = true;
    }

    IEnumerator NextThrow()
    {
        yield return new WaitForSeconds(throwInterval);
        if (stick != null)
        {
            //Destroy(stick);
        }
        canThrow = true;
    }

}
