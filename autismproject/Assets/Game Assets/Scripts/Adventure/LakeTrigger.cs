using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeTrigger : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            GoodsManager.Instance.RemoveGoods();
    }
}
