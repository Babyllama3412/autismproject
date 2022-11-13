using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SortStorage : MonoBehaviour
{
	public SortingItem objectCategoryAllowed;
	public TMP_Text categoryText;
	[ReadOnly] public bool sortedProperly;
	[ReadOnly] public List<SortDragger> items = new List<SortDragger>(); 
	// Start is called before the first frame update
	void Start()
	{
		GetComponent<Collider2D>().isTrigger = true;
		categoryText.text = objectCategoryAllowed.categoryName;
	}
	
	void Update()
	{
		sortedProperly = items.All(x=> x.categoryName.Equals(objectCategoryAllowed.categoryName)) 
								   && items.Count > 0;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.GetComponent<SortDragger>() != null)
		{
			SortDragger enteredItem = other.GetComponent<SortDragger>();
			if(!items.Contains(enteredItem))
			{
				items.Add(enteredItem);
				enteredItem.inStorage = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.GetComponent<SortDragger>() != null)
		{
			SortDragger exitItem = other.GetComponent<SortDragger>();
			if(items.Contains(exitItem))
			{
				items.Remove(exitItem);
				exitItem.inStorage = false;
			}
		}
	}
}
