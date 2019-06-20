using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks {

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

    public PlayerController GetPlayer(Player player) {
        var playerList = GameObject.FindObjectsOfType<PlayerNetwork>();
        for (int ii = 0; ii < playerList.Length; ii++) {
            if (player == playerList[ii].photonView.Owner) {
                return playerList[ii].GetComponent<PlayerController>();
            }
        }
        return null;
    }

    #region Pun Callbacks

    public override void OnJoinedRoom() {
        Debug.Log("OnJoinedRoom");
        PlayerController newPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
        _players.Add(newPlayer);

        InitializeOverlayHealthBars();
    }

    public override void OnLeftRoom() {
        Debug.Log("OnLeftRoom");
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log("OnPlayerEnteredRoom: (" + newPlayer.UserId + ")" + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        Debug.Log("OnPlayerLeftRoom: (" + otherPlayer.UserId + ")" + otherPlayer.NickName);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps) {
        Debug.Log("OnPlayerPropertiesUpdate: (" + target.UserId + ")");

        if (changedProps.ContainsKey(GameVariables.PLAYER_HEALTH_FIELD)) {
            //check if owned character has been died
            return;
        }
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log(cause);
    }

    #endregion

}
