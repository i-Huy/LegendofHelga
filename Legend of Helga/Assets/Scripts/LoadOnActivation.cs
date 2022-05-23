using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LoadOnActivation : MonoBehaviour
{
    public PlayableDirector director;
    // Start is called before the first frame update
    public SceneChanger sceneChanger;

    private void Update()
    {
        if (director.state != PlayState.Playing)
        {
            sceneChanger.FadetoScene("Credits");
        }
    }
}
