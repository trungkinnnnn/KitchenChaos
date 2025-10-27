using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgessBar : MonoBehaviour
{
    [SerializeField] Image _imageFill;

    private void OnEnable()
    {
        if(_imageFill != null)
            _imageFill.fillAmount = 1;
    }

    public void Init(float maxTime)
    {
        StartCoroutine(WaitSeconeFillOut(maxTime));
    }    

    private IEnumerator WaitSeconeFillOut(float maxTime)
    {
        float time = 0;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            _imageFill.fillAmount = Mathf.Lerp(0, 1, time / maxTime);
            yield return null;
        }
        PoolManager.Instance.Despawner(gameObject);
    }    

    public void DespawnerProgessBar()
    {
        PoolManager.Instance.Despawner(gameObject);
    }
}
