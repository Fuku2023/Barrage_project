using UnityEngine;

/// <summary>
/// �v���C���[��HP�Q�[�W�̈ʒu���X�V���鏈��
/// </summary>
public class UIOverlay : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g�Q��
    [SerializeField]
    private Transform _targetTransform;
    // UI�̃R���|�[�l���g
    private RectTransform _rectTransform;
    // UI�̈ʒu
    private Vector3 _pos = new Vector3(0, 11, 0);
    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        // RecrTransform�i�[
        _rectTransform = GetComponent<RectTransform>();
    }
    /// <summary>
    /// UI�̍X�V����
    /// </summary>
    private void Update()
    {
        // �v���C���[�̃I�u�W�F�N�g�ɍ��킹��UI�ړ�
        _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetTransform.position + _pos);
    }
}
