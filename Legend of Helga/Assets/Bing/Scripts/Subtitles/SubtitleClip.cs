using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
    public string subtitleText;

    public override Playable CreatePlayable(
        PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);

        SubtitleBehaviour sBehaviour = playable.GetBehaviour();
        sBehaviour.subtitleText = subtitleText;

        return playable;
    }
}
