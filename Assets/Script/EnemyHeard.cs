/*---------------------------------------------------------------
 * �쐬��:2��1��
 * 
 * �쐬��:���� ����
 *---------------------------------------------------------------*/

using System.Collections;
using UnityEngine;

public class EnemyHeard : MonoBehaviour
{
    // �e�̃N���X�Q��
    [SerializeField]
    public EnemyHeardBullet _bullet;
    // BulletPool�N���X�Q��
    [SerializeField]
    private EnemyHeardPool _bulletPool;
    // �v���C���[�I�u�W�F�N�g�Q��
    [SerializeField]
    GameObject _playerObj = null;
    // �e��ł��x
    private float _interval = 1.1f;
    /// <summary>
    /// ����������
    /// </summary>
    public void Start()
    {
        // �v���C���[�I�u�W�F�N�g���擾����:
        _playerObj = GameObject.Find("Player");
        // ���˗p�R���[�`���X�^�[�g
        StartCoroutine(Shot());
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
            // �v���C���[�̈ʒu�ɔ��˂���I�u�W�F�N�g�v�[����Launch�֐��Ăэ���
            _bulletPool.Launch(_playerObj.transform.position - transform.position);
            // ���˂̃C���^�[�o��
            yield return new WaitForSeconds(_interval);
        }
    }
}
