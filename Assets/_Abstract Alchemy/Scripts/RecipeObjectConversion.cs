using UnityEngine;

[CreateAssetMenu(fileName = "New Object Conversion", menuName = "Recipes")]
public class RecipeObjectConversion : ScriptableObject
{
    public string inputIngredientName;
    public GameObject outputPrefab;
    public GameObject vfxPrefab;

    public static GameObject ConvertObjectToOtherObject(ObjectRoot input, GameObject outputPrefab, GameObject vfxPrefab)
    {
        return ConvertObjectToOtherObject(input, outputPrefab, vfxPrefab, input.transform.position, input.transform.rotation);
    }
    public static GameObject ConvertObjectToOtherObject(ObjectRoot input, GameObject outputPrefab, GameObject vfxPrefab, Vector3 position, Quaternion rotation)
    {
        PotionEffects.Effects potionEffects = PotionEffects.Effects.None;
        if (input.potionTarget)
        {
            potionEffects = input.potionTarget.GetPotionEffects();
        }
        GameObject.Destroy(input.gameObject);
        if (vfxPrefab)
        {
            GameObject.Instantiate(vfxPrefab, position, rotation);
        }
        if (outputPrefab)
        {
            GameObject converted = GameObject.Instantiate(outputPrefab, position, rotation);
            if (potionEffects != PotionEffects.Effects.None && converted.TryGetComponent<PotionTargetAbstract>(out PotionTargetAbstract potionTarget))
            {
                potionTarget.AddPotionEffect(potionEffects);
            }
            return converted;
        }
        return null;
    }
}