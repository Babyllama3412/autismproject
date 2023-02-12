using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SortManager : MonoBehaviour
{
	[SerializeField] List<SortingItem> items = new List<SortingItem>();
	[SerializeField] float currentTimer;
	[SerializeField] float currentScore;
	[SerializeField][ReadOnly] int currentBasket;
	[HorizontalLine(height: 7,color:EColor.Black)]
	[SerializeField] TMP_Text timerText;
	[SerializeField] TMP_Text scoreText;
	[SerializeField] TMP_Text finalScoreText;
	[SerializeField] Image itemPicture;
	[SerializeField] TMP_Text basketOneText;
	[SerializeField] TMP_Text basketTwoText;
	[SerializeField] TMP_Text basketThreeText;
	[SerializeField] GameObject winPanel;

	[SerializeField][ReadOnly] List<SortingItem> currentBasketList = new List<SortingItem>();

	bool startTimer;

	void Start()
	{
		Init();
	}

	void Init()
	{
		if(items.Count < 3) return;

		currentBasketList.Add(RemoveItem<SortingItem>(items, Random.Range(0, items.Count)));
		currentBasketList.Add(RemoveItem<SortingItem>(items, Random.Range(0, items.Count)));
		currentBasketList.Add(RemoveItem<SortingItem>(items, Random.Range(0, items.Count)));

		basketOneText.text = currentBasketList[0].categoryName;
		basketTwoText.text = currentBasketList[1].categoryName;
		basketThreeText.text = currentBasketList[2].categoryName;
	}

	void Update()
	{
		if(startTimer) currentTimer -= Time.deltaTime;
		timerText.text = currentTimer.ToString("F2");
		scoreText.text = "Score: " + currentScore + "";

		if(currentTimer <= 0)
		{
			startTimer = false;
			currentTimer = 0;
			finalScoreText.text = "Final Score: " + currentScore + "";
			winPanel.SetActive(true);
		}
	}

	public void Play()
	{
		startTimer = true;
		PopulateNewItem();
	}

	public void SortToBasket(int index)
	{
		if(currentBasket == index)
		{
			currentScore++;
			PopulateNewItem();
		} else currentTimer -= 5;
	}

	void PopulateNewItem()
	{
		currentBasket = Random.Range(0,3);
		itemPicture.sprite = currentBasketList[currentBasket].items[Random.Range(0, currentBasketList[currentBasket].items.Count)];
	}

	public T RemoveItem<T>(List<T> list, int index)
    {
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }
}
