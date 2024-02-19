using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �v���C���[�̒e�̃v�[��
/// </summary>
public class BulletPool1 : MonoBehaviour
{
    // �e��prefab
    [SerializeField]
    private BulletScript1 _bullet;
    // �������鐔
    [SerializeField]
    private int _maxCount = default;
    // ���񐶐����̃|�W�V����(�e1)
    [SerializeField]
    private Transform _setPos;
    // ���񐶐����̃|�W�V����(�e2)
    [SerializeField]
    private Transform _setPos1;
    // ���������e���i�[����bulletQueue
    Queue<BulletScript1> _bulletQueue;
    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        // bulletQueue�̏�����
        _bulletQueue = new Queue<BulletScript1>();

        // �e�𐶐����郋�[�v
        for (int i = 0; i < _maxCount; i++)
        {
            // �e�̐��� --------------------------------
            // �����̒e
            BulletScript1 tmpBullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            // ���T�C�h�̒e
            BulletScript1 tmpBullet1 = Instantiate(_bullet, _setPos1.position, Quaternion.identity, transform);
            // -----------------------------------------
            // bulletQueue�ɒǉ� -----------------------
            // �����̒e
            _bulletQueue.Enqueue(tmpBullet);
            // ���T�C�h�̒e
            _bulletQueue.Enqueue(tmpBullet1);
            // -----------------------------------------
        }
    }
    /// <summary>
    /// �e��݂��o������
    /// </summary>
    /// <param name="pos">�ꏊ</param>
    /// <returns></returns>
    public BulletScript1 Launch(Vector3 pos)
    {
        // bulletQueue����Ȃ�null
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueue����e������o��
        BulletScript1 tmpBullet = _bulletQueue.Dequeue();
        // bulletQueue����e����������o��
        BulletScript1 tmpBullet1 = _bulletQueue.Dequeue();
        // �e��\������
        tmpBullet.gameObject.SetActive(true);
        // �e��\������
        tmpBullet1.gameObject.SetActive(true);
        // �n���ꂽ���W�ɒe���ړ�����
        tmpBullet.ShowInStage(pos);
        // �n���ꂽ���W�ɒe���ړ�����
        tmpBullet1.ShowInStage(pos);
        // �Ăяo�����ɓn��
        return tmpBullet;
    }
    /// <summary>
    /// �e�̉������
    /// </summary>
    /// <param name="bullet">�e</param>
    public void Collect(BulletScript1 bullet)
    {
        //�e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);
        //bulletQueue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }
}