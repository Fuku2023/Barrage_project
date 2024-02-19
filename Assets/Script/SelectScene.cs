using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// セレクトシーンにシーン遷移
/// </summary>
public class SelectScene : MonoBehaviour
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
        // ゲームシーンに移動
        SceneManager.LoadScene("SelectScene");
    }
}
