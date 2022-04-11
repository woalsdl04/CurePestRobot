using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject SpawnPoint_Parent;

    public List<Transform> points = new List<Transform>();

    public int EnemyCount = 0;

    public List<GameObject> EnemyList = new List<GameObject>();

    private void Awake()
    {
        V.Spawn = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SpawnPoint_Parent == null) GameObject.Find("SpawnPoint");

        points = V.FInd_Child_Component_List<Transform>(SpawnPoint_Parent);
    }

    public void SpawnBoss(Vector3 pos) 
    {
        V.Boss.GetComponent<BossBase>().BossOn(pos, pos + (Vector3.down * 20));
    }

    public void SpawnEnemy(POOLTYPE enemyType, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            var obj = V.pool.Get<EnemyBase>(enemyType);
            obj.StartPos = points[EnemyCount % 5].position;

            EnemyList.Add(obj.gameObject);

            EnemyCount++;
        }
    }

    public void SpawnEnemy(POOLTYPE enemyType, Vector3 pos)
    {
        var obj = V.pool.Get<EnemyBase>(enemyType);
        obj.StartPos = pos;
        
        if(enemyType  != POOLTYPE.E_RED && enemyType != POOLTYPE.E_WHITE) EnemyList.Add(obj.gameObject);
    }
}
