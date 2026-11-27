using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        //シーンをまたいでもデータが消えないようにした
        DontDestroyOnLoad(this.gameObject);
    }
}
