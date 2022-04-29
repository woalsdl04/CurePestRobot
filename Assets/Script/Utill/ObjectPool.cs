using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POOLTYPE //오브젝트 종류
{
    OBJECT,
    PLAYER,
    E_BACTERIA,
    E_GERM,
    E_CANCER,
    E_VIRUS,
    E_RED,
    E_WHITE,

    BULLET,
    LASER,
    BOSS_LASER,
    ITEM,
    BOMB,

    PARTICLE_HIT,
    PARTICLE_HIT_LASER,
    PARTICLE_DIE,
    PARTICLE_LASER,

    PARTICLE_BOSS_HIT_LASER,

    END //끝
}

public class ObjectPool : MonoBehaviour //오브젝트 풀
{

    public List<GameObject> pools = new List<GameObject>();
    private void Awake()
    {
        V.pool = this;
    }

    public T Get<T>(POOLTYPE poolType) where T : Component //오브젝트 찾기 <T>
    {
        var obj_Parenet = pools[(int)poolType];

        var allChilds = obj_Parenet.GetComponentsInChildren<Transform>(true);

        for(int i = 0; i < obj_Parenet.transform.childCount; i++) 
        {
            var child = obj_Parenet.transform.GetChild(i).gameObject;

            if (!child.activeSelf) 
            {
                child.gameObject.SetActive(true);

                return child.GetComponent<T>();
            }
        }

        var obj = GetObj(poolType);

        obj.transform.SetParent(obj_Parenet.transform);

        return obj.GetComponent<T>();
    }

    public void Return(GameObject obj) // 오브젝트 반납
    {
        obj.SetActive(false);
    }

    public GameObject GetObj(POOLTYPE poolType) //프리펩 찾기
    {
        string objPath = "";

        switch (poolType) 
        {
            case POOLTYPE.OBJECT:
                break;
            case POOLTYPE.PLAYER:
                objPath = "Prefab/Player/Player";
                break;
            case POOLTYPE.BULLET:
                objPath = "Prefab/Bullet/Bullet";
                break;
            case POOLTYPE.LASER:
                objPath = "Prefab/Bullet/Laser";
                break;
            case POOLTYPE.BOSS_LASER:
                objPath = "Prefab/Bullet/Boss_Laser";
                break;
            case POOLTYPE.BOMB:
                objPath = "Prefab/Bullet/Bomb";
                break;
            case POOLTYPE.ITEM:
                objPath = "Prefab/Object/Item";
                break;
            case POOLTYPE.E_BACTERIA:
                objPath = "Prefab/Enemy/E_Bacteria";
                break;
            case POOLTYPE.E_GERM:
                objPath = "Prefab/Enemy/E_Germ";
                break;
            case POOLTYPE.E_CANCER:
                objPath = "Prefab/Enemy/E_Cancer";
                break;
            case POOLTYPE.E_VIRUS:
                objPath = "Prefab/Enemy/E_Virus";
                break;
            case POOLTYPE.E_RED:
                objPath = "Prefab/Enemy/E_Red";
                break;
            case POOLTYPE.E_WHITE:
                objPath = "Prefab/Enemy/E_White";
                break;
            case POOLTYPE.PARTICLE_HIT:
                objPath = "Prefab/Particle/Hit";
                break;
            case POOLTYPE.PARTICLE_LASER:
                objPath = "Prefab/Particle/Laser";
                break;
            case POOLTYPE.PARTICLE_HIT_LASER:
                objPath = "Prefab/Particle/Laser_Hit";
                break;
            case POOLTYPE.PARTICLE_BOSS_HIT_LASER:
                objPath = "Prefab/Particle/Boss_Laser_Hit";
                break;
            case POOLTYPE.PARTICLE_DIE:
                objPath = "Prefab/Particle/Explosion";
                break;

        }

        var obj = Instantiate(Resources.Load<GameObject>(objPath)); //오브젝트 생성

        return obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < (int)POOLTYPE.END; i++) //부모 오브젝트들 생성
        {
            var obj = new GameObject(((POOLTYPE)i).ToString());
            obj.transform.SetParent(gameObject.transform);

            pools.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
