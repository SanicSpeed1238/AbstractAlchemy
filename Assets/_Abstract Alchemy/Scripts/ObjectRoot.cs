using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectRoot : MonoBehaviour
{
    [Header("Basic Components")]
    [Tooltip("The potion target of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public PotionTargetAbstract potionTarget;
    [Tooltip("The XR grab interactable of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public XRGrabInteractable XRGrabInteractable;
    [Tooltip("The rigid body of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public Rigidbody rigidBody;
    [Tooltip("The collider of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public new Collider collider;
    [Tooltip("The renderer of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public new Renderer renderer;  

    [Header("Special Properties")]
    [Tooltip("Identifier used to determine what kind of ingredient/smashable object this is. Be sure to use consistent naming.")]
    public string smashableIdentifier;
    [Tooltip("Whether the object can teleport between shelves")]
    public bool canShelf;

    void Awake()
    {
        if (!potionTarget) { potionTarget = GetComponent<PotionTargetAbstract>(); }
        if (!rigidBody) { rigidBody = GetComponent<Rigidbody>(); }
        if (!XRGrabInteractable) { XRGrabInteractable = GetComponent<XRGrabInteractable>(); }
        if (!renderer) { renderer = GetComponent<Renderer>(); }
        if (!collider) { collider = GetComponentInChildren<Collider>(); }
    }

    public void OnDestroy()
    {
        if (canShelf && Shelf.objectsInShelf.Contains(this))
        {
            Shelf.objectsInShelf.Remove(this);
        }
    }
}