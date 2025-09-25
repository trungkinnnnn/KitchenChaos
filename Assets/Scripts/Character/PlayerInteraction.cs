using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameInput _gameInput;
    [SerializeField] LayerMask _counterMark;
    [SerializeField] float _maxDirectionMark = 2f;
    [SerializeField] Transform _holdPoint;

    private float _radius = 0.5f;
    private KitchenObj _kitchenObj;
    private ISelectable _currentSelectable;
    private Transform _currentTransform;

    private void Start()
    {
        _gameInput.OnInteractAction += OnInteractAction; ;
    }

    private void OnDestroy()
    {
        _gameInput.OnInteractAction -= OnInteractAction;
    }

    private void OnInteractAction(object sender, System.EventArgs e)
    {
        if (_currentSelectable == null) return;

        if (_currentSelectable is ICounterSpawner spawner)
        {
            bool result = spawner.Spawner(this);
            if (result) return;
        }

        if (_currentSelectable is IPickable pickable)
        {
            if(_kitchenObj != null && _kitchenObj is IPickable place)
            {
                place.Place(this);
            }

            KitchenObj obj = pickable.PickUp(this, _holdPoint);
            if (obj != null)
            {
                _kitchenObj = obj;
                return;
            }
        }

        
    }

    private void Update()
    {
        if(_gameInput.GetMovementVectorNormalized() != Vector3.zero)
        {
            HandleSelectedCounter();
        }    
    }

    private void HandleSelectedCounter()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, _radius, out hit, _maxDirectionMark, _counterMark))
        {

            if(_currentSelectable == null || hit.transform != _currentTransform)
            {
                var selectable = hit.transform.GetComponent<ISelectable>();
        
                _currentSelectable?.OnDeselected();
                _currentSelectable = selectable;
                _currentTransform = selectable.GetTransform();
                _currentSelectable.OnSelected(this);
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

}
