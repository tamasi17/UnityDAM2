using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    
        void Start()
        {
            Destroy(gameObject, 1f); // matches duration
        }
 

}
