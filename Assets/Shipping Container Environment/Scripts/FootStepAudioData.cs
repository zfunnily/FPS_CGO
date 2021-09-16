using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FPS/FootStep Audio Data")]
public class FootStepAudioData : ScriptableObject {
    public List<FootSetpAudio> footSetpAudio = new List<FootSetpAudio>();
}


[System.Serializable]
public class FootSetpAudio {

    public string tag;
    public List<AudioClip> audioClips = new List<AudioClip>();
    public float delay;
}