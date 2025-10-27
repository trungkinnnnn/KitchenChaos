using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    private ISpawner _pool;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        _pool = SpawnerFactory.GetSpawner();
    }

 

    public GameObject Spawner(GameObject obj, Vector3 position, Quaternion quaternion, Transform parent = null)
    {
        return _pool.Spawn(obj, position, quaternion, parent);
    }

    public void Despawner(GameObject obj)
    {
       _pool.Despawn(obj);
    }

}
