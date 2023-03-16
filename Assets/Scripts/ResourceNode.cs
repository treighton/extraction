
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode {

    public static event EventHandler OnResourceNodeClicked;

    private Transform resourceNodeTransform;
    private ResourceManager.ResourceType resourceType;
    
    private int resourceAmount;
    private int resourceAmountMax;
    private bool hasBuilding;

    public ResourceNode(Transform resourceNodeTransform, ResourceManager.ResourceType resourceType) {
        this.resourceNodeTransform = resourceNodeTransform;
        this.resourceType = resourceType;
        resourceAmountMax = 3000;
        resourceAmount = resourceAmountMax;
    }

    public Vector3 GetPosition() {
        return resourceNodeTransform.position;
    }

    public ResourceManager.ResourceType GetResourceType() {
        return resourceType;
    }

    public ResourceManager.ResourceType GrabResource() {
        resourceAmount -= 1;

        return resourceType;
    }

    public bool HasResources() {
        return resourceAmount > 0;
    }
}