using UnityEngine;

/// <summary>
/// ESC�������ꂽ��Q�[���I������
/// </summary>
public class EndGame : MonoBehaviour
{
    // ���ʉ��Đ�
    [SerializeField]
    private AudioClip _audio;
    /// <summary>
    /// �{�^���������ꂽ��
    /// </summary>
    public void OnClickStartButton()
    {
        // ���ʉ��Đ�
        AudioSource.PlayClipAtPoint(_audio, transform.position);
        // �Q�[���I��
        Application.Quit();
    }
}
