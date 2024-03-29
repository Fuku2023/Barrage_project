using UnityEngine;

/// <summary>
/// プレイヤーの弾クラス
/// </summary>
public class BulletScript1 : MonoBehaviour
{
    // BulletPoolクラス参照
    private BulletPool1 _objectPool1;
    // 弾の速さ
    [SerializeField]
    private float _speed = default;
    // 弾の攻撃力
    [SerializeField]
    public int _bulletPower = default;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Start()
    {
        // オブジェクトプールを取得
        _objectPool1 = transform.parent.GetComponent<BulletPool1>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 弾の移動処理
    /// </summary>
    private void Update()
    {
        // 弾の移動
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    /// <summary>
    /// 回収処理を呼び込むメソッド
    /// </summary>
    private void OnBecameInvisible()
    {
        // 回収処理を呼び出す
        HideFromStage();
    }
    /// <summary>
    /// 渡された座標に弾を移動する処理
    /// </summary>
    /// <param name="pos">場所</param>
    public void ShowInStage(Vector3 pos)
    {
        // positionを渡された座標に設定
        transform.position += pos;
    }
    /// <summary>
    /// 自身の弾の回収処理
    /// </summary>
    public void HideFromStage()
    {
        // オブジェクトプールのCollect関数を呼び出し自身を回収
        _objectPool1.Collect(this);
    }
}
