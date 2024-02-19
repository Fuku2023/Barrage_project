using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �G�l�~�[�̒e�̃v�[��
/// </summary>
public class EnemyBulletPool : MonoBehaviour
{
    // �e��prefab
    [SerializeField]
    private EnemyBullet _bullet;
    // ���������e���i�[����bulletQueue
    Queue<EnemyBullet> _bulletQueue;
    // ���񐶐����̃|�W�V����
    Vector3 _setPos = new Vector3(100, 100, 0);
    // ���˂̃C���^�[�o��
    private float _interval = 0.5f;
    // �p�x�̌v�Z�Ɏg���l
    private int _calculation = 2;
    // ���x����
    private float _halfAngle = 2f;
    // ����U���p�^�[�����s����
    private int _atakkCount = 3;
    // �U����(�E�F�[�u��)
    private int _waveAtakk = 20;
    // �U����(�����_����)
    private int _randomAtakk = 10;
    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        // bulletQueue�̏�����
        _bulletQueue = new Queue<EnemyBullet>();
        // ���˗p�R���[�`���X�^�[�g
        StartCoroutine(CPU());
    }
    /// <summary>
    /// �e�̏���
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="speed">����</param>
    private void Shot(float angle, float speed)
    {
        // �e�̐���
        EnemyBullet bullet = Instantiate(_bullet, _setPos, Quaternion.identity, transform);
        // �e�̊p�x,����
        bullet.Setting(angle, speed);
        // bulletQueue�ɒǉ�
        _bulletQueue.Enqueue(bullet);
    }
    /// <summary>
    /// �e�̔���
    /// </summary>
    /// <returns></returns>
    IEnumerator CPU()
    {
        // while(�J�b�R�̒���true�̊ԌJ��Ԃ�����������)
        while (true)
        {
            // �E�F�[�u��̒e
            yield return WaveShot(_atakkCount, _waveAtakk);
            // ��莞�Ԃ��Ƃɒe�𔭎�
            yield return new WaitForSeconds(_interval);
            // �����_����̒e
            yield return RandomShot(_atakkCount, _randomAtakk);
            // ��莞�Ԃ��Ƃɒe�𔭎�
            yield return new WaitForSeconds(_interval);
        }
    }
    /// <summary>
    /// �ŏ��̒e�̔���(�E�F�[�u��)
    /// </summary>
    /// <param name="wave">����p�^�[�������邩</param>
    /// <param name="atakk">�U����</param>
    /// <returns></returns>
    IEnumerator WaveShot(int wave, int atakk)
    {
        // 4��8�����Ɍ�������
        for (int i = 0; i < wave; i++)
        {
            // ��莞�Ԃ��Ƃɒe�𔭎�
            yield return new WaitForSeconds(_interval);
            // �E�F�[�u��̒e�̔��ˏ����Ăэ���
            ShotWaveProsess(atakk, _calculation);
        }
    }
    /// <summary>
    /// �r������̒e�̔���(�����_����)
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="atakk"></param>
    /// <returns></returns>
    IEnumerator RandomShot(int wave, int atakk)
    {
        // 4��8�����Ɍ�������
        for (int i = 0; i < wave; i++)
        {
            // ��莞�Ԃ��Ƃɒe�𔭎�
            yield return new WaitForSeconds(_interval);
            // �����_����̒e�̔��ˏ����Ăэ���
            yield return RandomShotProsess(atakk, _calculation);
        }
    }
    /// <summary>
    /// �e�̔���(�����_����)
    /// </summary>
    /// <param name="count">�e�̃J�E���g</param>
    /// <param name="speed">�e�̑���</param>
    /// <returns></returns>
    IEnumerator RandomShotProsess(int count, float speed)
    {
        // �e�J�E���g
        int bulletCount = count;
        // �e�̃J�E���g�ȓ��Ȃ�e����
        for (int i = 0; i < bulletCount; i++)
        {
            // 360�x�ɒe�𔭎�
            float angle = i * (_calculation * Mathf.PI / bulletCount);
            // �v�Z�����ʒu�ɒe�𐶐�
            Shot(angle - Mathf.PI / _halfAngle, speed);
            // �v�Z�����ʒu�ɒe�𐶐�
            Shot(-angle - Mathf.PI / _halfAngle, speed);
            // ��莞�Ԃ��Ƃɒe�𔭎�
            yield return new WaitForSeconds(_interval);
        }
    }
    /// <summary>
    /// �e�̔���(�E�F�[�u��)
    /// </summary>
    /// <param name="count">�e�̃J�E���g</param>
    /// <param name="speed">�e�̑���</param>
    public void ShotWaveProsess(int count, float speed)
    {
        // �e�J�E���g
        int bulletCount = count;
        // �e�̃J�E���g�ȓ��Ȃ�e��
        for (int i = 0; i < bulletCount; i++)
        {
            // 360�x�ɒe�𔭎�
            float angle = i * (_calculation * Mathf.PI / bulletCount);
            // �v�Z�����ʒu�ɒe�𔭎�
            Shot(angle, speed);
        }
    }
    /// <summary>
    /// �e��݂��o������
    /// </summary>
    /// <param name="pos">�ꏊ</param>
    /// <returns></returns>
    public EnemyBullet Launch(Vector3 pos)
    {
        // bulletQueue����Ȃ�null
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueue����e������o��
        EnemyBullet tmpBullet = _bulletQueue.Dequeue();
        // �e��\������
        tmpBullet.gameObject.SetActive(true);
        // �n���ꂽ���W�ɒe���ړ�����
        tmpBullet.ShowInStage(pos);
        // �Ăяo�����ɓn��
        return tmpBullet;
    }
    /// <summary>
    /// �e�̉������
    /// </summary>
    /// <param name="bullet">�e</param>
    public void Collect(EnemyBullet bullet)
    {
        // �e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);
        // bulletQueue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }
}
