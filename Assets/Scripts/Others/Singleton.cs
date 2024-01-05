using UnityEngine;

namespace Others
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance => instance;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                //DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
