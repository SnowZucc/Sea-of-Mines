using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public GameObject intText;
    public float interactionDistance;
    public GameObject TPPointBoat;
    public GameObject TPPointExterior;
    private bool isOnBoat = false;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= interactionDistance)
        {
                //Debug.Log("Distance true");

                if (Input.GetKeyDown(KeyCode.E) && !isOnBoat)
                {
                    isOnBoat = true;
                    Debug.Log("Clicked E");
                    InputManager inputManager = player.GetComponent<InputManager>();
                    InputManager inputManagerBoat = GetComponent<InputManager>();

                    player.transform.position = TPPointBoat.transform.position;

                    if (inputManager != null)
                    {
                        inputManager.enabled = false;
                    }
                    else {
                        Debug.Log("No Inputmanager found for player");
                    }

                    if (inputManagerBoat !=null)
                    {
                        inputManagerBoat.enabled = true;
                    }
                    else {
                        Debug.Log("No Inputmanager found for boat");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E) && isOnBoat)
                {
                    isOnBoat = false;
                    Debug.Log("Re-Clicked E");
                    InputManager inputManager = player.GetComponent<InputManager>();
                    InputManager inputManagerBoat = GetComponent<InputManager>();

                    player.transform.position = TPPointExterior.transform.position;
                    
                    if (inputManager != null)
                    {
                        inputManager.enabled = true;
                    }
                    else {
                        Debug.Log("No Inputmanager found for player");
                    }

                    if (inputManagerBoat !=null)
                    {
                        inputManagerBoat.enabled = false;
                    }
                    else {
                        Debug.Log("No Inputmanager found for boat");
                    }
                }
        }

        if (isOnBoat)
        {
            player.transform.position = TPPointBoat.transform.position;
            player.transform.rotation = TPPointBoat.transform.rotation;
        }
    }
}
