using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour //생성 매니저
{
    public GameObject SpawnPoint_Parent;

    public List<Transform> points = new List<Transform>();

    public List<GameObject> EnemyList = new List<GameObject>();

    private void Awake()
    {
        V.Spawn = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SpawnPoint_Parent == null) GameObject.Find("SpawnPoint");

        points = V.Find_Child_Component_List<Transform>(SpawnPoint_Parent);
    }

    public void SpawnBoss(Vector3 pos) //보스 생성
    {
        V.Boss.GetComponent<BossBase>().BossOn(pos, pos + (Vector3.down * 20));
    }

    public void SpawnEnemy(POOLTYPE enemyType, int Count) //적 생성 
    {
        var obj = V.pool.Get<EnemyBase>(enemyType);
        obj.StartPos = points[Count].position;

        EnemyList.Add(obj.gameObject);
    }

    public void SpawnEnemy(POOLTYPE enemyType, Vector3 pos) //적 생성(백혈구, 적혈구)
    {
        var obj = V.pool.Get<EnemyBase>(enemyType);
        obj.StartPos = pos;
        
        if(enemyType  != POOLTYPE.E_RED && enemyType != POOLTYPE.E_WHITE) EnemyList.Add(obj.gameObject);
    }
}
