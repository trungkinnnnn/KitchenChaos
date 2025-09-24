using UnityEngine;

public interface ISpawner
{
    GameObject Spawn(GameObject objPrefab, Vector3 position, Quaternion rotation); 
    void Despawn(GameObject obj);
}
