using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _upwardsModifier = 0.5f;
    [SerializeField] private LayerMask _affectedLayers;

    public void Explode(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius, _affectedLayers);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rigidbody = nearby.attachedRigidbody;

            if (rigidbody == null) 
                continue;

            rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }
}