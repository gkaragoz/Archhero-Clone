using UnityEngine;

public class CharacterTargetSelector : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private GameObject _selectedTarget;

    public bool HasTarget { get { return _selectedTarget == null ? false : true; } }
    public GameObject SelectedTarget { get { return _selectedTarget; } }

    public void SearchTarget() {
        GameObject[] potantialTargets = GameObject.FindGameObjectsWithTag("Target");

        int enemyCount = potantialTargets.Length;
        if (enemyCount <= 0) {
            return;
        }

        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int ii = 0; ii < enemyCount; ii++) {
            GameObject potantialTarget = potantialTargets[ii];
            //if (potantialTarget.IsDead) {
            //    if (potantialTarget == _selectedTarget) {
            //        _selectedTarget = null;
            //    }
            //    continue;
            //}

            if (!potantialTarget.gameObject.activeInHierarchy) {
                continue;
            }

            float potantialDistance = Vector3.Distance(transform.position, potantialTarget.transform.position);
            if (potantialDistance <= closestDistance) {
                closestTarget = potantialTarget;
                closestDistance = potantialDistance;

                _selectedTarget = closestTarget;
            }
        }
    }

    public void DeselectTarget() {
        _selectedTarget = null;
    }

}
