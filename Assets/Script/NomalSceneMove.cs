using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �m�[�}�����[�h�ɃV�[���J��
/// </summary>
public class NomalSceneMove : MonoBehaviour
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
        SceneManager.LoadScene("NomalScene");
    }
}
