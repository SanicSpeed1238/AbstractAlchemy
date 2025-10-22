using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Potion Effect")]
public class PotionEffects : ScriptableObject
{
    public enum Effects
    {
        None,
        Shrink,
    }
    public Effects currentEffect;

    [Header("VFX")]
    public GameObject potionFX;
    public GameObject breakFX;

    public void PotionEffect(GameObject other)
    {
        Instantiate(potionFX, other.transform.position, Quaternion.identity, other.transform);
        
        switch(currentEffect)
        {
            case Effects.None:
                break;
            case Effects.Shrink:
                EffectShrink(other); break;
            default:
                break;
        }
    }

    #region Effect Functions

    private void EffectShrink(GameObject other)
    {
        other.transform.localScale = new(.5f, .5f, .5f);
    }

    #endregion
}