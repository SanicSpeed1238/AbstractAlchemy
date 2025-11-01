using UnityEngine;

[CreateAssetMenu(fileName = "New Object Conversion", menuName = "Recipes")]
public class RecipeObjectConversion : ScriptableObject
{
    public string inputIngredientName;
    public GameObject outputPrefab;

    public static GameObject ConvertObjectToOtherObject(ObjectRoot input, GameObject outputPrefab)
    {
        PotionEffects.Effects potionEffects = PotionEffects.Effects.None;
        if (input.potionTarget)
        {
            potionEffects = input.potionTarget.GetPotionEffects();
        }
        GameObject converted = GameObject.Instantiate(outputPrefab, input.transform.position, input.transform.rotation);
        if (potionEffects != PotionEffects.Effects.None && converted.TryGetComponent<PotionTargetAbstract>(out PotionTargetAbstract potionTarget))
        {
            potionTarget.AddPotionEffect(potionEffects);
        }
        GameObject.Destroy(input.gameObject);
        return converted;
    }
}