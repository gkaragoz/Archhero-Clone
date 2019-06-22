using UnityEngine;

public class ApplicationTargetFrame : MonoBehaviour {

    [SerializeField]
    [Range(1, 60)]
    private int _applicationTargetFrame = 60;

    private void Update() {
        Application.targetFrameRate = _applicationTargetFrame;
    }

}
