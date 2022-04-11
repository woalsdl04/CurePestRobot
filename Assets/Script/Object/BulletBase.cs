using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BULLETTYPE 
{ 
    BASIC,
    SMART,
    LASER,
}

public class BulletBase : CBase
{
    //Base
    public BULLETTYPE BulletType = BULLETTYPE.BASIC;
    public Transform StartPos;
    public float BulletLifeTime = 0f;

    //Smart
    public GameObject Target = null;
    public Vector3[] SmartPos = new Vector3[3];
    public float SmartCurTime = 0f;

    //Laser
    public float HitCoolTime = 0f;

    public void TrailInit(GameObject obj, float time, Gradient color) 
    {
        var trail = obj.GetComponent<TrailRenderer>();

        if(trail != null) 
        {
            trail.Clear();
            trail.time = time;
            trail.colorGradient = color;
        }
    }

    public void SetUp(BULLETTYPE bullettype, Transform trs, Caric owner, GameObject target, float ang, float speed, float lifeTime = 10f) 
    {
        BulletType = bullettype;
        Owner = owner;
        StartPos = trs;
        BulletLifeTime = V.WorldTIme + lifeTime;
        MoveSpeed = speed;
        Target = target;

        transform.rotation = Quaternion.Euler(0, 0, ang);
        transform.position = StartPos.position;

        if (Target != null) 
        {
            SmartCurTime = 0f;

            SmartPos[0] = StartPos.position;

            SmartPos[1] = 
                StartPos.position + (6 * Random.Range(-1f, 1f) * StartPos.right) 
                + (6 * Random.Range(-1f, 0f) * StartPos.up)
                + (2 * Random.Range(-1f, 0f) * StartPos.forward);

            SmartPos[2] = Target.transform.position;
        }

    }

    public void SetUp(BULLETTYPE bullettype, Vector3 trs, Caric owner, GameObject target, float ang, float speed, float lifeTime = 10f)
    {
        BulletType = bullettype;
        Owner = owner;
        BulletLifeTime = V.WorldTIme + lifeTime;
        MoveSpeed = speed;
        Target = target;

        transform.rotation = Quaternion.Euler(0, 0, ang);
        transform.position = trs;

        if (Target != null)
        {
            SmartCurTime = 0f;

            SmartPos[0] = StartPos.position;

            SmartPos[1] =
                StartPos.position + (6 * Random.Range(-1f, 1f) * StartPos.right)
                + (6 * Random.Range(-1f, 0f) * StartPos.up)
                + (2 * Random.Range(-1f, 0f) * StartPos.forward);

            SmartPos[2] = Target.transform.position;
        }

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