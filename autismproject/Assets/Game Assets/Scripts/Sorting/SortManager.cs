using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class SortManager : MonoBehaviour
{
	public UnityEvent OnWin;
	[HorizontalLine(height: 7,color:EColor.Black)]
	public Transform itemSpawner;
	public List<SortingItem> categories = new List<SortingItem>();
	public List<SortStorage> sortStorages = new List<SortStorage>();
	[ReadOnly] public List<SortDragger> items = new List<SortDragger>();
	[ReadOnly] public bool allStorageSorted;
	[ReadOnly] public bool allItemsInStorage;

	void Start()
	{
		for (int i = 0; i < categories.Count; i++)
		{
			for (int k = 0; k < categories[i].items.Count; k++)
			{
				GameObject newItem = Instantiate(categories[i].items[k].gameObject,
													itemSpawner.position, Quaternion.identity, itemSpawner);
				newItem.AddComponent(typeof(Rigidbody2D));
				newItem.AddComponent(typeof(CircleCollider2D));
				newItem.AddComponent(typeof(SortDragger));
				newItem.GetComponent<SortDragger>().categoryName = categories[i].categoryName;
				items.Add(newItem.GetComponent<SortDragger>());
			}
		}
	}

	void Update()
	{
		allStorageSorted = sortStorages.All(x=>x.sortedProperly);
		allItemsInStorage = items.All(x=>x.inStorage);
		if(allStorageSorted && allItemsInStorage) OnWin.Invoke();
	}
}
