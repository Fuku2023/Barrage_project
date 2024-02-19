using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHeardPool : MonoBehaviour
{
    // �e��prefab
    [SerializeField]
    private EnemyHeardBullet _bullet;
    // �v���C���[�̃I�u�W�F�N�g�Q��
    [SerializeField]
    GameObject _playerObj = null;
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
    Queue<EnemyHeardBullet> _bulletQueue;
    /// <summary>
    /// ����������
    /// </summary>
    void Awake()
    {
        // bulletQueue�̏�����
        _bulletQueue = new Queue<EnemyHeardBullet>();
        // �v���C���[�I�u�W�F�N�g���擾����
        _playerObj = GameObject.Find("Player");
        // �e�𐶐����郋�[�v
        for (int i = 0; i < _maxCount; i++)
        {
            if (_playerObj == null) { break; }
            // �e�̐��� --------------------------------
            //�E�̌Œ�C��
            EnemyHeardBullet bullet = Instantiate(_bullet, _setPos.position, Quaternion.identity, transform);
            //���̌Œ�C��
            EnemyHeardBullet bullet1 = Instantiate(_bullet, _setPos1.position, Quaternion.identity, transform);
            // -----------------------------------------
            // �v���C���[�̈ʒu�Ɉړ� ------------------
            //�E�̌Œ�C��
            bullet.SetMoveVec(_playerObj.transform.position - transform.position);
            //���̌Œ�C��
            bullet1.SetMoveVec(_playerObj.transform.position - transform.position);
            // -----------------------------------------
            // bulletQueue�ɒǉ� -----------------------------
            //�E�̌Œ�C��
            _bulletQueue.Enqueue(bullet);
            //���̌Œ�C��
            _bulletQueue.Enqueue(bullet1);
            // -----------------------------------------
        }
    }
    /// <summary>
    /// �e��݂��o������
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public EnemyHeardBullet Launch(Vector3 vec)
    {
        // bulletQueue����Ȃ�null
        if (_bulletQueue.Count <= 0)
        {
            return null;
        }
        // bulletQueue����e������o�� 
        EnemyHeardBullet tmpBullet = _bulletQueue.Dequeue();
        // �e��\������
        tmpBullet.gameObject.SetActive(true);
        // �n���ꂽ���W�ɒe���ړ�����
        tmpBullet.SetMoveVec(vec);
        // �Ăяo�����ɓn��
        return tmpBullet;
    }
    /// <summary>
    /// �e�̉������
    /// </summary>
    /// <param name="bullet">�e</param>
    public void Collect(EnemyHeardBullet bullet)
    {
        // �e�̃Q�[���I�u�W�F�N�g���\��
        bullet.gameObject.SetActive(false);
        // bulletQueue�Ɋi�[
        _bulletQueue.Enqueue(bullet);
    }
}
