using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// プレイヤーの弾のプール
/// </summary>
public class BulletPool1 : MonoBehaviour
{
    // 弾のprefab
    [SerializeField]
    private BulletScript1 _bullet;
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
    Queue<BulletScript1> _bulletQueue;
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        // bulletQueueの初期化
        _bulletQueue = new Queue<BulletScript1>();

        // 弾を生成するループ
        for (int i = 0; i < _maxCount; i++)
        {
            // 弾の生成 --------------------------------
            // 中央の弾
            BulletScript1 tmpBullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            // 両サイドの弾
            BulletScript1 tmpBullet1 = Instantiate(_bullet, _setPos1.position, Quaternion.identity, transform);
            // -----------------------------------------
            // bulletQueueに追加 -----------------------
            // 中央の弾
            _bulletQueue.Enqueue(tmpBullet);
            // 両サイドの弾
            _bulletQueue.Enqueue(tmpBullet1);
            // -----------------------------------------
        }
    }
    /// <summary>
    /// 弾を貸し出す処理
    /// </summary>
    /// <param name="pos">場所</param>
    /// <returns></returns>
    public BulletScript1 Launch(Vector3 pos)
    {
        // bulletQueueが空ならnull
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueueから弾を一つ取り出す
        BulletScript1 tmpBullet = _bulletQueue.Dequeue();
        // bulletQueueから弾をもう一つ取り出す
        BulletScript1 tmpBullet1 = _bulletQueue.Dequeue();
        // 弾を表示する
        tmpBullet.gameObject.SetActive(true);
        // 弾を表示する
        tmpBullet1.gameObject.SetActive(true);
        // 渡された座標に弾を移動する
        tmpBullet.ShowInStage(pos);
        // 渡された座標に弾を移動する
        tmpBullet1.ShowInStage(pos);
        // 呼び出し元に渡す
        return tmpBullet;
    }
    /// <summary>
    /// 弾の回収処理
    /// </summary>
    /// <param name="bullet">弾</param>
    public void Collect(BulletScript1 bullet)
    {
        //弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);
        //bulletQueueに格納
        _bulletQueue.Enqueue(bullet);
    }
}