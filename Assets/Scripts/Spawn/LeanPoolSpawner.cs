using UnityEngine;
using Lean.Pool;

public class LeanPoolSpawner : ISpawner
{
    public void Despawn(GameObject obj)
    {
        LeanPool.Despawn(obj);
    }

    public GameObject Spawn(GameObject objPrefab, Vector3 position, Quaternion rotation)
    {
        return LeanPool.Spawn(objPrefab,position,rotation);
    }
}
