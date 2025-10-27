using System.Collections;
using System.Data;
using UnityEngine;

public class SlicesCounter : BaseCounter
{
    public static int _PARA_ANI_HAS_SLICES = Animator.StringToHash("IsSlices");

    [SerializeField] CounterType _counterType;
    [SerializeField] float _changeColliderCenterY = -0.1f;
    private Vector3 _originalCenterCollider;
    private Animator _animator;
    private BoxCollider _boxCollider;   
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _originalCenterCollider = _boxCollider.center;
    }


    public override void SetKitchenObj(KitchenObj objKitchen)
    {
        base.SetKitchenObj(objKitchen);

        if (objKitchen == null) return;

        ProcessRule rule = RecipeManager.Instance.GetOutputKitchen(_counterType, objKitchen.GetKitchenType());
        if (rule == null) return;
        StartSlice(rule, objKitchen);
    }

    private void StartSlice(ProcessRule rule, KitchenObj obj)
    {
        //obj.SetColliderKitchen(false);
        StartCoroutine(WaitForSeconds(rule, obj));  
    }    

    private IEnumerator WaitForSeconds(ProcessRule rule, KitchenObj obj)
    {
        obj.SetColliderKitchen(false); 
        _animator.SetBool(_PARA_ANI_HAS_SLICES, true);
        ChangePositionCollider(true);
        ProgessManager.Instance.CreateProgessBar(_holdKitchenTransform, rule.processTime);

        yield return new WaitForSeconds(rule.processTime);
        ChangePositionCollider(false);
        _animator.SetBool(_PARA_ANI_HAS_SLICES, false);
        SpawnKitchen(rule, obj);
    }    

    private void SpawnKitchen( ProcessRule rule, KitchenObj obj)
    {
        PoolManager.Instance.Despawner(obj.gameObject);
        var resultProcess = PoolManager.Instance.Spawner(rule.resultPrefab, _holdKitchenTransform.position, Quaternion.identity, _holdKitchenTransform);
        if (resultProcess.TryGetComponent<KitchenObj>(out obj)) obj.Init(this);
    }    

    private void ChangePositionCollider(bool value)
    {
        Vector3 center = _boxCollider.center;
        if (value) center.y = _changeColliderCenterY;
        else center.y = _originalCenterCollider.y;

        _boxCollider.center = center;
    }

}
