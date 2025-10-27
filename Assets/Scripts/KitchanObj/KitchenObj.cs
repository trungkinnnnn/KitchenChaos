using System.Collections;
using UnityEngine;
using DG;
using DG.Tweening;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;

public class KitchenObj : MonoBehaviour, ISelectable, IPickable
{
    [SerializeField] KitchenObjSO _kitchenObjSO;
    [SerializeField] Vector2 _scaleSeleted = new Vector2(1f, 1.3f);

    private BoxCollider _collider;
    private PlayerInteraction _player;
    private BaseCounter _counter;

    private bool _seleted;  

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void Init(BaseCounter counter, PlayerInteraction player = null)
    {
        _counter = counter;
        _collider.enabled = true;
        _player = player;
        SetParentCounter(counter);
    }

    private void OnSelectedChangeScale(bool selected, float duration = 0.3f)
    {
        _seleted = selected;    
        transform.DOKill();
        transform.DOScale(selected ? _scaleSeleted.y : _scaleSeleted.x, duration).SetEase(Ease.OutQuad);
    }

    public KitchenObj PickUpKitchen(Transform transform, BaseCounter baseCounter = null, float duration = 0.1f)
    {
        this.transform.DOKill();

        this.transform.DORotateQuaternion(transform.rotation, duration).SetEase(Ease.OutQuad);
        this.transform.DOMove(transform.position, duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            this.transform.SetParent(transform, false);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        });

        SetParentCounter(baseCounter);
        return this;
    }

    public void TrashKitchen(float duration = 0.5f)
    {
        _collider.enabled = false;
        transform.DOScale(0f, duration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            PoolManager.Instance.Despawner(gameObject);
        });
    }

    // =================
    // IPickable
    // =================
    public KitchenObj PickUp(PlayerInteraction player, Transform transform, BaseCounter counter = null)
    {
        if (_player == player)
        {
            OnSelectedChangeScale(false);
            return PickUpKitchen(transform, counter);
        }
        return null;
    }

    public BaseCounter GetBaseCounter() => _counter;

    // =================
    // ISelectable
    // =================

    public void OnSelected(PlayerInteraction player)
    {
        _player = player;
        OnSelectedChangeScale(true);
        //GetNameObjKitchen();
    }

    public void OnDeselected() => OnSelectedChangeScale(false);
    public Transform GetSelectableTransform() => transform;

    // Get, Set 
    public void SetColliderKitchen(bool value) => _collider.enabled = value;
    public void SetParentCounter(BaseCounter counter)
    {
        _collider.enabled = counter != null;
        if (counter == null) _counter.SetKitchenObj(null);
        else counter.SetKitchenObj(this);
        _counter = counter;
    }

    public KitchenType GetKitchenType() => _kitchenObjSO.kitchenType;
    public void GetNameObjKitchen() => Debug.Log(_kitchenObjSO.nameObj);
    public PlayerInteraction GetPlayerInteraction()
    {
        if(_seleted) return _player;
        return null;
    }


}
