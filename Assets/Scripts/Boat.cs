using UnityEngine;
using Crest;
public class Boat : MonoBehaviour
{
    public GameObject intText;
    public float interactionDistance;
    public GameObject TPPointBoat;
    public GameObject TPPointExterior;
    private bool isOnBoat = false;
    public AudioClip splashSoundEffect;
    public AudioClip woodSoundEffect;

    // Update is called once per frame
    void Update()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance <= interactionDistance)
        {
                //Debug.Log("Distance true");

                if (Input.GetKeyDown(KeyCode.E) && !isOnBoat)
                {
                    isOnBoat = true;
                    audioSource.PlayOneShot(woodSoundEffect);
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
                    audioSource.PlayOneShot(splashSoundEffect);
                    Debug.Log("Re-Clicked E");
                    InputManager inputManager = player.GetComponent<InputManager>();
                    InputManager inputManagerBoat = GetComponent<InputManager>();

                    SimpleFloatingObject simpleFloatingObject = GetComponent<SimpleFloatingObject>();
                    simpleFloatingObject.enabled = false;
                    player.transform.position = TPPointExterior.transform.position;
                    simpleFloatingObject.enabled = true;
                    
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
