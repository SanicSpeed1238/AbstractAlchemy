using System;
using UnityEngine;

public class CleanserDroplet : MonoBehaviour
{
    private float defaultScale;

    public Rigidbody rigidBody;
    public Vector3 maxStretchSizeMult;
    public float maxStretchVelocity;

    public GameObject applyEffect;
    public GameObject dropletEffect;

    private void Start()
    {
        defaultScale = transform.localScale.x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Initial Application of Potion Effect

        ObjectRoot objectRoot = collision.gameObject.GetComponentInParent<ObjectRoot>();
        if (objectRoot && objectRoot.potionTarget && objectRoot.potionTarget.currentEffects != PotionEffects.Effects.None)
        {
            Instantiate(applyEffect, transform.position, Quaternion.identity);
            objectRoot.potionTarget.ClearPotionEffects();
        }

        #endregion

        #region Simple Splash VFX

        Instantiate(dropletEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        #endregion
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(Vector3.one * defaultScale, defaultScale * maxStretchSizeMult, rigidBody.velocity.magnitude / maxStretchVelocity);
    }
}