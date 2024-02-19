using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Z���N�g�V�[���ɃV�[���J��
/// </summary>
public class SelectScene : MonoBehaviour
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
        // �Q�[���V�[���Ɉړ�
        SceneManager.LoadScene("SelectScene");
    }
}
