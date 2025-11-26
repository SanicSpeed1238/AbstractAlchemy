using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectRootTrigger : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("The collider of the object this component is attached to. If empty, this value will attempt to be set automatically.")]
    public new Collider collider;
    [Header("Requirements")]
    [Tooltip("What potion effects must be applied to the object for it to trigger any events. If set to \"Everything\", it will allow any object regardless of potion effects")]
    public PotionEffects.Effects requiredPotionEffects = (PotionEffects.Effects)~0;
    [Tooltip("What ingredient must the object be for it to trigger any events. If empty, it will allow any object regardless of ingredient name")]
    public string requiredIngredientName;
    [Header("Events")]
    [Tooltip("Triggers when an object matching all requirements enters the trigger")]
    public UnityEvent<GameObject> onObjectEntered;
    [Tooltip("Triggers when an object matching all requirements exits the trigger. In the case that the object is destroyed, this event may return a null GameObject")]
    public UnityEvent<GameObject> onObjectExited;

    private List<ObjectRoot> objectsInTrigger;
    private List<ObjectRoot> correctObjectsInTrigger;

    void Awake()
    {
        if (!collider) { collider = GetComponent<Collider>(); }
    }

    private void Start()
    {
        objectsInTrigger = new List<ObjectRoot>();
        correctObjectsInTrigger = new List<ObjectRoot>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < objectsInTrigger.Count; i++)
        {
            if (!objectsInTrigger[i] || !objectsInTrigger[i].gameObject)
            {
                objectsInTrigger.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < correctObjectsInTrigger.Count; i++)
        {
            if (!correctObjectsInTrigger[i] || !correctObjectsInTrigger[i].gameObject)
            {
                correctObjectsInTrigger.RemoveAt(i);
                onObjectExited.Invoke(null);
                i--;
            }
        }
    }

    private void AddObjectToTrigger(ObjectRoot objectRoot)
    {
        objectsInTrigger.Add(objectRoot);
        if (requiredPotionEffects != (PotionEffects.Effects)~0)
        {
            if (objectRoot.potionTarget)
            {
                objectRoot.potionTarget.OnPotionEffectsChangedEvent += PotionEffectsUpdated;
                PotionEffectsUpdated(objectRoot);
            }
        }
        else if (requiredIngredientName == objectRoot.smashableIdentifier || requiredIngredientName == "")
        {
            AddObjectToCorrectTrigger(objectRoot);
        }
    }

    private void AddObjectToCorrectTrigger(ObjectRoot objectRoot)
    {
        if (!correctObjectsInTrigger.Contains(objectRoot))
        {
            correctObjectsInTrigger.Add(objectRoot);
            onObjectEntered.Invoke(objectRoot.gameObject);
        }
    }

    private void RemoveObjectFromTrigger(ObjectRoot objectRoot)
    {
        objectsInTrigger.Remove(objectRoot);
        if (objectRoot && objectRoot.potionTarget)
        {
            objectRoot.potionTarget.OnPotionEffectsChangedEvent -= PotionEffectsUpdated;
        }
        if (correctObjectsInTrigger.Contains(objectRoot))
        {
            RemoveObjectFromCorrectTrigger(objectRoot);
        }
    }
    
    private void RemoveObjectFromCorrectTrigger(ObjectRoot objectRoot)
    {
        if (correctObjectsInTrigger.Contains(objectRoot))
        {
            correctObjectsInTrigger.Remove(objectRoot);
            onObjectExited.Invoke(objectRoot.gameObject);
        }
    }

    private void PotionEffectsUpdated(ObjectRoot objectRoot)
    {
        if (objectRoot.potionTarget.HasPotionEffect(requiredPotionEffects))
        {
            AddObjectToCorrectTrigger(objectRoot);
        }
        else
        {
            RemoveObjectFromCorrectTrigger(objectRoot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectRoot objectRoot = other.transform.GetComponentInParent<ObjectRoot>();
        if (objectRoot)
        {
            if ((objectRoot.smashableIdentifier == requiredIngredientName || requiredIngredientName == "") && (objectRoot.potionTarget || requiredPotionEffects == (PotionEffects.Effects)~0))
            {
                AddObjectToTrigger(objectRoot);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectRoot objectRoot = other.transform.GetComponentInParent<ObjectRoot>();
        if (objectRoot && objectsInTrigger.Contains(objectRoot))
        {
            RemoveObjectFromTrigger(objectRoot);
        }
    }
}