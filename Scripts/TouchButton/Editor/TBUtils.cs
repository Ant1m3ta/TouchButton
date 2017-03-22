using UnityEditor.Animations;
using UnityEngine;
using UnityEditor;

namespace UI.TouchButton {
	public static class TBUtils {
		public static void GenerateAnimator(TouchButton touchButton)
		{
			const string defaultStateName = "Default";
			const string pressStateName = "Press";
			const string releaseStateName = "Release";

			string path = EditorUtility.SaveFilePanel(
						"New Animation Controller",
						"",
						touchButton.gameObject.name + ".controller",
						"controller");

			path = FileUtil.GetProjectRelativePath(path);

			AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(path);

			AssignNewStateToController(controller, defaultStateName, touchButton.defaultTrigger);
			AssignNewStateToController(controller, pressStateName, touchButton.pressTrigger);
			AssignNewStateToController(controller, releaseStateName, touchButton.releaseTrigger, "InvokeReleaseEvent");

			AssignControllerToButton(controller, touchButton);
		}

		static void AssignNewStateToController(AnimatorController controller, string stateName, string stateTrigger, string stateEvent = null) {
			AnimationClip stateClip = CreateClip(stateName, stateEvent);
			AnimatorState state = CreateState(controller, stateName, stateClip);

			CreateTransitionFromAnyState(controller, state, stateTrigger);
			BindClipToAnimatorController(controller, stateClip);
		}

		static AnimationClip CreateClip (string name, string stateEvent) {
			AnimationClip newClip = new AnimationClip();
			newClip.name = name;

			if (!string.IsNullOrEmpty(stateEvent))
				AssignEventToClip(newClip, stateEvent);

			return newClip;
		}

		static void AssignEventToClip (AnimationClip clip, string eventName) {
			AnimationEvent clipEvent = new AnimationEvent();
			clipEvent.functionName = eventName;

			AnimationUtility.SetAnimationEvents(clip, new AnimationEvent[1] { clipEvent });
		}

		static AnimatorState CreateState (AnimatorController controller, string stateName, AnimationClip stateMotion) {
			AnimatorStateMachine root = controller.layers[0].stateMachine;
			AnimatorState newState = root.AddState(stateName);
			newState.motion = stateMotion;

			return newState;
		}

		static void CreateTransitionFromAnyState (AnimatorController controller, AnimatorState state, string triggerName) {
			AnimatorStateMachine root = controller.layers[0].stateMachine;
			AnimatorStateTransition transition = root.AddAnyStateTransition(state);

			transition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.If, 0, triggerName);
			controller.AddParameter(triggerName, AnimatorControllerParameterType.Trigger);
		}

		static void BindClipToAnimatorController (AnimatorController controller, AnimationClip clip) {
			AssetDatabase.AddObjectToAsset(clip, controller);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(clip));
		}

		static void AssignControllerToButton (AnimatorController controller, TouchButton button) {
			Animator animator = button.GetComponent<Animator>();

			if (animator == null)
				animator = button.gameObject.AddComponent<Animator>();

			AnimatorController.SetAnimatorController(animator, controller);
		}
	}
}
