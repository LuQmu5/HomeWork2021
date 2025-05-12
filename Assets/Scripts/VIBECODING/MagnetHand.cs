using UnityEngine;

public class MagnetHand : MonoBehaviour
{
    [Header("Magnet Settings")]
    [SerializeField] private float magnetRange = 5f;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float energy = 100f;
    [SerializeField] private float energyDrainPerSecond = 20f;
    [SerializeField] private float energyRechargePerSecond = 10f;
    [SerializeField] private Transform magnetAttachPoint;
    [SerializeField] private LayerMask boxLayer;

    private Rigidbody heldObject;
    private Collider heldCollider;
    private float currentEnergy;
    private bool isDraining;
    private bool isObjectAttached;

    private void Start()
    {
        currentEnergy = energy;
    }

    private void Update()
    {
        bool holding = Input.GetMouseButton(0);

        if (holding && currentEnergy > 0f)
        {
            isDraining = true;
            currentEnergy -= energyDrainPerSecond * Time.deltaTime;
            currentEnergy = Mathf.Max(0f, currentEnergy);

            if (heldObject == null)
            {
                TryMagnetize();
            }
            else if (isObjectAttached)
            {
                heldObject.MovePosition(magnetAttachPoint.position);
            }
        }
        else
        {
            isDraining = false;
            currentEnergy += energyRechargePerSecond * Time.deltaTime;
            currentEnergy = Mathf.Min(energy, currentEnergy);

            if (heldObject != null)
            {
                Release();
            }
        }
    }

    private void TryMagnetize()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRange, boxLayer);

        if (hits.Length == 0) return;

        Collider closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(hit.transform.position, transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = hit;
            }
        }

        if (closest != null && closest.attachedRigidbody != null)
        {
            heldObject = closest.attachedRigidbody;
            heldCollider = closest;
            heldObject.useGravity = false;
            heldObject.isKinematic = true;
            heldCollider.enabled = false;
            StartCoroutine(MoveToAttachPoint(heldObject));
        }
    }

    private System.Collections.IEnumerator MoveToAttachPoint(Rigidbody obj)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.position;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            obj.MovePosition(Vector3.Lerp(startPos, magnetAttachPoint.position, t));
            yield return null;
        }

        obj.MovePosition(magnetAttachPoint.position);
        isObjectAttached = true;
    }

    private void Release()
    {
        isObjectAttached = false;
        heldObject.useGravity = true;
        heldObject.isKinematic = false;
        heldCollider.enabled = true;
        heldObject = null;
        heldCollider = null;
    }

    public float GetEnergyPercent() => currentEnergy / energy;
}
