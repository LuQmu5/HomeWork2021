using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 700f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float upwardsModifier = 0.5f;
    [SerializeField] private LayerMask affectedLayers;

    public void Explode(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, explosionRadius, affectedLayers);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.attachedRigidbody;
            if (rb == null) continue;

            rb.AddExplosionForce(explosionForce, position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}