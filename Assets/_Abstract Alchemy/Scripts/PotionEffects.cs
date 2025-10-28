using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Potion Effect")]
public class PotionEffects : ScriptableObject
{
    public enum Effects
    {
        None = 0,
        Grow = 1,
        Shrink = 2,
        Heavy = 4,
        Light = 8,
        Hot = 16,
        Cold = 32
    }
    [Header("Effect")]
    public Effects currentEffect;
    public Effects conflictingEffect;

    [Header("VFX")]
    public GameObject potionFX;
    public GameObject breakFX;

    public void PotionEffect(GameObject other)
    {
        if (other.TryGetComponent<PotionTargetAbstract>(out PotionTargetAbstract potionTarget))
        {
            Instantiate(potionFX, other.transform.position, Quaternion.identity, other.transform);

            potionTarget.AddPotionEffect(this);
        }
    }
}