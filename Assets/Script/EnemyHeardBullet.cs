using UnityEngine;

public class EnemyHeardBullet : MonoBehaviour
{
    // 弾の速さ
    [SerializeField] 
    float _moveSpeed = default;
    // 弾の方向
    [SerializeField] 
    Vector3 _moveVec = new Vector3(0, 0, 0);
    // BulletPoolクラス参照
    private EnemyHeardPool _objectPool;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Start()
    {
        // オブジェクトプールを取得
        _objectPool = transform.parent.GetComponent<EnemyHeardPool>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 弾の更新処理
    /// </summary>
    public void Update()
    {
        // 弾の速さ
        float addMove = _moveSpeed * Time.deltaTime;
        // 弾の速さと角度の換算
        transform.Translate(_moveVec * addMove);
    }
    /// <summary>
    /// 弾の発射する角度
    /// </summary>
    /// <param name="vec">角度</param>
    public void SetMoveVec(Vector3 vec)
    {
        // 発射する角度
        _moveVec = vec.normalized;
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
    /// 自身の弾の回収処理
    /// </summary>
    public void HideFromStage()
    {
        // オブジェクトプールのCollect関数を呼び出し自身を回収
        _objectPool.Collect(this);
    }
}
