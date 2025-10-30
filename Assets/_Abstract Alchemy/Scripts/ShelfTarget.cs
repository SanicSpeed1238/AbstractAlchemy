using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("ShelfTarget should be replaced with ObjectRoot with the canShelf bool set to true", true)]
public class ShelfTarget : MonoBehaviour
{
    [Tooltip("The rigid body of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        if (!rigidBody)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
