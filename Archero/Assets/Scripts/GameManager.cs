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
    private GameObject _playerPrefab = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private List<PlayerController> _players = new List<PlayerController>();
    public List<PlayerController> Players { get { return _players; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("OverlayHealthBar");
        ObjectPooler.instance.InitializePool("Arrow");

        Vector3 spawnPosition = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        _players.Add(Instantiate(_playerPrefab, spawnPosition, Quaternion.identity).GetComponent<PlayerController>());

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
