using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionTargetSword : PotionTargetAbstract
{
    [Header("Size Effect")]
    [Range(0.1f, 1f)]
    public float shrinkMultiplier = 0.5f;
    [Range(1f, 2f)]
    public float growMultiplier = 1.5f;

    [Header("Weight Effect")]
    [Range(1f, 10f)]
    public float lightMultiplier = 5f;
    [Range(0.1f, 1f)]
    public float heavyMultiplier = 0.5f;

    private GameObject swordObject;
    public GameObject regularSword;
    public GameObject lightSword;
    public GameObject heavySword;

    [Header("Temperature Effect")]
    [Range(0f, 0.5f)]
    public float coldMultiplier = 0.2f;
    protected readonly float defaultFrictionValue = 0.8f;
    [Range(0.1f, 1f)]
    public float hotMultiplier = 0.5f;
    protected readonly float defaultBouncinessValue = 0f;

    public override void Start()
    {    
        base.Start();
        swordObject = objectRoot.transform.Find("SwordObject").gameObject;
    }

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
                Destroy(swordObject.transform.GetChild(0).gameObject);
                SpawnNewSword(lightSword);
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale *= heavyMultiplier;
                Destroy(swordObject.transform.GetChild(0).gameObject);
                SpawnNewSword(heavySword);
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
                Destroy(swordObject.transform.GetChild(0).gameObject);
            }
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            if (objectRoot.XRGrabInteractable)
            {
                objectRoot.XRGrabInteractable.throwVelocityScale /= heavyMultiplier;
                Destroy(swordObject.transform.GetChild(0).gameObject);
            }
        }
        if (!effects.HasFlag(PotionEffects.Effects.Light) && !effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            SpawnNewSword(regularSword);
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

    private void SpawnNewSword(GameObject swordType)
    {
        GameObject newSword = Instantiate(swordType, objectRoot.transform.position, Quaternion.identity, swordObject.transform);

        objectRoot.rigidBody = newSword.GetComponent<Rigidbody>();
        objectRoot.collider = newSword.GetComponent<MeshCollider>();
        objectRoot.renderer = newSword.GetComponent<MeshRenderer>();
        objectRoot.XRGrabInteractable = newSword.GetComponent<XRGrabInteractable>();

        currentVFX.transform.parent = newSword.transform;
    }
}