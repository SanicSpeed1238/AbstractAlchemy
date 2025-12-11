using UnityEngine;

/// <summary>
/// Launch projectile by directly setting its velocity.
/// </summary>
public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform startPoint;
    public float launchSpeed = 1.0f;

    public void Fire()
    {
        GameObject newObject = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation);

        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            Launch(rigidBody);
    }

    private void Launch(Rigidbody rigidBody)
    {
        rigidBody.velocity = startPoint.forward * launchSpeed;
    }
}