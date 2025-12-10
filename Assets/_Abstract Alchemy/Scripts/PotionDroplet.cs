using System;
using UnityEngine;

public class PotionDroplet : MonoBehaviour
{
    private PotionEffects.Effects currentEffects;
    [NonSerialized]
    public PotionObject sourcePotionObject;
    private float defaultScale;

    public Rigidbody rigidBody;
    public Vector3 maxStretchSizeMult;
    public float maxStretchVelocity;

    private void Start()
    {
        defaultScale = transform.localScale.x;
    }
    public void SetDropletEffect(PotionEffects.Effects effect)
    {
        currentEffects = effect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Initial Application of Potion Effect

        ObjectRoot objectRoot = collision.gameObject.GetComponentInParent<ObjectRoot>();
        if (objectRoot && objectRoot.potionTarget)
        {
            foreach (var item in currentEffects.GetScriptableObjects())
            {
                if (!objectRoot.potionTarget.HasPotionEffect(item.currentEffect))
                {
                    Instantiate(item.startFX, transform.position, Quaternion.identity);
                    if (sourcePotionObject)
                    {
                        sourcePotionObject.UpdateCurrentPotion(PotionEffects.Effects.None);
                    }
                    objectRoot.potionTarget.AddPotionEffect(currentEffects);
                }               
            }     
        }

        #endregion

        #region Simple Splash VFX

        foreach (var item in currentEffects.GetScriptableObjects())
        {
            Instantiate(item.dropletFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

        #endregion
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(Vector3.one * defaultScale, defaultScale * maxStretchSizeMult, rigidBody.velocity.magnitude / maxStretchVelocity);
    }
}