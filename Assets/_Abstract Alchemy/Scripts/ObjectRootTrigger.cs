using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectRootTrigger : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The collider of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public new Collider collider;
    [Header("Requirements")]
    [Tooltip("What potion effects must be applied to the object for it to trigger any events")]
    public PotionEffects.Effects requiredPotionEffects;
    [Tooltip("What ingredient must the object be for it to trigger any events")]
    public string requiredIngredientName;
    [Header("Events")]
    public UnityEvent onObjectEntered;
    public UnityEvent onObjectExited;

    void Awake()
    {
        if (!collider) { collider = GetComponent<Collider>(); }
    }

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}