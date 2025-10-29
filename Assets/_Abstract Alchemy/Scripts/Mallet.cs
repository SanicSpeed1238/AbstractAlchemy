using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mallet : MonoBehaviour
{
    public Recipes[] recipes;
    
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
        GameObject prefab = PrefabUtility.GetNearestPrefabInstanceRoot(collision.gameObject);
        if (prefab)
        {
            foreach (var item in recipes)
            {
                if (item.inputPrefab == prefab)
                {
                    RecipeConvertItem(collision.gameObject);
                    break;
                }
            }
        }
    }

    public void RecipeConvertItem(GameObject gameObject)
    {

    }

    [Serializable]
    public struct Recipes
    {
        public GameObject inputPrefab;
        public GameObject outputPrefab;
    }
}
