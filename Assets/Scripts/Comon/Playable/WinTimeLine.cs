using UnityEngine;
using UnityEngine.Playables;

public class WinTimeLine : MonoBehaviour {
    /**
     * 胜利后的timeline
     */
    public PlayableDirector timeLine;

    private void Start() {
        timeLine = GameObject.Find("TimeLine").GetComponent<PlayableDirector>();
    }

    private void Update() {
        if (GameManager.Instance.gameMode == GameManager.GameMode.GameWin) {
            GameWinPlay();
        }
    }

    //播放
    public void GameWinPlay() {
        timeLine.Play();
    }
}