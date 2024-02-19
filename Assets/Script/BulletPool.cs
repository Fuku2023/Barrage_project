using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �v���C���[�̒e�̃v�[��
/// </summary>
public class BulletPool : MonoBehaviour
{
    // �e��prefab
    [SerializeField] 
    private BulletScript _bullet;
    // �������鐔
    [SerializeField]
    private int _maxCount = default;
    // ���������e���i�[����Queue
    Queue<BulletScript> _bulletQueue;
    // ���񐶐����̃|�W�V����
    [SerializeField]
    private Transform _setPos;
    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        // bulletQueue�̏�����
        _bulletQueue = new Queue<BulletScript>();
        // �e�𐶐����郋�[�v
        for (int i = 0; i < _maxCount; i++)
        {
            // �e�̐���
            BulletScript tmpBullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            // bulletQueue�ɒǉ�
            _bulletQueue.Enqueue(tmpBullet);
        }
    }
    /// <summary>
    /// �e��݂��o������
    /// </summary>
    /// <param name="pos">�ꏊ</param>
    /// <returns></returns>
    public BulletScript Launch(Vector3 pos)
    {
        //Queue����Ȃ�null
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueue����e������o��
        BulletScript tmpBullet = _bulletQueue.Dequeue();
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
    public void Collect(BulletScript bullet)
    {
        // �e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);
        // bulletQueue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }
}
