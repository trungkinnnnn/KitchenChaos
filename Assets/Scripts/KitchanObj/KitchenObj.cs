using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class KitchenObj : MonoBehaviour
{
    [SerializeField] KitchenObjSO _kitchenObjSO;

    private ISpawner _spawner;
    private CounterVisuals _visuals;

    private void Awake()
    {
        _spawner = SpawnerFactory.GetSpawner();
    }

    private void OnEnable()
    {
        //StartCoroutine(SetActive(10f));
    }
    public void GetNameObjKitchen()
    {
        Debug.Log(_kitchenObjSO.nameObj);
    }

    private IEnumerator SetActive(float time)
    {
        yield return new WaitForSeconds(time);
        _spawner.Despawn(gameObject);
    }

    public void SetCounterVisuals(CounterVisuals visuals)
    {
        _visuals = visuals;
    }  

    public void GetCounterVisuals() => Debug.Log(_visuals);
        

}
