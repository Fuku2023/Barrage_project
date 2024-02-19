/*--------------------------------------------------
�쐬��:12��2��

�쐬��:���� ����
---------------------------------------------------*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �v���C���[�N���X
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // BulletPool�N���X�Q��
    [SerializeField]
    private BulletPool _bulletPool;
    // BulletPool1�N���X�Q��
    [SerializeField]
    private BulletPool1 _bulletPool1;
    // ���ʉ��Q��
    [SerializeField]
    private AudioClip _audio;
    // �����ړ��̑��x
    [SerializeField]
    private float _horSpeed = default;
    // �����ړ��̑��x
    [SerializeField]
    private float _verSpeed = default;
    // Player�̏�̈ړ�����̕ϐ�
    [SerializeField]
    private float _topMax = default;
    // Player�̉��̈ړ�����̕ϐ�
    [SerializeField]
    private float _bottomMax = default;
    // Player�̉E�̈ړ�����̕ϐ�
    [SerializeField]
    private float _rigthSideMax = default;
    // Player�̍��̈ړ�����̕ϐ�
    [SerializeField]
    private float _leftSideMax = default;
    // �e��ł��x
    [SerializeField]
    private float _interval = default;
    // �_�ł̊Ԋu
    [SerializeField]
    private float _flashInterval = default;
    // �_�ł�����Ƃ��̃��[�v�J�E���g(�_�ŉ�)
    [SerializeField] int _loopCount;
    // �̗̓Q�[�W
    [SerializeField]
    private Slider _slider;
    // �v���C���[��HP
    private int _playerHp = 300;
    // �G�̒e�̃_���[�W
    private int _nomalDamage = 10;
    // �G�̒e�̃_���[�W(�n�[�h)
    private int _heardDamage = 20;
    // �V�[���ړ�����܂ł̎���
    private float _sceneInterval = 1.5f;
    // �_�ł����邽�߂�SpriteRenderer
    private SpriteRenderer _spriteRenderer;
    // �R���C�_�[���I���I�t���邽�߂�Collider2D
    private CapsuleCollider2D _collider2D;
    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        // ���˗p�R���[�`���X�^�[�g
        StartCoroutine(Shot());
        // SpriteRenderer�i�[
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // CapsuleCollider�i�[
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// �e�̍X�V����
    /// </summary>
    public void Update()
    {
        // �v���C���[�ړ����\�b�h�Ăэ���
        PlayerMove();
        // �v���C���[�̈ړ����䃁�\�b�h�Ăэ���
        MoveLimit();
    }

    /// <summary>
    /// �e�̔���
    /// </summary>
    /// <returns></returns>
    IEnumerator Shot()
    {
        // ���˃��[�v
        while (true)
        {
            // �I�u�W�F�N�g�v�[����Launch�֐��Ăэ��� ------------
            // �����̒e
            _bulletPool.Launch(transform.position);
            // �����̒e�ł͂Ȃ����T�C�h�̒e
            _bulletPool1.Launch(transform.position);
            // ---------------------------------------------------
            // ���˂̃C���^�[�o��
            yield return new WaitForSeconds(_interval);
        }
    }

    /// <summary>
    /// �v���C���[�̈ړ�����
    /// </summary>
    public void MoveLimit()
    {
        // �E����
        if (transform.localPosition.x > _rigthSideMax)
        {
            // �͈͓��̈ړ�
            transform.localPosition = new Vector2(_rigthSideMax, transform.localPosition.y);
        }
        // ������
        if (transform.localPosition.x < _leftSideMax)
        {
            // �͈͓��̈ړ�
            transform.localPosition = new Vector2(_leftSideMax, transform.localPosition.y);
        }
        // �㐧��
        if (transform.localPosition.y > _topMax)
        {
            // �͈͓��̈ړ�
            transform.localPosition = new Vector2(transform.localPosition.x, _topMax);
        }
        // ������
        if (transform.localPosition.y < _bottomMax)
        {
            // �͈͓��̈ړ�
            transform.localPosition = new Vector2(transform.localPosition.x, _bottomMax);
        }
    }
    /// <summary>
    /// Player�̈ړ�����
    /// </summary>
    public void PlayerMove()
    {
        // ������
        float ver = Input.GetAxis("Vertical");
        // �c����
        float hor = Input.GetAxis("Horizontal");
        // ���x
        transform.position += new Vector3(hor * _horSpeed, ver * _verSpeed, 0);
    }
    /// <summary>
    /// �G�̒e�������������̏���
    /// </summary>
    /// <param name="collider">�R���C�_�[(�����蔻��)</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // �G�̒e������������
        if (collider.gameObject.tag == "EnemyBullet")
        {
            // �v���C���[�̃_���[�W�������s��
            NomalHpDown();
            // �_�ŃR���[�`�����J�n
            StartCoroutine(Hit());
        }
        // �G�̒e������������(�n�[�h�̒e)
        else if (collider.gameObject.tag == "HeardBullet")
        {
            // �v���C���[�̃_���[�W�������s��(�n�[�h)
            HeardHpDown();
            // �_�ŃR���[�`�����J�n
            StartCoroutine(Hit());
        }
    }
    /// <summary>
    /// �G�̒e�ɓ����������̃v���C���[��HP����
    /// </summary>
    private void NomalHpDown()
    {
        // �v���C���[��HP���炷
        _playerHp = _playerHp - _nomalDamage;
        // �G��HP�Q�[�W�����炷
        _slider.value = _playerHp - _nomalDamage;
        // �v���C���[��HP��0�ɂȂ�����
        if (_playerHp <= 0)
        {
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // �v���C���[��\��
            gameObject.SetActive(false);
            // ��莞�Ԍ�ɃV�[���ړ�
            Invoke("SceneMove", _sceneInterval);
        }
    }
    /// <summary>
    /// �G�̒e�ɓ����������̃v���C���[��HP����(�n�[�h)
    /// </summary>
    private void HeardHpDown()
    {
        // �v���C���[��HP���炷
        _playerHp = _playerHp - _heardDamage;
        // �G��HP�Q�[�W�����炷
        _slider.value = _playerHp - _heardDamage;
        // �v���C���[��HP��0�ɂȂ�����
        if (_playerHp <= 0)
        {
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // �v���C���[��\��
            gameObject.SetActive(false);
            // ��莞�Ԍ�ɃV�[���ړ�
            Invoke("SceneMove", _sceneInterval);
        }
    }
    /// <summary>
    /// �_�ł����鏈��
    /// </summary>
    /// <returns></returns>
    IEnumerator Hit()
    {
        // �����蔻��I�t�ɂ���
        _collider2D.enabled = false;
        // �_�Ń��[�v�J�n
        for (int i = 0; i < _loopCount; i++)
        {
            // �_�ł̃C���^�[�o��
            yield return new WaitForSeconds(_flashInterval);
            // spriteRenderer���I�t
            _spriteRenderer.enabled = false;
            // �_�ł̃C���^�[�o��
            yield return new WaitForSeconds(_flashInterval);
            // spriteRenderer���I��
            _spriteRenderer.enabled = true;
        }
        // �����蔻����I���ɂ���
        _collider2D.enabled = true;
    }
    /// <summary>
    /// �V�[���J��
    /// </summary>
    private void SceneMove()
    {
        // �Q�[���I�[�o�[�V�[���Ɉړ�
        SceneManager.LoadScene("GameOverScene");
    }
}
