using UnityEngine;

public interface ISpawner
{
    public void Despawn(GameObject obj);
    public GameObject Spawn(GameObject objPrefab, Vector3 position, Quaternion rotation, Transform parent = null);

}
