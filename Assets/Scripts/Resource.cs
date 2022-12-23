using System.Collections;
using UnityEngine;

public class Resource : MonoBehaviour
{



    private Building parentBuilding;

    public void Spawn(Building building)
    {
        parentBuilding = building;

        StartCoroutine(FallAnimation());
    }

    private IEnumerator FallAnimation()
    {
        transform.position = parentBuilding.transform.position;

        while (transform.position.y < parentBuilding.transform.position.y + 1.5f)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * 0.2f, transform.position.y + Time.deltaTime * 6);
            yield return null;
        }

        var randomX = Random.Range(-1, 2.5f);
        while (transform.position.y > parentBuilding.transform.position.y - 0.2f)
        {
            transform.position = new Vector2(transform.position.x + Time.deltaTime * randomX, transform.position.y - Time.deltaTime * 4);
            yield return null;
        }
    }
}