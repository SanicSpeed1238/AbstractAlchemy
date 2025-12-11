using UnityEngine;

public class PotionTargetDog : PotionTargetAbstract
{
    [Header("Size Effect")]
    [Range(0.1f, 1f)]
    public float shrinkMultiplier = 0.5f;
    [Range(1f, 3f)]
    public float growMultiplier = 2f;

    public GameObject normalDog;
    public GameObject smallDog;
    public GameObject bigDog;

    [Header("Weight Effect")]
    [Range(1f, 2f)]
    public float lightMultiplier = 1f;
    [Range(0.1f, 1f)]
    public float heavyMultiplier = 1f;

    [Header("Temperature Effect")]
    [Range(0f, 1f)]
    public float coldMultiplier = 1f;
    protected readonly float defaultFrictionValue = 1f;
    [Range(0f, 1f)]
    public float hotMultiplier = 0f;
    protected readonly float defaultBouncinessValue = 0f;

    protected override void OnPotionEffectsAdded(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= shrinkMultiplier;
            normalDog.SetActive(false);
            smallDog.SetActive(true);
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale *= growMultiplier;
            normalDog.SetActive(false);
            bigDog.SetActive(true);
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            if (objectRoot.XRGrabInteractable)
            {
                //objectRoot.XRGrabInteractable.throwVelocityScale *= lightMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                //objectRoot.XRGrabInteractable.throwVelocityScale *= heavyMultiplier;
            }
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                //objectRoot.collider.material.staticFriction = coldMultiplier;
                //objectRoot.collider.material.dynamicFriction = coldMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                //objectRoot.collider.material.bounciness = hotMultiplier;
            }
        }
    }
    protected override void OnPotionEffectsRemoved(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale /= shrinkMultiplier;
            smallDog.SetActive(false);
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale /= growMultiplier;
            bigDog.SetActive(false);
        }
        if (!effects.HasFlag(PotionEffects.Effects.Shrink) && !effects.HasFlag(PotionEffects.Effects.Grow))
        {
            normalDog.SetActive(true);
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            if (objectRoot.XRGrabInteractable)
            {
                //objectRoot.XRGrabInteractable.throwVelocityScale /= lightMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                //objectRoot.XRGrabInteractable.throwVelocityScale /= heavyMultiplier;
            }
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                //objectRoot.collider.material.staticFriction = defaultFrictionValue;
                //objectRoot.collider.material.dynamicFriction = defaultFrictionValue;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                //objectRoot.collider.material.bounciness = defaultBouncinessValue;
            }
        }
    }
}