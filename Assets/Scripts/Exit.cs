using UnityEngine;

public class Exit : MonoBehaviour
{
    public MapManager mapManager;
    public Transform player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveData data = SaveSystem.GetData();
            data.currentMap += 1;
            SaveSystem.Save(data);
            player.GetComponent<PlayerInventory>().SetMoneyToZero();
            player.localPosition = StoreData.SPAWN_POINT_PLAYER;
            mapManager.SetMap();
        }
    }
}
