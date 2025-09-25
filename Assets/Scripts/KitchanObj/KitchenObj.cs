using System.Collections;
using UnityEngine;
using DG;
using DG.Tweening;

public class KitchenObj : MonoBehaviour,ISelectable, IPickable
{
    [SerializeField] KitchenObjSO _kitchenObjSO;
    [SerializeField] KitchenSelectedSO _kitchenSelectedSO;
    [SerializeField] MeshRenderer _kitchenMesh;

    private static string _PARA_BASEMAP_MAT =  "_BaseMap";

    private ISpawner _spawner;
    private PlayerInteraction _player;
    private CounterVisuals _visuals;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _spawner = SpawnerFactory.GetSpawner();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //StartCoroutine(SetActive(10f));
       
    }
    public void GetNameObjKitchen()
    {
        Debug.Log(_kitchenObjSO.nameObj);
    }

    private IEnumerator SetActive(float time)
    {
        yield return new WaitForSeconds(time);
        _spawner.Despawn(gameObject);
    }

    public void SetCounterVisuals(CounterVisuals visuals)
    {
        _visuals = visuals;
    }  

    public void GetCounterVisuals() => Debug.Log(_visuals);

    private void OnSelectedChangeMat(bool selected)
    {
        Material[] mat = _kitchenMesh.materials;
        _kitchenSelectedSO.selectedMat.SetTexture(_PARA_BASEMAP_MAT, _kitchenSelectedSO.seletedTexture2D);
        mat[0] = selected ? _kitchenSelectedSO.selectedMat : _kitchenSelectedSO.normalMat; 
        _kitchenMesh.materials = mat;
    }

    public KitchenObj PickUpKitchen(Transform transform, float duration = 0.3f)
    {
        _rigidbody.isKinematic = true;

        this.transform.DOKill();    
        
        this.transform.DORotateQuaternion(transform.rotation, duration).SetEase(Ease.OutQuad);
        this.transform.DOMove(transform.position, duration).SetEase(Ease.OutQuad).OnComplete(() => 
        { 
            this.transform.SetParent(transform, false); 
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        });
        
        return this;
    }    

    public void PlaceKitchen(Transform transform = null)
    {
        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;

        this.transform.SetParent(transform, false);
        _rigidbody.isKinematic = false;

        this.transform.position = position;
        this.transform.rotation = rotation;
        _player = null;
    }

    // Interface

    public void Interact(PlayerInteraction player)
    {
        _player = player;
    }

    public KitchenObj PickUp(PlayerInteraction player, Transform transform)
    {
        if(_player == player)
        {
            return PickUpKitchen(transform);
        }
        return null;
    }
    public void Place(PlayerInteraction player, Transform transform = null)
    {
        if (_player == player)
        {
            PlaceKitchen();
        }
    }

    public void OnSelected(PlayerInteraction player)
    {
        _player = player;
        Debug.Log(_player.name);
        OnSelectedChangeMat(true);
        GetNameObjKitchen();
    }

    public void OnDeselected()
    {
        OnSelectedChangeMat(false);
    }

    public Transform GetTransform()
    {
        return transform;
    }


}
