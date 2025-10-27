using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ProgessManager : MonoBehaviour
{

    [SerializeField] Transform _parentTransform;
    [SerializeField] GameObject _progeressPrefabs;

    public static ProgessManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else
        {
            Destroy(gameObject);
        }    
    }

    public ProgessBar CreateProgessBar(Transform transform, float time)
    {
        Vector3 position = transform.position + Vector3.up * 1.5f;
        var progessUI = PoolManager.Instance.Spawner(_progeressPrefabs, position, quaternion.identity);
        progessUI.transform.SetParent(_parentTransform);
        ProgessBar progessBar = progessUI.GetComponent<ProgessBar>();
        progessBar.Init(time);
        return progessBar;
    }

}
