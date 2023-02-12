using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Sorting Items", menuName = "Sort/Sorting Item")]
public class SortingItem : ScriptableObject
{
	public string categoryName;
	public List<Sprite> items = new List<Sprite>();
}
