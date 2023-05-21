using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }

            if (instance == null)
            {
                instance = new GameObject().AddComponent<T>();
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
}
