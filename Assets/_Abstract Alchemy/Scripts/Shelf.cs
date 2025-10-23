using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public static Shelf currentShelf;
    public bool isCurrentShelf { get { return currentShelf == this; } }
    [Tooltip("Set this to true if it is the shelf closest to the player's starting location. No more than one shelf in the scene should have this value set to true.")]
    public bool defaultToCurrentShelf;

    public static List<ShelfTarget> objectsInShelf = new();

    private static bool shelfChanging;

    // Start is called before the first frame update
    void Start()
    {
        if (defaultToCurrentShelf)
        {
            objectsInShelf.Clear();
            currentShelf = null;
            SetActiveShelf();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Tooltip("Sets this shelf to be the active shelf. Use this when teleporting to a spot that's within reach of this shelf. Only one shelf can be set to the active shelf.")]
    public void SetActiveShelf()
    {
        if (isCurrentShelf) { return; }
        shelfChanging = true;
        if (currentShelf)
        {
            MoveObjectsToThisShelf();
        }
        currentShelf = this;
        shelfChanging = false;
    }

    protected void MoveObjectsToThisShelf()
    {
        //Debug.Log($"{objectsInShelf.Count}");
        foreach (ShelfTarget obj in objectsInShelf)
        {
            if (obj.rigidBody)
            {
                obj.rigidBody.Sleep();
            }
            Vector3 relativePos = obj.transform.position - currentShelf.gameObject.transform.position;
            relativePos = gameObject.transform.rotation * (Quaternion.Inverse(currentShelf.transform.rotation) * relativePos);

            Quaternion rotation = gameObject.transform.rotation * (Quaternion.Inverse(currentShelf.transform.rotation) * obj.transform.rotation);

            obj.transform.SetPositionAndRotation(gameObject.transform.position + relativePos, rotation);
            //Debug.Log($"Moved {obj.name} in shelf to {obj.transform.position} facing {obj.transform.rotation.eulerAngles}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCurrentShelf && !shelfChanging && TryGetShelfTargetInParent(other, out ShelfTarget target) && !objectsInShelf.Contains(target))
        {
            objectsInShelf.Add(target);
            //Debug.Log("Enter shelf");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCurrentShelf && !shelfChanging && TryGetShelfTargetInParent(other, out ShelfTarget target) && objectsInShelf.Contains(target))
        {
            objectsInShelf.Remove(target);
            //Debug.Log("Exit shelf");
        }
    }

    private bool TryGetShelfTargetInParent(Collider collider, out ShelfTarget shelfTarget)
    {
        shelfTarget = (ShelfTarget)collider.GetComponentInParent(typeof(ShelfTarget), false);
        return shelfTarget;
    }
}
