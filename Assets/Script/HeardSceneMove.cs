using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �n�[�h���[�h�ɃV�[���J��
/// </summary>
public class HeardSceneMove : MonoBehaviour
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
        SceneManager.LoadScene("HeardScene");
    }
}