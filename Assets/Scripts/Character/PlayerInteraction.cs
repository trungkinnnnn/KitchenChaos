using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Transform RaycastTransform;
    [SerializeField] GameInput _gameInput;
    [SerializeField] LayerMask _counterMark;
    [SerializeField] float _maxDirectionMark = 2f;
    [SerializeField] Transform _holdPoint;
    
    private float _radius = 0.5f;
    private IPickable _pickObj;
    private ISelectable _currentSelectable;
    private Transform _currentTransform;

    private void Start()
    {
        _gameInput.OnInteract += OnInteract;  
    }

    private void OnDestroy()
    {
        _gameInput.OnInteract -= OnInteract;
    }

    // OnInteract
    private void OnInteract(object sender, System.EventArgs e)
    {
        if (_currentSelectable == null) return;

        if(PickUp(_holdPoint))return;

        Spawner();

    }

    // ===============
    // PickUp
    // ==============

    private bool PickUp(Transform transform)
    {
        bool change = false;
        IPickable obj = null;
        if (_currentSelectable is IPickable pickable)
        {
            change = true;
            obj = HandlePickable(pickable, transform);
        }
        else if(_pickObj != null && _currentSelectable is IKitchenHolder holder)
        {
            change = true;
            _pickObj.PickUp(this, holder.GetTransformHolder(), holder.GetBaseCounter());
        }
        _pickObj = obj;
        StartCoroutine(WaitForWaitForSeconds(0.2f));
        return change;
    }

    private IPickable HandlePickable(IPickable pickable, Transform transform)
    {
        BaseCounter baseCounter = pickable.GetBaseCounter();
        IPickable obj = pickable.PickUp(this, transform);

        if (_pickObj != null) _pickObj.PickUp(this, baseCounter.GetHoldKitchenTransform(), baseCounter);
        return obj;
    }    


    // ================
    // Spawner
    // ================
    private void Spawner()
    {
        if (_currentTransform.TryGetComponent<ICounterSpawner>(out var ICounterSpawner))
        {
            ICounterSpawner.SpawnerKitchen(this);
            HandleSelectedCounter();
        }
    }    

   
    private void Update()
    {
        if(_gameInput.GetMovementVectorNormalized() != Vector3.zero)
        {
            HandleSelectedCounter();
        }    
    }

    public void HandleSelectedCounter()
    {
        Ray ray = new Ray(RaycastTransform.position, Vector3.down);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * _maxDirectionMark, Color.red);
        if (Physics.SphereCast(ray, _radius, out hit, _maxDirectionMark, _counterMark))
        {

            if(_currentSelectable == null || hit.transform != _currentTransform)
            {
                var selectable = hit.transform.GetComponent<ISelectable>();
        
                _currentSelectable?.OnDeselected();
                SetSelectedCounter(selectable);
            }    

        }else
        {
            if(_currentSelectable != null)
            {
                _currentSelectable.OnDeselected();
                _currentSelectable = null;
                _currentTransform = null;
            }    
        }    
    }    

    private IEnumerator WaitForWaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        HandleSelectedCounter();
    }

    public void SetSelectedCounter(ISelectable selectable)
    {
        _currentSelectable = selectable;
        _currentTransform = selectable.GetSelectableTransform();
        _currentSelectable.OnSelected(this);
    }

}
