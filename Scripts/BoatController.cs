using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed = 10.0f;
    private bool isControlled = false;
    private Rigidbody boatRigidbody;

    void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();
    }

void Update()
{
    Debug.Log("Update method called with isControlled: " + isControlled);
    if (isControlled)
    {
        Debug.Log("isControlled condition met");
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        boatRigidbody.velocity = movement * speed;

        Debug.Log("Boat is being controlled");
        Debug.Log("Horizontal input: " + moveHorizontal);
        Debug.Log("Vertical input: " + moveVertical);
    }
}

public void ControlBoat(bool control)
{
    Debug.Log("ControlBoat method called with control: " + control);
    isControlled = control;
    boatRigidbody.isKinematic = !control;
    if (isControlled)
    {
        Debug.Log("Boat control activated");
    }
    else
    {
        Debug.Log("Boat control deactivated");
    }
}
}