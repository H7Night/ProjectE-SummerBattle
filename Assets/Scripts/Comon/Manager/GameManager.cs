using UnityEngine;
using UnityEngine.Playables;


public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    /**
     * 游戏状态
     */
    public GameMode gameMode;

    public enum GameMode {
        GamePlay,
        GameWin,
        GameLose,
        GameProtect,
        DialogueMoment
    }

    /**
     * 对话时的动画
     */
    private PlayableDirector currentDirector;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            if (Instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        //状态为游戏中
        gameMode = GameMode.GamePlay;
        //帧率
        Application.targetFrameRate = 30;
    }

    private void Update() {
        //失败
        if (gameMode == GameMode.GameLose) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

        //空格键和鼠标左键控制视频时的对话
        if (gameMode == GameMode.DialogueMoment) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                //按下空格继续下一段播放
                ResumeTimeLine();
            }
        }
    }

    //播放停止
    public void PauseTimeLine(PlayableDirector playableDirector) {
        currentDirector = playableDirector;
        gameMode = GameMode.DialogueMoment;
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
        UIManager.Instance.ToggleDialogueBox(true);
    }

    public void ResumeTimeLine() {
        gameMode = GameMode.GamePlay;
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        //UIManager.Instance.ToggleDialogueBar(false);
        UIManager.Instance.ToggleDialogueBox(true);
    }


    //失败
    public void GameLose() {
    }
}