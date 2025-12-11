using UnityEngine;

public class PotionTargetDefault : PotionTargetAbstract
{
    [Header("Size Effect")]
    [Range(0.1f, 1f)]
    public float shrinkMultiplier = 0.5f;
    [Range(1f, 3f)]
    public float growMultiplier = 2f;

    [Header("Weight Effect")]
    [Range(1f, 3f)]
    public float lightMultiplier = 2f;
    [Range(0.1f, 1f)]
    public float heavyMultiplier = 0.5f;

    [Header("Temperature Effect")]
    [Range(0f, 0.5f)]
    public float coldMultiplier = 0.2f;
    protected readonly float defaultFrictionValue = 0.8f;
    [Range(0.1f, 1f)]
    public float hotMultiplier = 0.5f;
    protected readonly float defaultBouncinessValue = 0f;

    protected override void OnPotionEffectsAdded(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= shrinkMultiplier;
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale *= growMultiplier;
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale *= lightMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale *= heavyMultiplier;
            }
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                objectRoot.collider.material.staticFriction = coldMultiplier;
                objectRoot.collider.material.dynamicFriction = coldMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                objectRoot.collider.material.bounciness = hotMultiplier;
            }
        }
    }
    protected override void OnPotionEffectsRemoved(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale /= shrinkMultiplier;
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale /= growMultiplier;
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale /= lightMultiplier;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale /= heavyMultiplier;
            }
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                objectRoot.collider.material.staticFriction = defaultFrictionValue;
                objectRoot.collider.material.dynamicFriction = defaultFrictionValue;
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            if (objectRoot.collider && objectRoot.collider.material)
            {
                objectRoot.collider.material.bounciness = defaultBouncinessValue;
            }
        }
    }
}