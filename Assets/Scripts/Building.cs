using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private BuildingType _buildingType;

    [HideInInspector]
    public Vector2Int size = Vector2Int.one;


    private Resource resourcePrefab;

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Gizmos.color = new Color(1, 1, 1, 0.3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, y, 0), new Vector3(1, 1, 0.1f));
            }
        }
    }

    //public void Set

    public void SetAvailableColor(bool available)
    {
        spriteRenderer.material.color = available ? Color.green : Color.red;
    }

    public bool IsBuildingInCameraView()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds);
    }

    public void PlacingBuilding()
    {
        spriteRenderer.material.color = Color.white;
        StartCoroutine(SpawnResources(7));
    }

    private IEnumerator SpawnResources(int timeToSpawn)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeToSpawn);

            var newResource = Instantiate(resourcePrefab);
            newResource.Spawn(this);
        }
    }
}