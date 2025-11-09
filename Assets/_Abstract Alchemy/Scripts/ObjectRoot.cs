using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectRoot : MonoBehaviour
{
    [Header("Object Properties")]
    [Tooltip("Whether the object can teleport between shelves")]
    public bool canShelf;
    [Tooltip("Identifier used to determine what kind of ingredient this is. Can be left blank if the object won't be used in any recipes.\n\nExamples names for consistency:\nRed Ingredient\nBlue Ingredient\nProcessed Red Ingredient")]
    public string ingredientName;
    [Header("Components")]
    [Tooltip("The potion target of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public PotionTargetAbstract potionTarget;
    [Tooltip("The rigid body of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public Rigidbody rigidBody;
    [Tooltip("The XR grab interactable of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public XRGrabInteractable xrGrabInteractable;
    [Tooltip("The renderer of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public new Renderer renderer;

    void Awake()
    {
        if (!potionTarget) { potionTarget = GetComponent<PotionTargetAbstract>(); }
        if (!rigidBody) { rigidBody = GetComponent<Rigidbody>(); }
        if (!xrGrabInteractable) { xrGrabInteractable = GetComponent<XRGrabInteractable>(); }
        if (!renderer) { renderer = GetComponent<Renderer>(); }
    }

    public void OnDestroy()
    {
        if (canShelf && Shelf.objectsInShelf.Contains(this))
        {
            Shelf.objectsInShelf.Remove(this);
        }
    }
}
