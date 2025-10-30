using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoot : MonoBehaviour
{
    [Header("Object Properties")]
    [Tooltip("Whether the object can teleport between shelves")]
    public bool canShelf;
    [Tooltip("Identifier used to determine what kind of ingredient this is. Can be left blank if the object won't be used in any recipes. Dylan's still working on the specifics of how this'll work.")]
    public string ingredientName;
    [Header("Components")]
    [Tooltip("The potion target of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public PotionTargetAbstract potionTarget;
    [Tooltip("The rigid body of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public Rigidbody rigidBody;

    void Awake()
    {
        if (!potionTarget) { potionTarget = GetComponent<PotionTargetAbstract>(); }
        if (!rigidBody) { rigidBody = GetComponent<Rigidbody>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
