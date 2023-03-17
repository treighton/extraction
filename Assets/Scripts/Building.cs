using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

    private GameObject gameObject;
    private SpriteRenderer spriteRenderer;
    private ResourceNode resourceNode;
    private int buildTick;
    private int buildTickMax;
    private bool isConstructing;

   public Building(Vector3 position, int ticksToConstruct, ResourceNode resourceNodeObj) {
        resourceNode = resourceNodeObj;
        gameObject = new GameObject("Building");
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameAssets.i.buildingConstruction_1;
        spriteRenderer.sortingOrder = 1;
        gameObject.transform.position = position;

        buildTick = 0;
        buildTickMax = ticksToConstruct;
        isConstructing = true;
        
        TickSystem.onTick += TickSystem_OnTick;
   }

    private void TickSystem_OnTick(object sender, TickSystem.OnTickEventArgs e) {
        if (isConstructing) {
            buildTick += 1;
            float buildTickNormalized = buildTick * 1f / buildTickMax;
            if (buildTickNormalized >= .3f) spriteRenderer.sprite = GameAssets.i.buildingConstruction_2;
            if (buildTickNormalized >= .6f) spriteRenderer.sprite = GameAssets.i.buildingConstruction_3;
            if (buildTickNormalized >=  1f) spriteRenderer.sprite = GameAssets.i.buildingConstruction_Built;

            if (buildTick >= buildTickMax) {
                // Building is fully constructed
                isConstructing = false;
                resourceNode.SetHasBuilding();
            }
        }
    }
}
