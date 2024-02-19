using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルにシーン遷移
/// </summary>
public class ReStryScript : MonoBehaviour
{
    // 効果音再生
    [SerializeField]
    private AudioClip _audio;
    /// <summary>
    /// ボタンが押されたら
    /// </summary>
    public void OnClickStartButton()
    {
        // 効果音再生
        AudioSource.PlayClipAtPoint(_audio, transform.position);
        // タイトルシーンに移動
        SceneManager.LoadScene("TitleScene");
    }
}
