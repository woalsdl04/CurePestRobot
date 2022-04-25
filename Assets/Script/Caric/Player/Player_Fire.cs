using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{
    public KeyCode AttackKey = KeyCode.Z;
    public KeyCode ChangeKey = KeyCode.X;
    public KeyCode BombKey = KeyCode.C;

    public float AttackCoolTime = 0.25f;
    public float BulletFireCoolTime = 0f;
    public float ChangeDelayTime = 0f;

    public float trailTIme = 0f;
    public Gradient trailColor;

    public Animation anim;
    public int AnimCount = 0;

    public Collider[] col;

    public float LaserCharge_Time = 0f;
    public GameObject LaserCharge_Pt = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        LaserCharge_Pt.SetActive(false);

        AttackCoolTime = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(AttackKey) && Input.GetKeyDown(ChangeKey))
        {
            if (ChangeDelayTime < V.WorldTIme)
            {
                switch (AnimCount % 3)
                {
                    case 0:
                        anim.Play("B2S");
                        break;
                    case 1:
                        anim.Play("S2L");
                        break;
                    case 2:
                        anim.Play("L2B");
                        break;
                }

                AnimCount++;
                ChangeDelayTime = V.WorldTIme + 1f;

                V.UI.ChangeClassImage(AnimCount);

                V.Player.Player_class = (BULLETTYPE)(AnimCount % 3);
            }
        }

        if (Input.GetKey(AttackKey))
        {
            if (BulletFireCoolTime < V.WorldTIme)
            {

                switch (V.Player.Player_class)
                {

                    case BULLETTYPE.BASIC:
                        
                        V.Sound.SFX_Play(SFX_SOUND.SHOOT, 0.5f);

                        BasicShoot();                   

                        break;
                    case BULLETTYPE.SMART:

                        col = Physics.OverlapSphere(transform.position, 8f, LayerMask.GetMask("Enemy"));

                        if(col.Length != 0)
                        {
                            V.Sound.SFX_Play(SFX_SOUND.SHOOT, 0.5f);
                            StartCoroutine(SmartShoot());
                        }

                        BulletFireCoolTime = V.WorldTIme + (AttackCoolTime * 2);

                        break;
                }
            }
        }

        if(Input.GetKeyDown(AttackKey) && V.Player.Player_class == BULLETTYPE.LASER) 
        {
            LaserChargeOn();
        }
        else if (Input.GetKeyUp(AttackKey) && V.Player.Player_class == BULLETTYPE.LASER) 
        {
            LaserShoot();
        }

        if (Input.GetKeyDown(BombKey)) 
        {
            if(V.Player.Mp >= 100) 
            {
                foreach (var item in V.Spawn.EnemyList)
                {
                    new JudgeMenetSign(V.Player, item.GetComponent<Caric>(), 5);      
                }
                
                var bomb = V.pool.Get<Transform>(POOLTYPE.BOMB);
                bomb.transform.position = transform.position;

                V.Cam.Shake();

                V.Player.Mp = 0;
            }
        }
    }

    public void BasicShoot() 
    {
        float w = 0.5f;
        float ws = -(w * V.Player_WeaponeLevel) / 2;
        ws += (w / 2);

        for(int i = 0; i < V.Player_WeaponeLevel; i++) 
        {
            var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
            obj.SetUp(BULLETTYPE.BASIC, V.Player.Muzzle.transform.position + new Vector3(ws + (w * i), 0, 0), V.Player, null, 90, 30f);
            obj.TrailInit(obj.gameObject, trailTIme, trailColor);
        }

        BulletFireCoolTime = V.WorldTIme + AttackCoolTime;
    }

    public IEnumerator SmartShoot() 
    {
        int shootCount = V.Player_WeaponeLevel + 1;

        int count = 0;

        while (shootCount > 0) 
        {
            if (col[count % col.Length].GetComponent<EnemyBase>().enemyStep == ENEMY_STEP.ATTACK)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.SMART, V.Player.Core.transform.position, V.Player, col[count % col.Length].gameObject, 0, 2f);
                obj.TrailInit(obj.gameObject, trailTIme, trailColor);
            }

            count++;

            shootCount--;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void LaserChargeOn()
    {
        LaserCharge_Pt.SetActive(true);
        LaserCharge_Time = V.WorldTIme + (AttackCoolTime * 8);
    }

    public void LaserShoot() 
    { 
        if(LaserCharge_Time < V.WorldTIme)
        {
            V.Sound.SFX_Play(SFX_SOUND.LASER);
            var obj = V.pool.Get<BulletBase>(POOLTYPE.LASER);
            obj.SetUp(BULLETTYPE.LASER, V.Player.Muzzle.transform.position, V.Player, null, 0, 0f, 3f);
        }
        else
        {
        }
            
        LaserCharge_Pt.SetActive(false);

    }

}




