using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class V : MonoBehaviour // 공용 함수
{
    public static void Slider_Create(GameObject Target) //체력바 생성
    {
        BossSlider = Instantiate(Resources.Load<GameObject>("Prefab/UI/BossSlider"));
        BossSlider.transform.SetParent(V.MainCanvas.transform);

        BossSlider.GetComponent<BossSlider>().SetUp(Target);
    }
    public static void Particle_Play(POOLTYPE pooltype, Vector3 pos) //파티클 실행
    {
        var obj = V.pool.Get<Transform>(pooltype);
        obj.transform.position = pos;

        if (obj.GetComponent<ReturnParticle>() == null) obj.gameObject.AddComponent<ReturnParticle>();
    }

    public static void Explosion(List<Rigidbody> rigids, Vector3 pos, float randomValue, float speed) //폭발 효과
    {
        float r = 10f;

        foreach(var rigid in rigids) 
        {
            rigid.AddExplosionForce(speed, pos + new Vector3(Random.Range(-randomValue, randomValue), 
                Random.Range(-randomValue, randomValue), Random.Range(-randomValue, randomValue)), r, 1, ForceMode.Impulse);
        }
    }

    public static List<T> Find_Child_Component_List<T>(GameObject rootObj) where T : Component //자식 오브젝트 리스트<T> 리턴
    {
        if (rootObj == null) return null;

        var allChilds = rootObj.GetComponentsInChildren<T>();

        List<T> list = new List<T>();

        foreach (var child in allChilds)
        {
            if (child.name == rootObj.name) continue;

            list.Add(child);   
        }

        return list;
    }

    public static GameObject FInd_Child_Name(string name, GameObject rootObj) //자식 이름으로 오브젝트 찾기
    {
        if (rootObj == null) return null;

        var allChilds = rootObj.GetComponentsInChildren<Transform>();

        foreach(var child in allChilds) 
        {
            if (child.name == rootObj.name) continue;
            
            if(child.name == name) 
            {
                return child.gameObject;
            }
        }

        return null;
    }
}
