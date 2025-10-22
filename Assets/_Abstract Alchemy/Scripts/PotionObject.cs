using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionEffects effect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Susceptible"))
        {
            effect.PotionEffect(collision.gameObject);

            Instantiate(effect.breakFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}