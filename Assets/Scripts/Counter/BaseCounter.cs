using UnityEngine;

public class BaseCounter : MonoBehaviour, ISelectable, IKitchenHolder
{
    [SerializeField] protected Transform _holdKitchenTransform;
    [SerializeField] protected MeshRenderer _meshRenderer;
    [SerializeField] protected Material _materialNormal;
    [SerializeField] protected Material _materialSelected;

    protected KitchenObj _objKitchen;
    protected PlayerInteraction _player;
    protected bool _hasKitchenObj = false;

    protected virtual void Selected(bool selected)
    {
        if (_materialNormal == null || _materialSelected == null) return;
        Material[] mats = _meshRenderer.materials;
        mats[0] = selected ? _materialSelected : _materialNormal;
        _meshRenderer.materials = mats;
    }

    // =====================
    // ISelectable
    // =====================
    public virtual void OnSelected(PlayerInteraction player)
    {
        _player = player;
        Selected(true);
    }

    public void OnDeselected()
    {
        Selected(false);
        _player = null;
    }

    public Transform GetSelectableTransform() => transform;

    // ========================
    // IKitchenHolder
    // ========================
    public Transform GetTransformHolder() => _holdKitchenTransform;
    public BaseCounter GetBaseCounter() => this;

    // Get, Set
    public virtual void SetKitchenObj(KitchenObj objKitchen)
    {
        _objKitchen = objKitchen;
        _hasKitchenObj = objKitchen == null ? false : true;
    }
    public Transform GetHoldKitchenTransform() => _holdKitchenTransform;
   
}
