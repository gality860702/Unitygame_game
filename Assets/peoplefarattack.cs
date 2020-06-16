
using UnityEngine;

public class peoplefarattack : PEOPLE_TRSEK
{
    [Header("停止距離"), Range(1, 10)]
    public float stop = 3f;
    [Header("子彈")]
    public GameObject bullet;
    [Header("冷卻時間"), Range(0.2f, 0.5f)]
    public float cd = 0.5f;

    private float timer;

    protected override void Start()
    {
        agent.stoppingDistance = stop;                      // 代理器,停止距離 = 停止距離
        target = GameObject.Find("殭屍").transform;         // 目標 = 殭屍
    }

    protected override void Track()
    {
        agent.SetDestination(target.position);
        transform.LookAt(target);
        if (agent.remainingDistance <= stop) Attack();       //如果 代理器,距離 < 停止距離 就
    }

    ///<summary>
    ///攻擊
    /// </summary>
    private void Attack()
    {
        timer += Time.deltaTime;

        if (timer >= cd)
        {
            timer = 0;                     //計時器歸0
            ani.SetTrigger("攻擊");
            GameObject temp = Instantiate(bullet, transform.position + transform.forward + transform.up, transform.rotation);
            Rigidbody rig = temp.AddComponent<Rigidbody>();
            rig.AddForce(transform.forward * 500);
        }
    }
}
