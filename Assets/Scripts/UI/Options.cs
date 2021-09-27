using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.SimpleLUT
{
    public class Options : MonoBehaviour
    {
        private SimpleLUT simple;
        private new AudioSource audio;

        [SerializeField] private Slider brightness;
        [SerializeField] private Slider music;

        private void Start()
        {
            simple = Camera.main.GetComponentInChildren<SimpleLUT>();
            audio = Player.instance.GetComponentInChildren<AudioSource>();
        }

        private void Update()
        {
            simple.Brightness = brightness.value;
            audio.volume = music.value;
        }
    }

}

