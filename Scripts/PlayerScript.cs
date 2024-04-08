// PlayerController.cs
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public BoatController boatController;
    public Transform boatPosition;
    private bool isOnBoat = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if (hit.transform.gameObject == boatController.gameObject)
                {
                    isOnBoat = true;
                    boatController.ControlBoat(true);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isOnBoat)
        {
            transform.position = boatPosition.position;
        }
    }
}