
using UnityEngine;

public class ItemTransformer : MonoBehaviour
{
    [SerializeField] private GameObject _commonItemModel;

    public void TransformItem(ItemData itemData)
    {
        if (itemData == null || _commonItemModel == null)
        {
            return;
        }

        GameObject commonModel = Instantiate(
            _commonItemModel,
            itemData.transform.position,
            itemData.transform.rotation,
            itemData.transform
        );

        commonModel.transform.localPosition = Vector3.zero;
        commonModel.transform.localRotation = Quaternion.identity;
    }
}
