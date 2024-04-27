using UnityEngine;

public class RotationScript : MonoBehaviour
{
    // Vitesse de rotation
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotation de l'objet sur son propre axe Y à la vitesse donnée
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

