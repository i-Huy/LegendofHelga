using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableStoppedSignal : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayableDirector director;
    public PlayIntroCutscene intro;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnEnable()
    {
        director.stopped += SendStopSignal;
    }

    void SendStopSignal(PlayableDirector d)
    {
        intro.SetIsPlaying(true);
    }

    void OnDisable()
    {
        director.stopped -= SendStopSignal;
    }
}
