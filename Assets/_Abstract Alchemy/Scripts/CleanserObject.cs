using UnityEngine;

public class CleanserObject : MonoBehaviour
{
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
                CleanserDroplet potionDroplet = pouredDroplet.GetComponent<CleanserDroplet>();
                var renderer = pouredDroplet.GetComponent<Renderer>();
                renderer.material = new Material(liquid.material);
                renderer.material.SetFloat("_Fill", 1);
                Collider dropletCollider = pouredDroplet.GetComponent<Collider>();
                Physics.IgnoreCollision(dropletCollider, root.collider);

                pourTimer = pourWaitTime + bottleAngle;
            }
            else
            {
                pourTimer -= Time.fixedDeltaTime;
            }
        }
    }
}