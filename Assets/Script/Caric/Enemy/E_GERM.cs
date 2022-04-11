using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GERM : EnemyBase
{
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        C_Init(V.DataInfo.EnemyHp[1], 0, V.DataInfo.EnemyDmg[1], 0.5f);
        DIsableCCAndMovePos(StartPos);
        EndPos = StartPos + (Vector3.down * 10f);

        AttackCoolTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }


    public override void EnemyAttack()
    {
        if(MAxAttackTime < V.WorldTIme) 
        {
            for(int i = 0; i < 360; i += 20) 
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform, this, null, i, 15);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }

            Dir = (count % 2 == 0) ? new Vector3(-1, -1, 0) : new Vector3(1, -1, 0);

            Vector3 nDir = new Vector3(Dir.x, -Dir.y, Dir.z);

            Body.transform.rotation = Quaternion.LookRotation(nDir);

            count++;

            MAxAttackTime = V.WorldTIme + AttackCoolTime;
        }

        base.EnemyAttack();
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;

        V.ScoreValue += 5000;

        base.Die();
    }
}
