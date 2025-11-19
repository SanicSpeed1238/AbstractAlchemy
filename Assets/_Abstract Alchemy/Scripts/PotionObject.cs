using UnityEngine;

public class PotionObject : MonoBehaviour
{
    [Header("Effects List")]
    public PotionEffects.Effects currentEffects;
    public PotionEffects[] listOfEffects;

    [Header("Important Components")]
    public MeshRenderer liquid;
    public GameObject droplet;

    private Transform rootTransform;
    private bool pouringLiquid;
    private float pouringTime;
    private float pouringTimer;   

    private void Start()
    {
        rootTransform = GetComponentInParent<ObjectRoot>().gameObject.transform;
        pouringTime = 0.2f;
        pouringTimer = 0f;
    }

    private void Update()
    {
        CheckIfUpsideDown();
        PourLiquid();
    }

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

    private void CheckIfUpsideDown()
    {
        float dotProduct = Vector3.Dot(rootTransform.up, Vector3.up);

        if (dotProduct < 0.5f) pouringLiquid = true;
        else pouringLiquid = false;
    }

    private void PourLiquid()
    {
        if(pouringLiquid) Instantiate(droplet, transform.position, Quaternion.identity);
    }
}