﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // This is the players hand
    [SerializeField]
    Transform hand;

    IItem heldItem;
    public int oxygenSupply = 0;
    public int coins = 0;
    bool crouch = false;


    // Update is called once per frame
    void Update()
    {
        crouching();

        if(Input.GetButton("Fire1"))
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

        if (heldItem == null)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                heldItem = other.GetComponent<IItem>();
                heldItem.Pickup(hand);
            }
        }
        else
        {
            Debug.Log("You already have an item!");
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
