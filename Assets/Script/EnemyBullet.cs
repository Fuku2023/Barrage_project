using UnityEngine;

/// <summary>
/// エネミーの弾クラス
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    // BulletPoolクラス参照
    private EnemyBulletPool _objectPool2;
    // 弾の攻撃力
    [SerializeField]
    public int _bulletPower = default;
    // 弾の横移動
    private float _moveWidth = default;
    // 弾の縦移動
    private float _moveHeight = default;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // オブジェクトプールを取得
        _objectPool2 = transform.parent.GetComponent<EnemyBulletPool>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 弾の更新処理
    /// </summary>
    private void Update()
    {
        // 弾の移動
        transform.position += new Vector3(_moveWidth, _moveHeight, 0) * Time.deltaTime;
    }
    /// <summary>
    /// 弾の角度と速さ計算
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="speed">速さ</param>
    public void Setting(float angle, float speed)
    {
        // 敵の右側が0度として反時計回りに角度を増やす
        _moveWidth = Mathf.Cos(angle) * speed;
        _moveHeight = Mathf.Sin(angle) * speed;

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
        transform.position = pos;
    }
    /// <summary>
    /// 自身の弾の回収処理
    /// </summary>
    public void HideFromStage()
    {
        // オブジェクトプールのCollect関数を呼び出し自身を回収
        _objectPool2.Collect(this);
    }

}

