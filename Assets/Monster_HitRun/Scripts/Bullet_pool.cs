using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet_pool : MonoBehaviour
{
    Transform vitri;
    public float speedBall=1f;
    private Transform spawnPos;
    private Transform targetPos;
    private int level;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "enemies")
    //    {
    //        gameObject.SetActive(false);
    //        Destroy(collision.transform.gameObject);
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("enemies"))
    //    {
    //        gameObject.SetActive(false);
    //        Destroy(other.gameObject);

    //    }


    //}
    public void Init(Transform spawnPos, Transform targetPos,int lve)
    {
        //Debug.LogError("spawn");
        this.spawnPos = spawnPos;
        this.targetPos = targetPos;
        transform.position = spawnPos.position;
        transform.gameObject.SetActive(true);
        level = lve;
    }
    public void Move()
    {
        float timer = TimeToTarget(this.transform.position,targetPos.transform.position);
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, targetPos.transform.position, 0.2f), 13.5f);
        transform.DOLocalMove(targetPos.position, timer, false).OnComplete(() => {

            Destroy(targetPos.gameObject);
            gameObject.SetActive(false);
            PlayerManager.PlayerManagerIstance.lvPlayer += level;
        });
           
    }
    public float TimeToTarget(Vector3 from,Vector3 to)
    {
        float dis = Vector3.Distance(from, to);
        return dis / speedBall;
    }
   
    void Update()
    {
        //Debug.LogError("update");

        if (targetPos != null)
        {
            Move();
        }
    }
}
