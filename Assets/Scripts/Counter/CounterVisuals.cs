using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEditor;

public class CounterVisuals : MonoBehaviour, ISelectable, ICounterInfo, ICounterSpawner, IPickable
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
    
    private void Awake()
    {
        _spawner = SpawnerFactory.GetSpawner();
    }


    public string GetNameCounter() => _nameCounter;
    public Vector3 GetPositionRespawn() => _topPositionSpawn.position;
    public bool CheckNullKitChenObj() => _objKitchen == null;
    public void SetObjKitchen(KitchenObj objKitchen) => _objKitchen = objKitchen;


    public void SelectedCounter(bool selected)
    {
        Material[] mats = _meshRenderer.materials;
        mats[0] = selected ? _materialSelected : _materialNormal;
        _meshRenderer.materials = mats;
    }

    private bool ReSpanwKithenObj()
    {
        if (_objKitchen != null) return false;

        GameObject obj = _spawner.Spawn(_objSpawn, _topPositionSpawn.position, Quaternion.identity);
        _objKitchen = obj.GetComponent<KitchenObj>();
        _objKitchen.GetNameObjKitchen();
        _objKitchen.SetCounterVisuals(this);
        return true;
    }


    public KitchenObj PickUpKitchen(PlayerInteraction player, Transform transform)
    {
        if(_objKitchen != null && _objKitchen is IPickable pickable) {
            pickable.Interact(player);
            _objKitchen = null;
            return pickable.PickUp(player, transform);
        }
        return null;
    }    

    //public void PlaceKitchen(Transform transform = null)
    //{
    //    if(_objKitchen == null)return;
    //    _objKitchen.PlaceKitchen(transform);

    //}

    // Interface

    public void Interact(PlayerInteraction player)
    {
        _player = player;
    }

    public KitchenObj PickUp(PlayerInteraction player, Transform transform)
    {
        if (_player == player) return PickUpKitchen(player, transform);
        return null;
    }

    public void Place(PlayerInteraction player, Transform transform)
    {
        //if(_player == player)PlaceKitchen();
    }

    public void OnSelected(PlayerInteraction player)
    {
        _player = player;
        Debug.Log(_player.name);

        SelectedCounter(true);
    }

    public void OnDeselected()
    {
        SelectedCounter(false);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool Spawner(PlayerInteraction player)
    {
        if (_player == player) return ReSpanwKithenObj();
        return false;
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
