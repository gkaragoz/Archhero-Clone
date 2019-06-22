using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationTrigger : MonoBehaviour {

    public UnityEvent onTriggered;

    public void ReleaseArrow() {
        onTriggered?.Invoke();
    }
    
}
