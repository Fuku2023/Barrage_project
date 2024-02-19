using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// エネミーの弾のプール
/// </summary>
public class EnemyBulletPool : MonoBehaviour
{
    // 弾のprefab
    [SerializeField]
    private EnemyBullet _bullet;
    // 生成した弾を格納するbulletQueue
    Queue<EnemyBullet> _bulletQueue;
    // 初回生成時のポジション
    Vector3 _setPos = new Vector3(100, 100, 0);
    // 発射のインターバル
    private float _interval = 0.5f;
    // 角度の計算に使う値
    private int _calculation = 2;
    // 速度半分
    private float _halfAngle = 2f;
    // 何回攻撃パターンを行うか
    private int _atakkCount = 3;
    // 攻撃回数(ウェーブ状)
    private int _waveAtakk = 20;
    // 攻撃回数(ランダム状)
    private int _randomAtakk = 10;
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        // bulletQueueの初期化
        _bulletQueue = new Queue<EnemyBullet>();
        // 発射用コルーチンスタート
        StartCoroutine(CPU());
    }
    /// <summary>
    /// 弾の処理
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="speed">速さ</param>
    private void Shot(float angle, float speed)
    {
        // 弾の生成
        EnemyBullet bullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);
        // 弾の角度,速さ
        bullet.Setting(angle, speed);
        // bulletQueueに追加
        _bulletQueue.Enqueue(bullet);
    }
    /// <summary>
    /// 弾の発射
    /// </summary>
    /// <returns></returns>
    IEnumerator CPU()
    {
        // while(カッコの中がtrueの間繰り返し処理をする)
        while (true)
        {
            // ウェーブ状の弾
            yield return WaveShot(_atakkCount, _waveAtakk);
            // 一定時間ごとに弾を発射
            yield return new WaitForSeconds(_interval);
            // ランダム状の弾
            yield return RandomShot(_atakkCount, _randomAtakk);
            // 一定時間ごとに弾を発射
            yield return new WaitForSeconds(_interval);
        }
    }
    /// <summary>
    /// 最初の弾の発射(ウェーブ状)
    /// </summary>
    /// <param name="wave">何回パターン化するか</param>
    /// <param name="atakk">攻撃回数</param>
    /// <returns></returns>
    IEnumerator WaveShot(int wave, int atakk)
    {
        // 4回8方向に撃ちたい
        for (int i = 0; i < wave; i++)
        {
            // 一定時間ごとに弾を発射
            yield return new WaitForSeconds(_interval);
            // ウェーブ状の弾の発射処理呼び込み
            ShotWaveProsess(atakk, _calculation);
        }
    }
    /// <summary>
    /// 途中からの弾の発射(ランダム状)
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="atakk"></param>
    /// <returns></returns>
    IEnumerator RandomShot(int wave, int atakk)
    {
        // 4回8方向に撃ちたい
        for (int i = 0; i < wave; i++)
        {
            // 一定時間ごとに弾を発射
            yield return new WaitForSeconds(_interval);
            // ランダム状の弾の発射処理呼び込み
            yield return RandomShotProsess(atakk, _calculation);
        }
    }
    /// <summary>
    /// 弾の発射(ランダム状)
    /// </summary>
    /// <param name="count">弾のカウント</param>
    /// <param name="speed">弾の速さ</param>
    /// <returns></returns>
    IEnumerator RandomShotProsess(int count, float speed)
    {
        // 弾カウント
        int bulletCount = count;
        // 弾のカウント以内なら弾発射
        for (int i = 0; i < bulletCount; i++)
        {
            // 360度に弾を発射
            float angle = i * (_calculation * Mathf.PI / bulletCount);
            // 計算した位置に弾を生成
            Shot(angle - Mathf.PI / _halfAngle, speed);
            // 計算した位置に弾を生成
            Shot(-angle - Mathf.PI / _halfAngle, speed);
            // 一定時間ごとに弾を発射
            yield return new WaitForSeconds(_interval);
        }
    }
    /// <summary>
    /// 弾の発射(ウェーブ状)
    /// </summary>
    /// <param name="count">弾のカウント</param>
    /// <param name="speed">弾の速さ</param>
    public void ShotWaveProsess(int count, float speed)
    {
        // 弾カウント
        int bulletCount = count;
        // 弾のカウント以内なら弾発
        for (int i = 0; i < bulletCount; i++)
        {
            // 360度に弾を発射
            float angle = i * (_calculation * Mathf.PI / bulletCount);
            // 計算した位置に弾を発射
            Shot(angle, speed);
        }
    }
    /// <summary>
    /// 弾を貸し出す処理
    /// </summary>
    /// <param name="pos">場所</param>
    /// <returns></returns>
    public EnemyBullet Launch(Vector3 pos)
    {
        // bulletQueueが空ならnull
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueueから弾を一つ取り出す
        EnemyBullet tmpBullet = _bulletQueue.Dequeue();
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
    public void Collect(EnemyBullet bullet)
    {
        // 弾のゲームオブジェクトを非表示
        bullet.gameObject.SetActive(false);
        // bulletQueueに格納
        _bulletQueue.Enqueue(bullet);
    }
}
