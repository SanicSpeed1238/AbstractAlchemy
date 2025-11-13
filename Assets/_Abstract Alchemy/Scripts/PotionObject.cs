using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionEffects currentEffect;
    public PotionEffects[] listOfEffects;
    public MeshRenderer templiquid;

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PotionTargetAbstract>() != null)
        {
            collision.gameObject.GetComponent<PotionTargetAbstract>().AddPotionEffect(currentEffect);

            Instantiate(currentEffect.startFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ObjectRoot>())
        {
            ObjectRoot objRoot = other.GetComponentInParent<ObjectRoot>();
            switch(objRoot.ingredientName)
            {
                case "Processed Red Ingredient":
                    UpdateCurrentPotion(listOfEffects[0]); Destroy(other.gameObject); break;
                case "Processed Yellow Ingredient":
                    UpdateCurrentPotion(listOfEffects[2]); Destroy(other.gameObject); break;
                case "Processed Blue Ingredient":
                    UpdateCurrentPotion(listOfEffects[4]); Destroy(other.gameObject); break;
                default: break;
            }
        }
    }

    private void UpdateCurrentPotion(PotionEffects newEffect)
    {
        currentEffect = newEffect;
        templiquid.material = currentEffect.liquidMaterial;
    }
}