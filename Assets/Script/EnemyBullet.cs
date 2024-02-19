using UnityEngine;

/// <summary>
/// �G�l�~�[�̒e�N���X
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    // BulletPool�N���X�Q��
    private EnemyBulletPool _objectPool2;
    // �e�̍U����
    [SerializeField]
    public int _bulletPower = default;
    // �e�̉��ړ�
    private float _moveWidth = default;
    // �e�̏c�ړ�
    private float _moveHeight = default;

    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        // �I�u�W�F�N�g�v�[�����擾
        _objectPool2 = transform.parent.GetComponent<EnemyBulletPool>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �e�̍X�V����
    /// </summary>
    private void Update()
    {
        // �e�̈ړ�
        transform.position += new Vector3(_moveWidth, _moveHeight, 0) * Time.deltaTime;
    }
    /// <summary>
    /// �e�̊p�x�Ƒ����v�Z
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="speed">����</param>
    public void Setting(float angle, float speed)
    {
        // �G�̉E����0�x�Ƃ��Ĕ����v���Ɋp�x�𑝂₷
        _moveWidth = Mathf.Cos(angle) * speed;
        _moveHeight = Mathf.Sin(angle) * speed;

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
        transform.position = pos;
    }
    /// <summary>
    /// ���g�̒e�̉������
    /// </summary>
    public void HideFromStage()
    {
        // �I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����g�����
        _objectPool2.Collect(this);
    }

}

