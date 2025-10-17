using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public static Shelf currentShelf;
    public bool isCurrentShelf { get { return currentShelf == this; } }
    public bool defaultToCurrentShelf;

    public static List<GameObject> objectsInShelf = new();

    private bool shelfChanging;

    private void Awake()
    {
        if (defaultToCurrentShelf)
        {
            SetActiveShelf();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveShelf()
    {
        if (isCurrentShelf) { return; }
        shelfChanging = true;
        MoveObjectsToThisShelf();
        currentShelf = this;
        shelfChanging = false;
    }

    protected void MoveObjectsToThisShelf()
    {
        foreach (GameObject obj in objectsInShelf)
        {
            Vector3 relativePos = currentShelf.gameObject.transform.position - obj.transform.position;
            // do rotation
            obj.transform.position = gameObject.transform.position + relativePos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCurrentShelf && !shelfChanging)
        {
            objectsInShelf.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCurrentShelf && !shelfChanging)
        {
            if (objectsInShelf.Contains(other.gameObject))
            {
                objectsInShelf.Remove(other.gameObject);
            }
        }
    }
}
