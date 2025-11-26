using UnityEngine;

public class PotionObject : MonoBehaviour
{
    [Header("Effects List")]
    public PotionEffects.Effects currentEffects;
    public PotionEffects[] listOfEffects;

    [Header("Important Components")]
    public MeshRenderer liquid;
    public GameObject droplet;

    public ObjectRoot root;
    private bool pouringLiquid;
    private float bottleAngle;       
    private float pourTimer;
    private readonly float pourWaitTime = 1.05f;

    private void Start()
    {
        if (!root) { root = GetComponentInParent<ObjectRoot>(); }
        pourTimer = 0f;
    }

    private void FixedUpdate()
    {
        CheckIfUpsideDown();
        PourLiquid();
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectRoot objRoot = other.GetComponentInParent<ObjectRoot>();
        if (objRoot)
        {
            switch(objRoot.smashableIdentifier)
            {
                case "Processed Red Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(PotionEffects.Effects.Grow); Destroy(objRoot.gameObject); break;
                case "Processed Yellow Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(PotionEffects.Effects.Heavy); Destroy(objRoot.gameObject); break;
                case "Processed Blue Ingredient":
                    /*if (currentEffects != PotionEffects.Effects.None)
                    {
                        Explode();
                        return;
                    }*/
                    UpdateCurrentPotion(PotionEffects.Effects.Hot); Destroy(objRoot.gameObject); break;
                default: break;
            }
        }
    }

    public void UpdateCurrentPotion(PotionEffects.Effects newEffect)
    {
        if (newEffect == PotionEffects.Effects.None)
        {
            liquid.enabled = false;
        }
        else
        {
            liquid.enabled = true;
            liquid.material.UpdateMaterialWithPotionEffect(newEffect);
        }
        currentEffects = newEffect;
        // |= adds the potion effect to the current list. To set the potion effect, just use the normal =
        // You can also do = Effect1 | Effect2 to set it equal to multiple effects at once
    }

    private void CheckIfUpsideDown()
    {
        bottleAngle = Vector3.Dot(root.transform.up, Vector3.up);
        pouringLiquid = bottleAngle < 0f;
    }

    private void PourLiquid()
    {
        if (pouringLiquid) 
        { 
            if (pourTimer <= 0f)
            {
                GameObject pouredDroplet = Instantiate(droplet, transform.position, Quaternion.identity);
                pouredDroplet.GetComponent<PotionDroplet>().SetDropletEffect(currentEffects);

                var renderer = pouredDroplet.GetComponent<Renderer>();
                renderer.material = new Material(liquid.material);
                renderer.material.SetFloat("_Fill", 1);

                pourTimer = pourWaitTime + bottleAngle;
            }
            else
            {
                pourTimer -= Time.fixedDeltaTime;
            }
        }
    }
}