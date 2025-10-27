using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class StoveCounter : BaseCounter
{
    [SerializeField] CounterType _counterType;
    [SerializeField] GameObject _fryEffectSizzling;
    [SerializeField] GameObject _fryEffectStoveOn;

    private bool _cooking = false;  
    private Coroutine _currentCoroutine;
    private ProgessBar _progessBar;

    public override void SetKitchenObj(KitchenObj objKitchen)
    {
        if(_currentCoroutine != null)
        {
            _cooking = false;
            StopCoroutine(_currentCoroutine);
            _progessBar?.DespawnerProgessBar();
            _progessBar = null;
            _currentCoroutine = null;
        }    

        if (objKitchen == null)
        {
            _cooking = false;
            return;
        }

        _objKitchen = objKitchen;
        ProcessRule rule = RecipeManager.Instance.GetOutputKitchen(_counterType, _objKitchen.GetKitchenType());
        if (rule == null) return;
        _currentCoroutine = StartCoroutine(HandleCooking(rule, objKitchen));
    }  

    private IEnumerator HandleCooking(ProcessRule rule, KitchenObj obj)
    {
        _cooking = true;
        _progessBar = ProgessManager.Instance.CreateProgessBar(_holdKitchenTransform, rule.processTime);
        float elapsedTime = 0;
        while (elapsedTime < rule.processTime)
        {
            elapsedTime += Time.deltaTime;
            // logic

            if(_cooking == false)
            {
                _progessBar.DespawnerProgessBar();
                _progessBar = null;
                yield break;
            }

            yield return null;
        }
        _currentCoroutine = null;
        SpawnKitchen(rule, obj);    
    }


    private void SpawnKitchen(ProcessRule rule, KitchenObj obj)
    {
        PlayerInteraction player = obj.GetPlayerInteraction();
        PoolManager.Instance.Despawner(obj.gameObject);
        var resultProcess = PoolManager.Instance.Spawner(rule.resultPrefab, _holdKitchenTransform.position, Quaternion.identity, _holdKitchenTransform);
        if (resultProcess.TryGetComponent<KitchenObj>(out obj))
        {
            obj.Init(this);
            if (player != null) player.SetSelectedCounter(obj);      
        }
       
    }

}
