using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyBase //보스 베이스 클래스
{
    public void BossOn(Vector3 startpos, Vector3 endPos) //보스 활성화
    {
        gameObject.SetActive(true);
        DIsableCCAndMovePos(startpos);
        EndPos = endPos;
        V.Cam.ChangeTarget(CAMERA_TARGET.BOSS);
        V.Spawn.EnemyList.Add(gameObject);

        enemyStep = ENEMY_STEP.START;
    }
}
