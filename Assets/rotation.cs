using UnityEngine;

public class RotationWithLightParticles : MonoBehaviour
{
    // Vitesse de rotation de l'objet
    public float rotationSpeed = 50f;

    // Prefab du système de particules de lumière
    public ParticleSystem lightParticlePrefab;

    // Référence au système de particules de lumière
    private ParticleSystem lightParticles;

    void Start()
    {
        // Instanciation du prefab de particules de lumière
        lightParticles = Instantiate(lightParticlePrefab, transform.position, Quaternion.identity, transform);
        lightParticles.Stop(); // Les particules ne démarrent pas automatiquement
    }

    void Update()
    {
        // Rotation de l'objet sur son propre axe Y à la vitesse donnée
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Démarrage des particules de lumière lorsque l'objet commence à tourner
        if (!lightParticles.isPlaying)
        {
            lightParticles.Play();
        }
    }
}

