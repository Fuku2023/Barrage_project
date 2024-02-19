using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHeardPool : MonoBehaviour
{
    // 弾のprefab
    [SerializeField]
    private EnemyHeardBullet _bullet;
    // プレイヤーのオブジェクト参照
    [SerializeField]
    GameObject _playerObj = null;
    // 生成する数
    [SerializeField]
    private int _maxCount = default;
    // 初回生成時のポジション(弾1)
    [SerializeField]
    private Transform _setPos;
    // 初回生成時のポジション(弾2)
    [SerializeField]
    private Transform _setPos1;
    // 生成した弾を格納するbulletQueue
    Queue<EnemyHeardBullet> _bulletQueue;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        // bulletQueueの初期化
        _bulletQueue = new Queue<EnemyHeardBullet>();
        // プレイヤーオブジェクトを取得する
        _playerObj = GameObject.Find("Player");
        // 弾を生成するループ
        for (int i = 0; i < _maxCount; i++)
        {
            if (_playerObj == null) { break; }
            // 弾の生成 --------------------------------
            //右の固定砲台
            EnemyHeardBullet bullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            //左の固定砲台
            EnemyHeardBullet bullet1 = Instantiate(_bullet, _setPos1.position, Quaternion.identity, transform);
            // -----------------------------------------
            // プレイヤーの位置に移動 ------------------
            //右の固定砲台
            bullet.SetMoveVec(_playerObj.transform.position - transform.position);
            //左の固定砲台
            bullet1.SetMoveVec(_playerObj.transform.position - transform.position);
            // -----------------------------------------
            // bulletQueueに追加 -----------------------------
            //右の固定砲台
            _bulletQueue.Enqueue(bullet);
            //左の固定砲台
            _bulletQueue.Enqueue(bullet1);
            // -----------------------------------------
        }
    }
    /// <summary>
    /// 弾を貸し出す処理
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public EnemyHeardBullet Launch(Vector3 vec)
    {
        // bulletQueueが空ならnull
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueueから弾を一つ取り出す 
        EnemyHeardBullet tmpBullet = _bulletQueue.Dequeue();
        // 弾を表示する
        tmpBullet.gameObject.SetActive(true);
        // 渡された座標に弾を移動する
        tmpBullet.SetMoveVec(vec);
        // 呼び出し元に渡す
        return tmpBullet;
    }
    /// <summary>
    /// 弾の回収処理
    /// </summary>
    /// <param name="bullet">弾</param>
    public void Collect(EnemyHeardBullet bullet)
    {
        // 弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);
        // bulletQueueに格納
        _bulletQueue.Enqueue(bullet);
    }
}
