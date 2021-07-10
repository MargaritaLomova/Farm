using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Texts"), SerializeField]
    private TMP_Text wheat;
    [SerializeField]
    private TMP_Text sweater;
    [SerializeField]
    private TMP_Text cheese;

    private List<Bounds> takenBounds = new List<Bounds>();
    private Building flyingBuilding;

    private int wheatScore = 0;
    private int sweaterScore = 0;
    private int cheeseScore = 0;

    private void Update()
    {
        if (flyingBuilding != null)
        {
            ReplaceFlyingBuilding();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit)
            {
                GetResource(hit);
            }
        }
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void GetResource(RaycastHit2D hit)
    {
        switch (hit.collider.tag)
        {
            case "Wheat":
                wheatScore++;
                break;
            case "Sweater":
                sweaterScore++;
                break;
            case "Cheese":
                cheeseScore++;
                break;
        }
        UpdateScores();
        Destroy(hit.collider.gameObject);
    }

    private void UpdateScores()
    {
        wheat.text = $"Wheats: {wheatScore}";
        sweater.text = $"Sweaters: {sweaterScore}";
        cheese.text = $"Cheese: {cheeseScore}";
    }

    private void ReplaceFlyingBuilding()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int x = Mathf.RoundToInt(mouseWorldPosition.x);
        int y = Mathf.RoundToInt(mouseWorldPosition.y);

        bool available = flyingBuilding.IsBuildingInCameraView() && !IsPlaceTaken(x, y);

        flyingBuilding.transform.position = new Vector2(x, y);
        flyingBuilding.SetAvailableColor(available);

        if (available && Input.GetMouseButtonDown(0))
        {
            PlaceFlyingBuilding(x, y);
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        if (takenBounds.Count > 0)
        {
            foreach (var bound in takenBounds)
            {
                if (bound.Contains(new Vector2(placeX, placeY)))
                {
                    return true;
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        takenBounds.Add(new Bounds(new Vector2(flyingBuilding.transform.position.x, flyingBuilding.transform.position.y),
                                   new Vector2(flyingBuilding.size.x, flyingBuilding.size.y)));

        flyingBuilding.PlacingBuilding();
        flyingBuilding = null;
    }
}