using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionEffects effect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PotionTargetAbstract>() != null)
        {
            collision.gameObject.GetComponent<PotionTargetAbstract>().AddPotionEffect(effect);

            Instantiate(effect.startFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}