using System.Collections.Generic;
using UnityEngine;

public abstract class SearcherBehavior : MonoBehaviour
{
    [SerializeField] private QueryTriggerInteraction queryTriggerInteraction;
    [SerializeField] private LayerMask SearchLayerMask;
    [SerializeField] private float Radius = 1f;
    [SerializeField] private float SearchInterval = 1f;
    [SerializeField] private float MaxDistance = 1f;

    private Dictionary<System.Type, RaycastHit[]> hitListLookUp = new Dictionary<System.Type, RaycastHit[]>();

    public void StartSearch<T>(System.Action<List<T>> callback, int hitLength) where T : MonoBehaviour
    {
        hitListLookUp.Add(typeof(T), new RaycastHit[hitLength]);

        DelayManager.WaitAndInvoke(() => Search(callback), t: SearchInterval, repeat: -1);
    }

    private void Search<A>(System.Action<List<A>> callback) where A : MonoBehaviour
    {
        var _foundedObject = new List<A>();

        var hits = hitListLookUp[typeof(A)];
        var length = Physics.SphereCastNonAlloc(transform.position, Radius, transform.up, hits, MaxDistance, SearchLayerMask, queryTriggerInteraction);
        if (length == 0) return;

        for (int i = 0; i < length; i++)
        {
            if (hits[i].transform.TryGetComponent(out A obj))
                if (!_foundedObject.Contains(obj))
                {
                    var dot = Vector3.Dot(transform.forward, obj.transform.position - transform.position);
                    if (dot >= 0f)
                        _foundedObject.Add(obj);
                }
        }

        callback.Invoke(_foundedObject);

#if UNITY_EDITOR
        _editorRadius = 0f;
#endif
    }

#if UNITY_EDITOR
    [SerializeField] private bool DrawGizmos = true;

    private float _editorRadius = 0f;

    private void OnDrawGizmos()
    {
        if (!DrawGizmos) return;
        _editorRadius = Mathf.Lerp(_editorRadius, Radius, Time.deltaTime * 10f);
        Gizmos.DrawWireSphere(transform.position, _editorRadius);
    }
#endif
}