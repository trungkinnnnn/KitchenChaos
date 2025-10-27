using UnityEngine;

public class ContainerCounter : BaseCounter, ICounterSpawner
{
    [SerializeField] GameObject _kitchenPrefab;

    private static int _PARA_ANI_HAS_SELECTED = Animator.StringToHash("IsSelected");
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Selected(bool selected)
    {
        base.Selected(selected);
        if (_animator != null && !_hasKitchenObj) _animator.SetBool(_PARA_ANI_HAS_SELECTED, selected);
    }


    // ========================
    // ICounterSpawner
    // ========================
    public void SpawnerKitchen(PlayerInteraction player)
    {
        if (player != _player || _hasKitchenObj) return;
        var obj = PoolManager.Instance.Spawner(_kitchenPrefab, _holdKitchenTransform.position, Quaternion.identity, _holdKitchenTransform);
        if(obj.TryGetComponent<KitchenObj>(out var kitchenObj))
        {
            kitchenObj.Init(this);
        }
        OnDeselected();
    }
}
