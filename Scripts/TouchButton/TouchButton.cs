using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.TouchButton {
    [RequireComponent(typeof(Image))]
    public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public readonly string defaultTrigger = "Default";
        public readonly string pressTrigger = "Press";
        public readonly string releaseTrigger = "Release";

        [SerializeField]
        public AudioClip _pressSFX;

        public AudioClip pressSFX {
            get {
                return _pressSFX;
            }
            set {
                _pressSFX = value;
            }
        }

        [SerializeField]        
        AudioClip _releaseSFX;

        public AudioClip releaseSFX {
            get {
                return _releaseSFX;
            }
            set {
                _releaseSFX = value;
            }
        }
        
        [SerializeField]        
        UnityEvent _releaseEvent;

        Animator _animator;
        AudioSource _audioSource;

        void Awake()
        {
            CacheComponents();
        }

        void CacheComponents () {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            PressButton ();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            ReleaseButton (eventData);
        }

        void PressButton () {
            TrySetTrigger(pressTrigger);

            if (_pressSFX != null)
            {
                _audioSource.clip = _pressSFX;
                _audioSource.Play();
            }
        }

        void ReleaseButton (PointerEventData eventData) {
            if (eventData.pointerPress != eventData.pointerCurrentRaycast.gameObject)
                TrySetTrigger(defaultTrigger);
            else
                TrySetTrigger(releaseTrigger);

            if (_releaseSFX != null)
            {
                _audioSource.clip = _releaseSFX;
                _audioSource.Play();
            }
        }

        public void InvokeReleaseEvent()
        {
            _releaseEvent.Invoke();
        }

        void TrySetTrigger (string trigger) {
            if (_animator != null)
                _animator.SetTrigger(trigger);
        }
    }
}