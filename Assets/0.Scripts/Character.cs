using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : PoolableMono
{
    public UnityEvent PickEvent;

    public void Picked()
    {
        Debug.Log($"{gameObject.name}이 선택되었습니다.");
        PoolManager.Instance.Pop("AreaBox", transform.position+new Vector3(1, 0));
        PoolManager.Instance.Pop("AreaBox", transform.position+new Vector3(-1, 0));
        PoolManager.Instance.Pop("AreaBox", transform.position+new Vector3(0, 1));
        PoolManager.Instance.Pop("AreaBox", transform.position+new Vector3(0, -1));
        PickEvent?.Invoke();
    }

    public override void Reset()
    {
    }
}
