using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// プレイヤーの弾のプール
/// </summary>
public class BulletPool : MonoBehaviour
{
    // 弾のprefab
    [SerializeField] 
    private BulletScript _bullet;
    // 生成する数
    [SerializeField]
    private int _maxCount = default;
    // 生成した弾を格納するQueue
    Queue<BulletScript> _bulletQueue;
    // 初回生成時のポジション
    [SerializeField]
    private Transform _setPos;
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        // bulletQueueの初期化
        _bulletQueue = new Queue<BulletScript>();
        // 弾を生成するループ
        for (int i = 0; i < _maxCount; i++)
        {
            // 弾の生成
            BulletScript tmpBullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            // bulletQueueに追加
            _bulletQueue.Enqueue(tmpBullet);
        }
    }
    /// <summary>
    /// 弾を貸し出す処理
    /// </summary>
    /// <param name="pos">場所</param>
    /// <returns></returns>
    public BulletScript Launch(Vector3 pos)
    {
        //Queueが空ならnull
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueueから弾を一つ取り出す
        BulletScript tmpBullet = _bulletQueue.Dequeue();
        // 弾を表示する
        tmpBullet.gameObject.SetActive(true);
        // 渡された座標に弾を移動する
        tmpBullet.ShowInStage(pos);
        // 呼び出し元に渡す
        return tmpBullet;
    }
    /// <summary>
    /// 弾の回収処理
    /// </summary>
    /// <param name="bullet">弾</param>
    public void Collect(BulletScript bullet)
    {
        // 弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);
        // bulletQueueに格納
        _bulletQueue.Enqueue(bullet);
    }
}
