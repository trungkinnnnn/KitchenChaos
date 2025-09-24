using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CounterVisuals : MonoBehaviour, IInteraction, ICounterInfo, ICounterSpawner, IPickable, IPlaceable
{
    [SerializeField] string _nameCounter;

    [Header("Selected")]
    [SerializeField] MeshRenderer _meshRenderer;    
    [SerializeField] Material _materialNormal;
    [SerializeField] Material _materialSelected;

    [Header("Obj")]
    [SerializeField] Transform _topPositionSpawn;
    [SerializeField] GameObject _objSpawn;

    private ISpawner _spawner;
    private KitchenObj _objKitchen;
    private PlayerInteraction _player;
    private bool _selected = false; 
    
    private void Awake()
    {
        _spawner = SpawnerFactory.GetSpawner();
    }


    public string GetNameCounter() => _nameCounter;
    public Vector3 GetPositionRespawn() => _topPositionSpawn.position;
    public bool CheckNullKitChenObj() => _objKitchen == null;
    public void SetObjKitchen(KitchenObj objKitchen) => _objKitchen = objKitchen;


    public void SelectedCounter()
    {
        _selected = !_selected;
        Material[] mats = _meshRenderer.materials;
        mats[0] = _selected ? _materialSelected : _materialNormal;
        _meshRenderer.materials = mats;
    }

    public void ReSpanwKithenObj()
    {
        if (_objKitchen == null)
        {
            GameObject obj = _spawner.Spawn(_objSpawn, _topPositionSpawn.position, Quaternion.identity);
            _objKitchen = obj.GetComponent<KitchenObj>();
            _objKitchen.GetNameObjKitchen();
            _objKitchen.SetCounterVisuals(this);
        }
        else
        {
            _objKitchen.GetCounterVisuals();
        }
    }


    public void PickUp(PlayerInteraction player, Transform transform, Vector3 position)
    {
        
    }

    public void Place(PlayerInteraction player, Transform transform, Vector3 position)
    {
        
    }

    public void Interact(PlayerInteraction player)
    {
        _player = player;
        Debug.Log(_player.name);
    }

    public void OnSelected()
    {
        SelectedCounter();
    }

    public void OnDeselected()
    {
        SelectedCounter();
    }

    public void Spawner(PlayerInteraction player)
    {
        if (_player == player) ReSpanwKithenObj();
    }

    public string GetName(PlayerInteraction player)
    {
        string name = "";
        if (_player == player) name = GetNameCounter();
        return name;
    }

    public System.Numerics.Vector3 GetPositionSpawer()
    {
        throw new System.NotImplementedException();
    }

    public bool CheckNUll()
    {
        return CheckNullKitChenObj();
    }

    public void SetKitchenObj(KitchenObj kitchenObj)
    {
      
    }
}
