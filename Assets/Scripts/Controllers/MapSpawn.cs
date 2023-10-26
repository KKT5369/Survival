using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSpawn : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    
    
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var playerPos = GameManager.Instance.playerController.transform.position;
            Vector3Int playerTilePosition = tilemap.WorldToCell(playerPos);
            Vector3 localPosition = tilemap.CellToLocalInterpolated(playerTilePosition);
            print(localPosition);
        }
    }
    
    
}
