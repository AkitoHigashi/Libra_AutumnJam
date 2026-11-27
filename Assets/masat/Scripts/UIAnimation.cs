using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    //ターゲットアイテムが表示されるUI（大きさ0）を
    //拡大しながら画面の真ん中から縮小しながら右上に移動する
    [SerializeField] RectTransform _targetUI;
    /// <summary>移動後の座標</summary>
    [SerializeField] Vector2 _targetPos = new Vector2();
    /// <summary>移動後のUIのサイズ</summary>
    [SerializeField] float _targetScale = 0.2f;
    /// <summary>アニメーション時間</summary>
    [SerializeField] float _duration = 0.5f;
    /// <summary>大きさ0のところから拡大する速さ</summary>
    [SerializeField] float _appearDuration = 0.3f;
    
    void Start()
    {
        PlayAnim();
        
    }
    public void PlayAnim()
    {
        
        _targetUI.DOKill(true);
        _targetUI.localScale = Vector3.zero;
        DOTween.Sequence().Append(_targetUI.DOScale(Vector3.one,_appearDuration)).SetEase(Ease.OutBack)
               .Append(_targetUI.DOAnchorPos(_targetPos, _duration)).SetEase(Ease.InBack)
               .Join(_targetUI.DOScale(_targetScale, _duration)).SetEase(Ease.InQuad)
               .SetLink(gameObject);
    }
}
