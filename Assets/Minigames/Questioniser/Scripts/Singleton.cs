using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; protected set; }
    public static bool InstanceExists => Instance != null;

    /// <summary>
    /// Gets the instance of this singleton, and returns true if it is not null.
    /// Prefer this whenever you would otherwise use InstanceExists and Instance together.
    /// </summary>
    public static bool TryGetInstance(out T result)
    {
        result = Instance;

        return result != null;
    }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != (T)this)
        {
            Debug.LogWarningFormat("Trying to create a second instance of {0}", typeof(T));
            Destroy(gameObject);
        }
        else
        {
            Instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}