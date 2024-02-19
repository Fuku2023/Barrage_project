using UnityEngine;

/// <summary>
/// �v���C���[�̒e�N���X
/// </summary>
public class BulletScript : MonoBehaviour
{
    // BulletPool�N���X�Q��
    private BulletPool _objectPool;
    // �e�̑���
    [SerializeField]
    private float _speed = default;
    // �e�̍U����
    [SerializeField]
    public int _bulletPower = default;
    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        // �I�u�W�F�N�g�v�[�����擾
        _objectPool = transform.parent.GetComponent<BulletPool>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �e�̍X�V����
    /// </summary>
    private void Update()
    {
        // �e�̈ړ�
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    /// <summary>
    /// ����������Ăэ��ރ��\�b�h
    /// </summary>
    private void OnBecameInvisible()
    {
        // ����������Ăэ���
        HideFromStage();
    }
    /// <summary>
    /// �n���ꂽ���W�ɒe���ړ����鏈��
    /// </summary>
    /// <param name="pos">�ꏊ</param>
    public void ShowInStage(Vector3 pos)
    {
        // position��n���ꂽ���W�ɐݒ�
        transform.position = pos;
    }
    /// <summary>
    /// ���g�̒e�̉������
    /// </summary>
    public void HideFromStage()
    {
        // �I�u�W�F�N�g�v�[����Collect�֐����Ăэ���Ŏ��g�����
        _objectPool.Collect(this);
    }
}
