using UnityEngine;

/// <summary>
/// プレイヤーのHPゲージの位置を更新する処理
/// </summary>
public class UIOverlay : MonoBehaviour
{
    // プレイヤーのオブジェクト参照
    [SerializeField]
    private Transform _targetTransform;
    // UIのコンポーネント
    private RectTransform _rectTransform;
    // UIの位置
    private Vector3 _pos = new Vector3(0, 11, 0);
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // RecrTransform格納
        _rectTransform = GetComponent<RectTransform>();
    }
    /// <summary>
    /// UIの更新処理
    /// </summary>
    private void Update()
    {
        // プレイヤーのオブジェクトに合わせてUI移動
        _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _pos);
    }
}
