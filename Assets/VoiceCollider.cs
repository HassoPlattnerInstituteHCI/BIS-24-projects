using System;
using UnityEngine;
using SpeechIO;

namespace DualPantoToolkit
{
    public class VoiceCollider : MonoBehaviour
    {
        public String text;
        
        protected SpeechOut speechOut
        { get { if (_speechOut == null) _speechOut = new SpeechOut(); return _speechOut; } }
        private SpeechOut _speechOut;

        protected AudioSource audioSrc
        { get { if (!_audioSrc) _audioSrc = gameObject.AddComponent<AudioSource>(); return _audioSrc; } }
        private AudioSource _audioSrc;


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(text);
            speechOut.Speak(text);
        }
    }
}