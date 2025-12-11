using UnityEngine;

public class CrossbowProjectile : MonoBehaviour
{
    public GameObject impactVFX;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}