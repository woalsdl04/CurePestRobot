using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase
{
    public void BossOn(Vector3 startpos, Vector3 endPos) 
    {
        gameObject.SetActive(true);
        DIsableCCAndMovePos(startpos);
        EndPos = endPos;
        V.Cam.ChangeTarget(CAMERA_TARGET.BOSS);
        V.Spawn.EnemyList.Add(gameObject);

        enemyStep = ENEMY_STEP.START;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
