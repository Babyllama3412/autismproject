using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SortStorage : MonoBehaviour
{
    public string objectCategoryAllowed;
    public bool sortedProperly;
    public List<GameObject> objects = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    
    void Update()
    {
        sortedProperly = objects.All(x=> x.GetComponent<SortDragger>().objectCategory.Equals(objectCategoryAllowed))
                        && objects.Count > 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<SortDragger>() != null)
        {
            if(!objects.Contains(other.gameObject))
            {
                objects.Add(other.gameObject);
                other.GetComponent<SortDragger>().isInStorage = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<SortDragger>() != null)
        {
            if(objects.Contains(other.gameObject))
            {
                objects.Remove(other.gameObject);
                other.GetComponent<SortDragger>().isInStorage = false;
            }
        }
    }
}
