using UnityEngine;

public class PotionDroplet : MonoBehaviour
{
    private PotionEffects.Effects currentEffects;
    public PotionEffects[] listOfEffects;

    public void SetDropletEffect(PotionEffects.Effects effect)
    {
        currentEffects = effect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectRoot objectRoot = collision.gameObject.GetComponentInParent<ObjectRoot>();
        if (objectRoot && objectRoot.potionTarget)
        {
            if (currentEffects.HasFlag(PotionEffects.Effects.Grow) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Grow))
            {
                Instantiate(listOfEffects[0].startFX, transform.position, Quaternion.identity);
            }
            if (currentEffects.HasFlag(PotionEffects.Effects.Shrink) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Shrink))
            {
                Instantiate(listOfEffects[1].startFX, transform.position, Quaternion.identity);
            }
            if (currentEffects.HasFlag(PotionEffects.Effects.Heavy) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Heavy))
            {
                Instantiate(listOfEffects[2].startFX, transform.position, Quaternion.identity);
            }
            if (currentEffects.HasFlag(PotionEffects.Effects.Light) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Light))
            {
                Instantiate(listOfEffects[3].startFX, transform.position, Quaternion.identity);
            }
            if (currentEffects.HasFlag(PotionEffects.Effects.Hot) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Hot))
            {
                Instantiate(listOfEffects[4].startFX, transform.position, Quaternion.identity);
            }
            if (currentEffects.HasFlag(PotionEffects.Effects.Cold) && !objectRoot.potionTarget.HasPotionEffect(PotionEffects.Effects.Cold))
            {
                Instantiate(listOfEffects[5].startFX, transform.position, Quaternion.identity);
            }
            objectRoot.potionTarget.AddPotionEffect(currentEffects);
        }

        Destroy(gameObject);
    }
}