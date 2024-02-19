using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �G�l�~�[�N���X
/// </summary>
public class EnemyNomal : MonoBehaviour
{
    //BulletScript�N���X�Q��
    private BulletScript _bulletScript;
    //BulletScript�N���X�Q��
    private BulletScript1 _bulletScript1;
    // BulletPool2�N���X�Q��
    [SerializeField]
    private EnemyBulletPool _bulletPool2;
    // �̗̓Q�[�W
    [SerializeField]
    private Slider _slider;
    // 2��ޖڂ̒e�̃N���X�Q��
    [SerializeField]
    public EnemyBullet _bullet2;
    // ���ʉ��Q��
    [SerializeField]
    private AudioClip _audio;
    // �G�̗̑�
    private int _enemyHp = 6000;
    // �G�̔����̗̑�
    private int _heafHp = 3000;
    // �e��ł��x
    private float _interval = 0.22f;
    // �e�̑ł��x��������ϐ�
    private float _heardInterval = 0.125f;
    // �V�[���ړ�����܂ł̎���
    private float _sceneInterval = 2f;
    /// <summary>
    /// ������
    /// </summary>
    public void Awake()
    {
        // ���˗p�R���[�`���X�^�[�g
        StartCoroutine(Shot());
        // BulletScript�i�[
        _bulletScript = FindObjectOfType<BulletScript>();
        // BulletScript1�i�[
        _bulletScript1 = FindObjectOfType<BulletScript1>();
    }
    /// <summary>
    /// �v���C���[�̒e���N�������Ƃ��̏���
    /// </summary>
    /// <param name="collider">�R���C�_�[(�����蔻��)</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ���v���C���[�̒e�ɓ���������
        if (collider.gameObject.tag == "BlueBullet")
        {
            // �G�̗̑͏����Ăэ���
            EnemyUpdateHp();
        }
        // �Ԃ��v���C���[�̒e�ɓ���������
        else if (collider.gameObject.tag == "RedBullet")
        {
            // �G�̗̑͏����Ăэ���
            EnemyStoringUpdateHp();
        }
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
            // �I�u�W�F�N�g�v�[����Launch�֐��Ăяo��(�~��`���悤�ɔ��˂���e����)
            _bulletPool2.Launch(transform.position);
            // �̗͂������ɂȂ�܂ł̓f�t�H���g�C���^�[�o��
            if (_enemyHp > _heafHp) 
            {
                // ���˂���C���^�[�o��
                yield return new WaitForSeconds(_interval);
            }
            // �̗͂������ɂȂ�����C���^�[�o����Z������
            if (_enemyHp <= _heafHp) 
            {
                // �Z�����˂̃C���^�[�o��
                yield return new WaitForSeconds(_heardInterval);
            }
        }
    }
    /// <summary>
    /// �G�̗̑͏���
    /// </summary>
    private void EnemyUpdateHp()
    {
        // �̗͂�0�ɂȂ�����
        if (_enemyHp <= 0)
        {
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // �G���\��
            gameObject.SetActive(false);
            // ��莞�Ԍ�ɃV�[���ړ�
            Invoke("SceneMove", _sceneInterval);
        }
        // �̗͂�0���傫��������
        else if (_enemyHp > 0)
        {
            // �G��HP���炷
            _enemyHp = _enemyHp - _bulletScript._bulletPower;
            // �G��HP�Q�[�W�����炷
            _slider.value = _enemyHp - _bulletScript._bulletPower;
            //Debug.Log("�c�� Hp : " + _enemyHp);
        }
    }
    /// <summary>
    /// �G�̗̑͏���
    /// </summary>
    private void EnemyStoringUpdateHp()
    {
        // �̗͂�0�ɂȂ�����
        if (_enemyHp <= 0)
        {
            // ���ʉ��Đ�
            AudioSource.PlayClipAtPoint(_audio, transform.position);
            // �G���\��
            gameObject.SetActive(false);
            // ��莞�Ԍ�ɃV�[���ړ�
            Invoke("SceneMove", _sceneInterval);
        }
        // �̗͂�0���傫��������
        else if (_enemyHp > 0)
        {
            // �G��HP���炷
            _enemyHp = _enemyHp - _bulletScript1._bulletPower;
            // �G��HP�Q�[�W�����炷
            _slider.value = _enemyHp - _bulletScript1._bulletPower;
        }
    }
    /// <summary>
    /// �V�[���J��
    /// </summary>
    private void SceneMove()
    {
        // �Q�[���N���A�V�[���Ɉړ�
        SceneManager.LoadScene("GameClearScene");
    }
}
