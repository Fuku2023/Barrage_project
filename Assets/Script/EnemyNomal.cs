using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// エネミークラス
/// </summary>
public class EnemyNomal : MonoBehaviour
{
    //BulletScriptクラス参照
    private BulletScript _bulletScript;
    //BulletScriptクラス参照
    private BulletScript1 _bulletScript1;
    // BulletPool2クラス参照
    [SerializeField]
    private EnemyBulletPool _bulletPool2;
    // 体力ゲージ
    [SerializeField]
    private Slider _slider;
    // 2種類目の弾のクラス参照
    [SerializeField]
    public EnemyBullet _bullet2;
    // 効果音参照
    [SerializeField]
    private AudioClip _audio;
    // 敵の体力
    private int _enemyHp = 6000;
    // 敵の半分の体力
    private int _heafHp = 3000;
    // 弾を打つ速度
    private float _interval = 0.22f;
    // 弾の打つ速度早くする変数
    private float _heardInterval = 0.125f;
    // シーン移動するまでの時間
    private float _sceneInterval = 2f;
    /// <summary>
    /// 初期化
    /// </summary>
    public void Awake()
    {
        // 発射用コルーチンスタート
        StartCoroutine(Shot());
        // BulletScript格納
        _bulletScript = FindObjectOfType<BulletScript>();
        // BulletScript1格納
        _bulletScript1 = FindObjectOfType<BulletScript1>();
    }
    /// <summary>
    /// プレイヤーの弾が侵入したときの処理
    /// </summary>
    /// <param name="collider">コライダー(当たり判定)</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 青いプレイヤーの弾に当たったら
        if (collider.gameObject.tag == "BlueBullet")
        {
            // 敵の体力処理呼び込み
            EnemyUpdateHp();
        }
        // 赤いプレイヤーの弾に当たったら
        else if (collider.gameObject.tag == "RedBullet")
        {
            // 敵の体力処理呼び込み
            EnemyStoringUpdateHp();
        }
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
            // オブジェクトプールのLaunch関数呼び出し(円を描くように発射する弾発射)
            _bulletPool2.Launch(transform.position);
            // 体力が半分になるまではデフォルトインターバル
            if (_enemyHp > _heafHp) 
            {
                // 発射するインターバル
                yield return new WaitForSeconds(_interval);
            }
            // 体力が半分になったらインターバルを短くする
            if (_enemyHp <= _heafHp) 
            {
                // 短い発射のインターバル
                yield return new WaitForSeconds(_heardInterval);
            }
        }
    }
    /// <summary>
    /// 敵の体力処理
    /// </summary>
    private void EnemyUpdateHp()
    {
        // 体力が0になったら
        if (_enemyHp <= 0)
        {
            // 効果音再生
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // 敵を非表示
            gameObject.SetActive(false);
            // 一定時間後にシーン移動
            Invoke("SceneMove", _sceneInterval);
        }
        // 体力が0より大きかったら
        else if (_enemyHp > 0)
        {
            // 敵のHP減らす
            _enemyHp = _enemyHp - _bulletScript._bulletPower;
            // 敵のHPゲージを減らす
            _slider.value = _enemyHp - _bulletScript._bulletPower;
            //Debug.Log("残り Hp : " + _enemyHp);
        }
    }
    /// <summary>
    /// 敵の体力処理
    /// </summary>
    private void EnemyStoringUpdateHp()
    {
        // 体力が0になったら
        if (_enemyHp <= 0)
        {
            // 効果音再生
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // 敵を非表示
            gameObject.SetActive(false);
            // 一定時間後にシーン移動
            Invoke("SceneMove", _sceneInterval);
        }
        // 体力が0より大きかったら
        else if (_enemyHp > 0)
        {
            // 敵のHP減らす
            _enemyHp = _enemyHp - _bulletScript1._bulletPower;
            // 敵のHPゲージを減らす
            _slider.value = _enemyHp - _bulletScript1._bulletPower;
        }
    }
    /// <summary>
    /// シーン遷移
    /// </summary>
    private void SceneMove()
    {
        // ゲームクリアシーンに移動
        SceneManager.LoadScene("GameClearScene");
    }
}
