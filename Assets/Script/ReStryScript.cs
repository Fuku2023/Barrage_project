using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�g���ɃV�[���J��
/// </summary>
public class ReStryScript : MonoBehaviour
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
        // �^�C�g���V�[���Ɉړ�
        SceneManager.LoadScene("TitleScene");
    }
}
