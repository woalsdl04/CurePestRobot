using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BulletLifeTime < V.WorldTIme || 
            (BulletType == BULLETTYPE.SMART && Target.GetComponent<EnemyBase>().enemyStep == ENEMY_STEP.DIE))
        {
            V.pool.Return(this.gameObject);
            return;
        }

        switch (BulletType)
        {
            case BULLETTYPE.BASIC:

                transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);

                break;
            case BULLETTYPE.SMART:

                SmartPos[2] = Target.transform.position;

                SmartCurTime += Time.deltaTime * MoveSpeed;

                transform.position = new Vector3
                    (
                        SmartCurve(SmartPos[0].x, SmartPos[1].x, SmartPos[2].x),
                        SmartCurve(SmartPos[0].y, SmartPos[1].y, SmartPos[2].y),
                        SmartCurve(SmartPos[0].z, SmartPos[1].z, SmartPos[2].z)
                    );

                break;
            case BULLETTYPE.LASER:

                if(HitCoolTime < V.WorldTIme) 
                {
                    var hit = Physics.RaycastAll(transform.position, Vector3.up);

                    foreach(var item in hit) 
                    {
                        if(item.transform != null && item.transform.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
                        {
                            new JudgeMenetSign(Owner, item.transform.gameObject.GetComponent<Caric>());
                            V.Particle_Play(POOLTYPE.PARTICLE_HIT_LASER, item.transform.position);
                        }
                    }
                    
                    HitCoolTime = V.WorldTIme + 0.25f;
                }

                gameObject.transform.position = Owner.GetComponent<Player_Main>().Muzzle.transform.position;
                gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);

                break;
        }
    }

    public float SmartCurve(float a, float b, float c) 
    {
        float ab = Mathf.Lerp(a, b, SmartCurTime);
        float bc = Mathf.Lerp(b, c, SmartCurTime);

        float abc = Mathf.Lerp(ab, bc, SmartCurTime);

        return abc;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Caric>() == Owner ||
            (other.gameObject.layer == Owner.gameObject.layer)) return;

        V.pool.Return(gameObject);

        if (other.GetComponent<Caric>() == V.Player && V.Player.Barrier.activeSelf) 
        {
            return;
        }

        new JudgeMenetSign(Owner, other.GetComponent<Caric>());

        V.Particle_Play(POOLTYPE.PARTICLE_HIT, transform.position);

        return;
    }
}
