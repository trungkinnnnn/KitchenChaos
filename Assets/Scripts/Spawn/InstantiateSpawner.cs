using UnityEngine;

public class InstantiateSpawner : ISpawner
{
    public void Despawn(GameObject obj)
    {
       GameObject.Destroy(obj);
    }

    public GameObject Spawn(GameObject objPrefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(objPrefab, position, rotation);
    }
}
