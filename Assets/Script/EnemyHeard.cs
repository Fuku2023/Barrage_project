/*---------------------------------------------------------------
 * 作成日:2月1日
 * 
 * 作成者:福原 龍弥
 *---------------------------------------------------------------*/

using System.Collections;
using UnityEngine;

public class EnemyHeard : MonoBehaviour
{
    // 弾のクラス参照
    [SerializeField]
    public EnemyHeardBullet _bullet;
    // BulletPoolクラス参照
    [SerializeField]
    private EnemyHeardPool _bulletPool;
    // プレイヤーオブジェクト参照
    [SerializeField]
    GameObject _playerObj = null;
    // 弾を打つ速度
    private float _interval = 1.1f;
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Start()
    {
        // プレイヤーオブジェクトを取得する:
        _playerObj = GameObject.Find("Player");
        // 発射用コルーチンスタート
        StartCoroutine(Shot());
    }
    /// <summary>
    /// 弾の発射
    /// </summary>
    /// <returns></returns>
    IEnumerator Shot()
    {
        // 発射ループ
        while (true)
        {
            // プレイヤーの位置に発射するオブジェクトプールのLaunch関数呼び込み
            _bulletPool.Launch(_playerObj.transform.position - transform.position);
            // 発射のインターバル
            yield return new WaitForSeconds(_interval);
        }
    }
}
