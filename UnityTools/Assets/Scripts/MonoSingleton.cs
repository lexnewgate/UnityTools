using UnityEngine;

namespace Lex.UnityTools
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T s_instance;

        /// <summary>
        /// Caution when use it in awake.
        /// The singleton may call awake later, then the singleton go will be destroyed.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    //here is a hack to make sure singleton when scripts are reloaded in editor
                    s_instance = FindObjectOfType<T>();

                    if (s_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        s_instance = obj.AddComponent<T>();
                    }
                }

                return s_instance;
            }
        }

        /// <summary>
        /// Use this for initialization.
        /// Has to call base.Awake() in the child class.
        /// </summary>
        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}