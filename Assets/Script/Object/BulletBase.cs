using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BULLETTYPE //총알 종류
{ 
    BASIC,
    SMART,
    LASER,
}

public class BulletBase : CBase //총알 베이스 클래스
{
    //Base
    public BULLETTYPE BulletType = BULLETTYPE.BASIC;
    public float BulletLifeTime = 0f;

    //Smart
    public GameObject Target = null;
    public Vector3[] SmartPos = new Vector3[3];
    public float SmartCurTime = 0f;

    //Laser
    public float HitCoolTime = 0f;

    public void TrailInit(GameObject obj, float time, Gradient color) //TrailComponent 초기화
    {
        var trail = obj.GetComponent<TrailRenderer>();

        if(trail != null) 
        {
            trail.Clear();
            trail.time = time;
            trail.colorGradient = color;
        }
    }

    public void SetUp(BULLETTYPE bullettype, Vector3 trs, Caric owner, GameObject target, float ang, float speed, float lifeTime = 10f) //총알 초기화
    {
        BulletType = bullettype; 
        Owner = owner;
        BulletLifeTime = V.WorldTIme + lifeTime;
        MoveSpeed = speed;
        Target = target;

        transform.rotation = Quaternion.Euler(0, 0, ang);
        transform.position = trs;

        if (Target != null) //Smart
        {
            SmartCurTime = 0f;

            SmartPos[0] = trs;

            SmartPos[1] = 
                trs + (6 * Random.Range(-1f, 1f) * Vector3.right) 
                + (6 * Random.Range(-1f, 0f) * Vector3.up)
                + (2 * Random.Range(-1f, 0f) * Vector3.forward);

            SmartPos[2] = Target.transform.position;
        }

    }
}
