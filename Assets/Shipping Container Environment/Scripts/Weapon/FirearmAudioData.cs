using UnityEngine;
namespace Scripts.weapon
{
    [CreateAssetMenu(menuName = "FPS/Firearm Audio Data")]
    public class FirearmAudioData : ScriptableObject
    {
        public AudioClip ShootingAudio;
        public AudioClip ReloadLeft;
        public AudioClip ReloadOutOf;
    }
}