using UnityEngine;
/// <summary>
/// 싱글턴 패턴 배치용 클래스
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            { SetupInstance(); }
            return instance;
        }
    }
    private static void SetupInstance()
    {
        instance = FindAnyObjectByType<T>();
        if (instance == null)
        {
            instance = new GameObject(typeof(T).Name).AddComponent<T>();
            DontDestroyOnLoad(instance.gameObject);
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        { Destroy(Instance.gameObject); }
        else
        { DontDestroyOnLoad(this.gameObject); }
    }
}
