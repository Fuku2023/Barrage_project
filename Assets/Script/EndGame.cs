using UnityEngine;

/// <summary>
/// ESCが押されたらゲーム終了処理
/// </summary>
public class EndGame : MonoBehaviour
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
        // ゲーム終了
        Application.Quit();
    }
}
