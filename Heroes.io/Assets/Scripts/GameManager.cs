using System.Collections.Generic;
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
    private GameObject _playerPrfab = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private List<PlayerController> _players = new List<PlayerController>();
    public List<PlayerController> Players { get { return _players; } }

    private void Start() {
        PlayerController newPlayer = Instantiate(_playerPrfab, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
        _players.Add(newPlayer);

        ObjectPooler.instance.InitializePool("OverlayHealthBar");

        InitializeOverlayHealthBars();
    }

    public void InitializeOverlayHealthBars() {
        GameObject[] overlayHealthBarObjs = ObjectPooler.instance.GetGameObjectsOnPool("OverlayHealthBar");

        for (int ii = 0; ii < overlayHealthBarObjs.Length; ii++) {
            if (ii >= _players.Count) {
                break;
            }

            PlayerController _playerController = _players[ii];
            overlayHealthBarObjs[ii].GetComponent<OverlayHealthBar>().Initialize(_playerController.CharacterController);
        }

        Debug.Log("[POOL OVERLAY HEALTH BARS]" + " have been initialized.");
    }

}
