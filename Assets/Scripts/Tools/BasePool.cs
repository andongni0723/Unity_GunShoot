using UnityEngine;
using UnityEngine.Pool;

public class BasePool<T> : MonoBehaviour where T : Component
{
    private ObjectPool<T> pool;

    [SerializeField] private T prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int defultSize = 100;
    [SerializeField] private int maxSize = 500;

    //Debug Data
    public int activeCount => pool.CountActive;
    public int inactiveCount => pool.CountInactive;
    public int totalCount => pool.CountAll;
    
    
    protected virtual void Initialize(bool collectionCheck = true) =>
        pool = new ObjectPool<T>(OnCreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, collectionCheck,
                    defultSize, maxSize);
    protected virtual void OnDestroyPoolItem(T obj) => Destroy(obj.gameObject);
    protected virtual void OnReleasePoolItem(T obj) => obj.gameObject.SetActive(false);
    protected virtual void OnGetPoolItem(T obj) => obj.gameObject.SetActive(true);
    protected virtual T OnCreatePoolItem() => Instantiate(prefab,parent);

    public T Get() => pool.Get();
    public void Release(T obj) => pool.Release(obj);
    public void Clear() => pool.Clear();

}
