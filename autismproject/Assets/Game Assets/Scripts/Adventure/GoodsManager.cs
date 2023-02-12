using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoodsManager : MonoBehaviour
{
    public List<GameObject> goods = new List<GameObject>();
    public UnityEvent OnGoodsRunOut;
    public bool ranOut;

    public int currentGoods;

    public static GoodsManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        currentGoods = goods.Count - 1;
        UpdateVisuals();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) RemoveGoods();
    }

    public void RemoveGoods()
    {
        currentGoods--;
        UpdateVisuals();

        if(currentGoods < 0)
        {
            currentGoods = -1;
            goods[0].SetActive(false);
            
            Time.timeScale = 0;
            ranOut = true;
            FirstPersonController.Instance.allowLook = false;
            OnGoodsRunOut?.Invoke();
        }
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < goods.Count; i++)
        {
            goods[i].SetActive(false);
        }
        for (int i = 0; i <= currentGoods; i++)
        {
            goods[i].SetActive(true);
        }
    }
}
