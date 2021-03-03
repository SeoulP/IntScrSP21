using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterGun : MonoBehaviour, IItem
{
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    float bulletForce;

    [SerializeField]
    GameObject teleDisc;

    [SerializeField]
    float fireInterval = 1;

    bool canShoot = true;
    GameObject bullet;
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
        if (canShoot)
        {
            bullet = Instantiate(teleDisc, firePoint.position, firePoint.rotation, null);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
            canShoot = false;
            StartCoroutine(Wait());
        }
    }

    public void SecondaryUse()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = bullet.transform.position;
        Destroy(bullet);
        StartCoroutine(Wait());
    }

    public void Drop()
    {
        Debug.Log("Dropping our item!");
        this.gameObject.transform.SetParent(null);
        transform.Translate(0, 0, 2);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        GetComponent<Collider>().enabled = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(fireInterval);
        canShoot = true;
    }
}
