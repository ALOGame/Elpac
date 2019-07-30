using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public SlotGrid slotGrid;
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = new LevelLoader();
        levelLoader.LoadLevel("levels/default/test.txt");

        if (levelLoader.loadedLevel.isLoaded)
            CreateAppliances();
        else
            Debug.LogError("Error when loading level");
    }

    private void CreateAppliances()
    {
        slotGrid.AddAppliances(levelLoader.loadedLevel.appliances);
    }

    private void Update()
    {
        // Check if user clicked on a appliance if so interact with it
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

            if (hit.collider != null) // Something was clicked
            {
                Appliance appliance = hit.collider.GetComponent<Appliance>();

                if (GameManager.gameRunning)
                {
                    if (appliance.canInteractOnPlay)
                    {
                        appliance.InteractOnPlay();
                    }
                }
            }
        }
    }
}
