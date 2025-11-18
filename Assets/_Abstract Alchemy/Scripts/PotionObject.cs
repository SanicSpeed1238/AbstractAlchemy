using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionEffects.Effects currentEffects;
    public PotionEffects[] listOfEffects;
    public MeshRenderer liquid;

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PotionTargetAbstract>() != null)
        {
            collision.gameObject.GetComponent<PotionTargetAbstract>().AddPotionEffect(currentEffects);

            Instantiate(currentEffects.startFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        ObjectRoot objRoot = other.GetComponentInParent<ObjectRoot>();
        if (objRoot)
        {
            switch(objRoot.ingredientName)
            {
                case "Processed Red Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(listOfEffects[0]); Destroy(objRoot.gameObject); break;
                case "Processed Yellow Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(listOfEffects[2]); Destroy(objRoot.gameObject); break;
                case "Processed Blue Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(listOfEffects[4]); Destroy(objRoot.gameObject); break;
                default: break;
            }
        }
    }

    public void UpdateCurrentPotion(PotionEffects newEffect)
    {
        if (currentEffects == PotionEffects.Effects.None)
        {
            liquid.enabled = true;
            liquid.material.SetColor("_MainColor", newEffect.liquidColor);
            liquid.material.SetColor("_MixedColor", newEffect.liquidColor);
        }
        else
        {
            liquid.enabled = true;
            liquid.material.SetColor("_MixedColor", newEffect.liquidColor);
        }
        currentEffects |= newEffect.currentEffect;
        // |= adds the potion effect to the current list. To set the potion effect, just use the normal =
        // You can also do = Effect1 | Effect2 to set it equal to multiple effects at once
    }
}