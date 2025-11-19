using UnityEngine;

public class PotionDroplet : MonoBehaviour
{
    private PotionEffects currentEffects;

    public void SetDropletEffect(PotionEffects effect)
    {
        currentEffects = effect;
        GetComponent<MeshRenderer>().material.color = currentEffects.liquidColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PotionTargetAbstract>() != null)
        {
            collision.gameObject.GetComponent<PotionTargetAbstract>().AddPotionEffect(currentEffects);
            Instantiate(currentEffects.startFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}