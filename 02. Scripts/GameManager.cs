using UnityEngine;

public class GameManager : MonoBehaviour {
    public Transform SpawnPoint;
    public GameObject m_Player_Prefab;

    private void Start()
    {
        GameObject m_Player = Instantiate(m_Player_Prefab) as GameObject;
    }
}
