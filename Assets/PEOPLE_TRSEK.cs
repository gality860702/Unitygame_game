﻿
using UnityEngine;
using System.Linq;             //引用 系統,查詢語言 Lin Quary

///<summary>
///追蹤人類
/// </summary>
public class PEOPLE_TRSEK : people
{
    ///<summary>
    ///目標
    /// </summary>
    protected Transform target;

    ///<summary>
    ///人類陣列
    /// </summary>
    public people[] people;

    /// <summary>
    /// 距離
    /// </summary>
    public float[] distance;

    protected virtual void Start()
    {
        //人類陣列 = 透過類型尋找物件<泛型>()
        people = FindObjectsOfType<people>();
        //距離陣列的數量 = 人類陣列的數量
        distance = new float[people.Length];
        //設定目的地(原點) - 避免一開始導航錯亂
        agent.SetDestination(Vector3.zero);
    }

    private void Update()
    {
        Track();
    }

    ///<summary>
    /// 追蹤方法
    /// </summary>
    protected virtual void Track()
    {
        // 儲存所有人跟此物件的距離
        for (int i = 0; i < people.Length; i++)
        {
             
            
                if(people[i]==null ||people[i].transform.name=="殭屍" || people[i].transform.name=="警察")
        {
                if (people[i] == null) distance[i] = 1000;       //如果人類死亡 距離 改為1000
                else distance[i] = 999;                          // 此物件的距離改為999
                continue;                                        // 繼續 - 跳過並執行下一次迴圈
            }
            distance[i] = Vector3.Distance(transform.position,people[i].transform.position);
        }
        //判斷最近
       float min=distance.Min();                     //最小值 = 距離.最小值
       int index= distance.ToList().IndexOf(min);    //索引值 = 距離.轉清單.取得索引值(最小值)
        target = people[index].transform;            //目標 = 人類[索引值].變形
        //追蹤最近目標
        agent.SetDestination(target.position);

        if (agent.remainingDistance <= 1f && min!=999) HitPeople();
    }

    private float timerhit;
    /// <summary>
    /// 傷害人類
    /// </summary>
    private void HitPeople()
    {
        if (timerhit >= 1f)
        {
            timerhit = 0;
            agent.isStopped = true;
            ani.SetTrigger("攻擊");
            target.GetComponent<people>().Dead();
        }
    else
        {
            agent.isStopped = false;
            timerhit += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "火球")
        {
            Dead();
        }
    }

}
