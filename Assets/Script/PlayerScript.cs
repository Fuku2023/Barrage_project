/*--------------------------------------------------
作成日:12月2日

作成者:福原 龍弥
---------------------------------------------------*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤークラス
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // BulletPoolクラス参照
    [SerializeField]
    private BulletPool _bulletPool;
    // BulletPool1クラス参照
    [SerializeField]
    private BulletPool1 _bulletPool1;
    // 効果音参照
    [SerializeField]
    private AudioClip _audio;
    // 水平移動の速度
    [SerializeField]
    private float _horSpeed = default;
    // 垂直移動の速度
    [SerializeField]
    private float _verSpeed = default;
    // Playerの上の移動制御の変数
    [SerializeField]
    private float _topMax = default;
    // Playerの下の移動制御の変数
    [SerializeField]
    private float _bottomMax = default;
    // Playerの右の移動制御の変数
    [SerializeField]
    private float _rigthSideMax = default;
    // Playerの左の移動制御の変数
    [SerializeField]
    private float _leftSideMax = default;
    // 弾を打つ速度
    [SerializeField]
    private float _interval = default;
    // 点滅の間隔
    [SerializeField]
    private float _flashInterval = default;
    // 点滅させるときのループカウント(点滅回数)
    [SerializeField] int _loopCount;
    // 体力ゲージ
    [SerializeField]
    private Slider _slider;
    // プレイヤーのHP
    private int _playerHp = 300;
    // 敵の弾のダメージ
    private int _nomalDamage = 10;
    // 敵の弾のダメージ(ハード)
    private int _heardDamage = 20;
    // シーン移動するまでの時間
    private float _sceneInterval = 1.5f;
    // 点滅させるためのSpriteRenderer
    private SpriteRenderer _spriteRenderer;
    // コライダーをオンオフするためのCollider2D
    private CapsuleCollider2D _collider2D;
    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // 発射用コルーチンスタート
        StartCoroutine(Shot());
        // SpriteRenderer格納
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // CapsuleCollider格納
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// 弾の更新処理
    /// </summary>
    public void Update()
    {
        // プレイヤー移動メソッド呼び込み
        PlayerMove();
        // プレイヤーの移動制御メソッド呼び込み
        MoveLimit();
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
            // オブジェクトプールのLaunch関数呼び込み ------------
            // 中央の弾
            _bulletPool.Launch(transform.position);
            // 中央の弾ではなく両サイドの弾
            _bulletPool1.Launch(transform.position);
            // ---------------------------------------------------
            // 発射のインターバル
            yield return new WaitForSeconds(_interval);
        }
    }

    /// <summary>
    /// プレイヤーの移動制御
    /// </summary>
    public void MoveLimit()
    {
        // 右制限
        if (transform.localPosition.x > _rigthSideMax)
        {
            // 範囲内の移動
            transform.localPosition = new Vector2(_rigthSideMax, transform.localPosition.y);
        }
        // 左制限
        if (transform.localPosition.x < _leftSideMax)
        {
            // 範囲内の移動
            transform.localPosition = new Vector2(_leftSideMax, transform.localPosition.y);
        }
        // 上制限
        if (transform.localPosition.y > _topMax)
        {
            // 範囲内の移動
            transform.localPosition = new Vector2(transform.localPosition.x, _topMax);
        }
        // 下制限
        if (transform.localPosition.y < _bottomMax)
        {
            // 範囲内の移動
            transform.localPosition = new Vector2(transform.localPosition.x, _bottomMax);
        }
    }
    /// <summary>
    /// Playerの移動処理
    /// </summary>
    public void PlayerMove()
    {
        // 横入力
        float ver = Input.GetAxis("Vertical");
        // 縦入力
        float hor = Input.GetAxis("Horizontal");
        // 速度
        transform.position += new Vector3(hor * _horSpeed, ver * _verSpeed, 0);
    }
    /// <summary>
    /// 敵の弾が当たった時の処理
    /// </summary>
    /// <param name="collider">コライダー(当たり判定)</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 敵の弾が当たったら
        if (collider.gameObject.tag == "EnemyBullet")
        {
            // プレイヤーのダメージ処理を行う
            NomalHpDown();
            // 点滅コルーチンを開始
            StartCoroutine(Hit());
        }
        // 敵の弾が当たったら(ハードの弾)
        else if (collider.gameObject.tag == "HeardBullet")
        {
            // プレイヤーのダメージ処理を行う(ハード)
            HeardHpDown();
            // 点滅コルーチンを開始
            StartCoroutine(Hit());
        }
    }
    /// <summary>
    /// 敵の弾に当たった時のプレイヤーのHP処理
    /// </summary>
    private void NomalHpDown()
    {
        // プレイヤーのHP減らす
        _playerHp = _playerHp - _nomalDamage;
        // 敵のHPゲージを減らす
        _slider.value = _playerHp - _nomalDamage;
        // プレイヤーのHPが0になったら
        if (_playerHp <= 0)
        {
            // 効果音再生
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // プレイヤー非表示
            gameObject.SetActive(false);
            // 一定時間後にシーン移動
            Invoke("SceneMove", _sceneInterval);
        }
    }
    /// <summary>
    /// 敵の弾に当たった時のプレイヤーのHP処理(ハード)
    /// </summary>
    private void HeardHpDown()
    {
        // プレイヤーのHP減らす
        _playerHp = _playerHp - _heardDamage;
        // 敵のHPゲージを減らす
        _slider.value = _playerHp - _heardDamage;
        // プレイヤーのHPが0になったら
        if (_playerHp <= 0)
        {
            // 効果音再生
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // プレイヤー非表示
            gameObject.SetActive(false);
            // 一定時間後にシーン移動
            Invoke("SceneMove", _sceneInterval);
        }
    }
    /// <summary>
    /// 点滅させる処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Hit()
    {
        // 当たり判定オフにする
        _collider2D.enabled = false;
        // 点滅ループ開始
        for (int i = 0; i < _loopCount; i++)
        {
            // 点滅のインターバル
            yield return new WaitForSeconds(_flashInterval);
            // spriteRendererをオフ
            _spriteRenderer.enabled = false;
            // 点滅のインターバル
            yield return new WaitForSeconds(_flashInterval);
            // spriteRendererをオン
            _spriteRenderer.enabled = true;
        }
        // 当たり判定をオンにする
        _collider2D.enabled = true;
    }
    /// <summary>
    /// シーン遷移
    /// </summary>
    private void SceneMove()
    {
        // ゲームオーバーシーンに移動
        SceneManager.LoadScene("GameOverScene");
    }
}
