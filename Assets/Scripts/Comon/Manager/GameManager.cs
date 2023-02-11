using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;


    public enum GameMode {
        GamePlay,
        GameWin,
        GameProtect,
        DialogueMoment
    }

    public GameMode gameMode;
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
        gameMode = GameMode.GamePlay;
        Application.targetFrameRate = 30; //帧率
    }

    private void Update() {
        if (gameMode == GameMode.DialogueMoment) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                //按下空格继续下一段播放
                ResumeTimeLine();
            }
        }
    }

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

    //通关
    public void GameWin() {
    }

    //失败
    public void GameLose() {
    }
}