using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_STEP 
{
    START,
    S2A,
    ATTACK,
    DIE_BEFORE,
    DIE
}

public class EnemyBase : Caric
{
    public ENEMY_STEP enemyStep = ENEMY_STEP.START;

    public Vector3 Dir;

    public Vector3 StartPos;

    public Vector3 EndPos;

    public float MAxAttackTime = 0f;

    public float AttackCoolTime = 0f;

    public float TrailTIme = 0f;
    public Gradient TrailColor;
    // Start is called before the first frame update

    // Update is called once per frame
    public void EnemyUpdate()
    {
        switch (enemyStep)
        {
            case ENEMY_STEP.START:

                EnemyStart();

                break;
            case ENEMY_STEP.S2A:

                EnemyS2A();

                enemyStep = ENEMY_STEP.ATTACK;

                break;
            case ENEMY_STEP.ATTACK:

                if(transform.position.y < -(V.PLAYER_MINMAX_POS) - 5)
                {
                    IsOverLine();

                    enemyStep = ENEMY_STEP.DIE_BEFORE;

                    return;
                }

                EnemyAttack();

                break;
            case ENEMY_STEP.DIE_BEFORE:

                EnemyDIe();

                enemyStep = ENEMY_STEP.DIE;

                break;
            case ENEMY_STEP.DIE:

                break;
        }
    }

    public virtual void IsOverLine() 
    {
        V.Ingame.PainValue += (Dmg / 2);
        V.Fade.FadeIn(new Color(0.5f, 0, 0.5f), 0.5f, 1, 2);
    }

    public virtual void EnemyStart()
    {
        if(Vector3.Distance(transform.position, EndPos) >= 0.01f) 
        {
            transform.position = Vector3.Lerp(transform.position, EndPos, Time.deltaTime * 3f);
        }
        else 
        {
            transform.position = EndPos;
            enemyStep = ENEMY_STEP.S2A;
        }
    }
    public virtual void EnemyS2A()
    {
        MAxAttackTime = V.WorldTIme + AttackCoolTime;
    }

    public virtual void EnemyAttack()
    {
        CC.Move(Dir * MoveSpeed * Time.deltaTime);
    }

    public virtual void EnemyDIe()
    {
        V.Particle_Play(POOLTYPE.PARTICLE_DIE, transform.position);
        V.FInd_Child_Name("body", Body).SetActive(false);
        V.Explosion(V.FInd_Child_Component_List<Rigidbody>(Body), Body.transform.position, 1f, 16f);

        DestroyEnemy(3f);
    }

    public void DestroyEnemy(float time) 
    {
        V.Spawn.EnemyList.Remove(gameObject);
        Destroy(gameObject, time);
    }
}
