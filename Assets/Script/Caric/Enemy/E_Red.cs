using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Red : EnemyBase //적혈구 클래스
{
    // Start is called before the first frame update
    void Start()
    {
        C_Init(1, 0, 0, Random.Range(3f, 6f));
        DIsableCCAndMovePos(StartPos);

        enemyStep = ENEMY_STEP.ATTACK;

        Dir = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }

    public override void IsOverLine()
    {
    }

    public override void EnemyAttack()
    {
        base.EnemyAttack();
    }

    public override void EnemyDIe()
    {
        base.EnemyDIe();
    }

    public override void Hit()
    {
        base.Hit();

    }

    public override void Attack()
    {
        base.Attack();
        Die();
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;

        V.Ingame.PainValue += 10;
        V.Fade.FadeIn(new Color(0.7f, 0, 0.7f), 0.5f, 1.5f, 2);

        base.Die();
    }
}
