using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mallet : MonoBehaviour
{
    public new Collider collider;
    public RecipeObjectConversion[] recipes;
    void Awake()
    {
        if (!collider) { collider = GetComponent<Collider>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 2f)
        {
            // All swinging requirements met, now check if it is a recipe thing
            ObjectRoot root = collision.gameObject.GetComponentInParent<ObjectRoot>(false);
            if (root)
            {
                foreach (var item in recipes)
                {
                    if (root.smashableIdentifier == item.inputIngredientName)
                    {
                        RecipeConvertItem(root, item);
                        break;
                    }
                }
            }
        }
    }

    public void RecipeConvertItem(ObjectRoot root, RecipeObjectConversion recipe)
    {
        Collider outputCollider = RecipeObjectConversion.ConvertObjectToOtherObject(root, recipe.outputPrefab, recipe.vfxPrefab, root.transform.position, Quaternion.identity).GetComponentInChildren<Collider>();
        if (outputCollider)
        {
            Physics.IgnoreCollision(outputCollider, this.collider);
        }

    }
}
