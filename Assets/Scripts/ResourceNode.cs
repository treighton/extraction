using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNode {

    private Transform resourceNodeTransform;
    private ResourceManager.ResourceType resourceType;
    
    public bool hasBuilding;
    private int resourceAmount;
    private int resourceAmountMax;


    public ResourceNode(Transform resourceNodeTransform, ResourceManager.ResourceType resourceType) {
        this.resourceNodeTransform = resourceNodeTransform;
        this.resourceType = resourceType;
        resourceAmountMax = 300;
        resourceAmount = resourceAmountMax;
    }

    public Vector3 GetPosition() {
        return resourceNodeTransform.position;
    }

    public ResourceManager.ResourceType GetResourceType() {
        return resourceType;
    }

    public ResourceManager.ResourceType GrabResource() {
        
        if (HasResources()) {
            resourceAmount -= 1;

            GameObject DamageText = GameObject.Instantiate(
                GameAssets.i.damageTextPrefab, 
                resourceNodeTransform
            );
            AddAmountIntoGameResources();
            DamageText.transform.GetComponent<TextMeshPro>().SetText("+1");
        }

        return resourceType;
    }


    private void AddAmountIntoGameResources() {
        foreach (ResourceManager.ResourceType resourceType in System.Enum.GetValues(typeof(ResourceManager.ResourceType))) {
            ResourceManager.AddResourceAmount(resourceType, 1);
        }
    }

    public bool HasResources() {
        return resourceAmount > 0;
    }

    public void SetHasBuilding(){
        hasBuilding = true;
    }
}