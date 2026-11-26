using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public GameObject cover;
    public ItemBase item;
}

public struct ItemBase
{
    [SerializeField] private GameObject itemOBJ;//ゲームオブジェクト本体
    [field:SerializeField] public int point { get; private set; }
    public string name;
    public Image itemImage;
}
