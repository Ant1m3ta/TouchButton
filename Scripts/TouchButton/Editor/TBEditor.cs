﻿using UnityEngine;
using UnityEditor;

namespace UI.TouchButton {
    [CustomEditor(typeof(TouchButton))]
    public class TBEditor : Editor {
        [MenuItem("GameObject/UI/Touch Button", false, 0)]
        public static void CreateButtonObject (MenuCommand command) {
            GameObject clickTarget = command.context as GameObject;

            GameObject touchButton = new GameObject("Button - Touch");
            touchButton.AddComponent<TouchButton> ();
            touchButton.transform.SetParent(clickTarget.transform);
            touchButton.transform.localPosition = Vector3.zero;

            Selection.activeGameObject = touchButton;
        }

        public override void OnInspectorGUI()
        {
            TouchButton button = (TouchButton)target;

            CheckAudioProperties(button);
            CheckAnimatorProperties(button);

            DrawDefaultInspector();
        }

        void CheckAudioProperties (TouchButton button) {
            if (button.GetComponent<AudioSource> () == null)
                if (button.pressSFX != null || button.releaseSFX != null)
                    button.gameObject.AddComponent<AudioSource>().playOnAwake = false;
        }

        void CheckAnimatorProperties (TouchButton button) {
            Animator targetAnimator = button.GetComponent<Animator>();

            if (targetAnimator == null || targetAnimator.runtimeAnimatorController == null)
                if (GUILayout.Button("Generate Animator"))
                    TBUtils.GenerateAnimator(button);
        }
    }
}