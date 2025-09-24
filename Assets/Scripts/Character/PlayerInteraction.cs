using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameInput _gameInput;
    [SerializeField] LayerMask _counterMark;
    [SerializeField] float _maxDirectionMark = 2f;


    private float _radius = 0.5f;
    private IInteraction _currentSelectable;
    private Transform _currentTransform;

    private void Start()
    {
        _gameInput.OnInteractAction += _gameInput_OnInteractAction;
    }

    private void _gameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (_currentSelectable != null)
        {
            _currentSelectable.Interact(this);
            if (_currentSelectable is ICounterInfo info) info.GetName(this);
            if (_currentSelectable is ICounterSpawner spawner) spawner.Spawner(this);
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
                var counter = hit.transform.GetComponent<CounterVisuals>();
                if(counter is IInteraction interaction)
                {
                    _currentSelectable?.OnDeselected();
                    _currentSelectable = interaction;
                    _currentTransform = counter.transform;
                    _currentSelectable.OnSelected();
                }
               
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
