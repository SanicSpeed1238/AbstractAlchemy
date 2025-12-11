using UnityEngine;

public class CrossbowProjectile : MonoBehaviour
{
    public Material projectileMaterial;
    public GameObject impactVFX;

    public void SetProjectileProperties(Material mat, GameObject vfx)
    {
        if (mat != null)
        {
            GetComponent<MeshRenderer>().material = mat;
        }

        if (vfx != null)
        {
            impactVFX = vfx;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(impactVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}