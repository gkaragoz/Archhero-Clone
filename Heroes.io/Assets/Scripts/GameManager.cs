using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [Header("Initialization")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterController[] _characters = null;
    public CharacterController[] Characters { get { return _characters; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("OverlayHealthBar");

        InitializeOverlayHealthBars();
    }

    public void InitializeOverlayHealthBars() {
        GameObject[] overlayHealthBarObjs = ObjectPooler.instance.GetGameObjectsOnPool("OverlayHealthBar");

        for (int ii = 0; ii < overlayHealthBarObjs.Length; ii++) {
            if (ii >= _characters.Length) {
                break;
            }

            CharacterController _characterController = _characters[ii];
            overlayHealthBarObjs[ii].GetComponent<OverlayHealthBar>().Initialize(_characterController);
        }

        Debug.Log("[POOL OVERLAY HEALTH BARS]" + " have been initialized.");
    }

}
