using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mallet : MonoBehaviour
{
    public Recipe[] recipes;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectRoot root = collision.gameObject.GetComponentInParent<ObjectRoot>();
        if (root)
        {
            foreach (var item in recipes)
            {
                if (root.ingredientName == item.inputIngredientName)
                {
                    RecipeConvertItem(root, item);
                    break;
                }
            }
        }
    }

    public void RecipeConvertItem(ObjectRoot root, Recipe recipe)
    {
        RecipeUtil.ConvertObjectToOtherObject(root, recipe.outputPrefab);
    }

    [Serializable]
    public struct Recipe
    {
        public string inputIngredientName;
        public GameObject outputPrefab;
    }
}
