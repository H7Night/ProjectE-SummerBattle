using UnityEngine;
using UnityEngine.Playables;
[System.Serializable]
public class DialogueBehavior : PlayableBehaviour
{
    private PlayableDirector _playableDirector;
    public string charcterName;
    [TextArea(8, 1)] public string dialogueLine;
    public int dialogueSize;
    private bool isClipPlayed;
    public bool requiredPause;//这个对话是否需要按下按键才可以继续播放
    private bool pauseScheduled;

    public override void OnPlayableCreate(Playable playable)
    {
        _playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
    }
    //类似update，每一帧都会调用
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!isClipPlayed && info.weight > 0)
        {
            UIManager.Instance.SetupDialogue(charcterName,dialogueLine,dialogueSize);
            if (requiredPause)
                pauseScheduled = true;
            isClipPlayed = true;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        isClipPlayed = false;
        Debug.Log("Stooop");
        if (pauseScheduled)
        {
            pauseScheduled = false;
            GameManager.Instance.PauseTimeLine(_playableDirector);
        }
        else
        {
            UIManager.Instance.ToggleDialogueBox(false);
        }
    }
}
