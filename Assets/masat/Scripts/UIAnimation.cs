using DG.Tweening;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    //ターゲットアイテムが表示されるUIを
    //画面の真ん中から縮小しながら右上に移動する
    [SerializeField] RectTransform _targetUI;
    /// <summary>移動後の座標</summary>
    [SerializeField] Vector2 _targetPos = new Vector2();
    /// <summary>移動後のUIのサイズ</summary>
    [SerializeField] float _targetScale = 0.2f;
    /// <summary>アニメーション時間</summary>
    [SerializeField] float _duration = 0.5f;

    void Start()
    {
        PlayAnim();
    }
    public void PlayAnim()
    {
        
        _targetUI.DOKill(true);

        DOTween.Sequence().Append(_targetUI.DOAnchorPos(_targetPos, _duration)).SetEase(Ease.InBack)
                          .Join(_targetUI.DOScale(_targetScale, _duration)).SetEase(Ease.InQuad)
                          .SetLink(gameObject);
    }
}
