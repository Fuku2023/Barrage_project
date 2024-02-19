using UnityEngine;

public class EnemyHeardBullet : MonoBehaviour
{
    // �e�̑���
    [SerializeField] 
    float _moveSpeed = default;
    // �e�̕���
    [SerializeField] 
    Vector3 _moveVec = new Vector3(0, 0, 0);
    // BulletPool�N���X�Q��
    private EnemyHeardPool _objectPool;
    /// <summary>
    /// ����������
    /// </summary>
    public void Start()
    {
        // �I�u�W�F�N�g�v�[�����擾
        _objectPool = transform.parent.GetComponent<EnemyHeardPool>();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// �e�̍X�V����
    /// </summary>
    public void Update()
    {
        // �e�̑���
        float addMove = _moveSpeed * Time.deltaTime;
        // �e�̑����Ɗp�x�̊��Z
        transform.Translate(_moveVec * addMove);
    }
    /// <summary>
    /// �e�̔��˂���p�x
    /// </summary>
    /// <param name="vec">�p�x</param>
    public void SetMoveVec(Vector3 vec)
    {
        // ���˂���p�x
        _moveVec = vec.normalized;
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
    /// ���g�̒e�̉������
    /// </summary>
    public void HideFromStage()
    {
        // �I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����g�����
        _objectPool.Collect(this);
    }
}
