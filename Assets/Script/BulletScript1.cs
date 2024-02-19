using UnityEngine;

/// <summary>
/// �v���C���[�̒e�N���X
/// </summary>
public class BulletScript1 : MonoBehaviour
{
    // BulletPool�N���X�Q��
    private BulletPool1 _objectPool1;
    // �e�̑���
    [SerializeField]
    private float _speed = default;
    // �e�̍U����
    [SerializeField]
    public int _bulletPower = default;
    /// <summary>
    /// ����������
    /// </summary>
    public void Start()
    {
        // �I�u�W�F�N�g�v�[�����擾
        _objectPool1 = transform.parent.GetComponent<BulletPool1>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �e�̈ړ�����
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
        // ����������Ăяo��
        HideFromStage();
    }
    /// <summary>
    /// �n���ꂽ���W�ɒe���ړ����鏈��
    /// </summary>
    /// <param name="pos">�ꏊ</param>
    public void ShowInStage(Vector3 pos)
    {
        // position��n���ꂽ���W�ɐݒ�
        transform.position += pos;
    }
    /// <summary>
    /// ���g�̒e�̉������
    /// </summary>
    public void HideFromStage()
    {
        // �I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����g�����
        _objectPool1.Collect(this);
    }
}
