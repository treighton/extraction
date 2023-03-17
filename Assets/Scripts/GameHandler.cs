using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    private static GameHandler instance;
    [SerializeField] GameObject resourceUI;

    [SerializeField] int resources = 100;
    [SerializeField] int credits = 100;
    [SerializeField] int scrapMetal = 100;
    [SerializeField] private Transform[] ResourceNodeTransformArray;
    public GameObject damageTextPrefab, enemyInstance;
    public Dictionary<Transform, ResourceNode> resourceNodeList;

    private void Awake() {
        instance = this;

        ResourceManager.Init();

        resourceNodeList = new Dictionary<Transform, ResourceNode>();
        resourceUI = GameObject.FindWithTag("Resource Text");

        foreach (Transform resourceNode in ResourceNodeTransformArray) {
            resourceNodeList.Add(
                resourceNode.transform, 
                new ResourceNode(resourceNode, ResourceManager.ResourceType.Vulcanite)
            );
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += delegate (object sender, TickSystem.OnTickEventArgs e) {        
            foreach (var resource in resourceNodeList) {
                if (resource.Value.hasBuilding) {
                    resource.Value.GrabResource();
                }
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;

            RaycastHit2D hit = CastRay();

            if (hit.collider != null && hit.collider.gameObject.tag == "Resource Location") {
                new Building(hit.collider.gameObject.transform.position, 30, resourceNodeList[hit.collider.gameObject.transform]);
            }
        }
        if (ResourceManager.GetResourceAmount(ResourceManager.ResourceType.Vulcanite) > 0) {

            resourceUI.GetComponent<TextMeshProUGUI>().SetText(""+ResourceManager.GetResourceAmount(ResourceManager.ResourceType.Vulcanite));
        }
    }

    private RaycastHit2D CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

        return hit;
    }
}
