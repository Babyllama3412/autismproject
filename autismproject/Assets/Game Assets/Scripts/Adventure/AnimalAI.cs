using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public Transform player;
    public float radiusCheck;
    public float lookSpeed;

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= radiusCheck)
        {
            GetComponent<Animator>().SetTrigger("attack");
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        }
    }

    public void AttackGoods()
    {
        if(Vector3.Distance(player.position, transform.position) <= radiusCheck)
        GoodsManager.Instance.RemoveGoods();
    }
}
