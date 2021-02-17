using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // This is the players hand
    [SerializeField]
    Transform hand;

    Gun heldItem;
    public int oxygenSupply = 0;
    public int coins = 0;
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        crouching();

        if(Input.GetButtonDown("Fire1"))
        {
            if(heldItem != null)
            {
                heldItem.Use();
            }
            else
            {
                Debug.Log("We aren't holding anything!");
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(heldItem != null)
            {
                heldItem.Drop();
                heldItem = null;
            }
            else
            {
                Debug.Log("We aren't holding anything!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("I have hit " + other.gameObject.name + "!");

        if (other.gameObject.CompareTag("Weapon"))
        {
            heldItem = other.GetComponent<Gun>();
            heldItem.Pickup(hand);
        }

        if(other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            oxygenSupply++;
            coins += 1;
        }

        if (other.gameObject.CompareTag("Floor")) {
            other.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;

        }
    }
    void crouching()
    {
        if (Input.GetKeyDown(KeyCode.C) && crouch == false)
        {
            transform.localScale = new Vector3(1, .5f, 1);
            crouch = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && crouch == true)
        {
            transform.localScale = new Vector3(1, 1, 1);
            crouch = false;
        }
    }
}
