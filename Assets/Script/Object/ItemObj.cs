using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : CBase //아이템 오브젝트 클래스
{
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 6f; //속도
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, 1);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) //아이템 획득
        {
            V.Item.GetItem();
            V.pool.Return(gameObject);
        }
    }
}
