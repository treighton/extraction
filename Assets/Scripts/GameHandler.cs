using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{

    private static GameHandler instance;

    [SerializeField] int resources = 100;
    [SerializeField] int credits = 100;
    [SerializeField] int scrapMetal = 100;
    [SerializeField] private Transform[] ResourceNodeTransformArray;
    public GameObject damageTextPrefab, enemyInstance;
    public string textToDisplay;

    private List<ResourceNode> resourceNodeList;

    private void Awake() {
        instance = this;

        ResourceManager.Init();

        resourceNodeList = new List<ResourceNode>();

        foreach (Transform resourceNode in ResourceNodeTransformArray) {
            resourceNodeList.Add(new ResourceNode(resourceNode, ResourceManager.ResourceType.Vulcanite));
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        TickSystem.onTick += delegate (object sender, TickSystem.OnTickEventArgs e) {        
                GameObject DamageText = Instantiate(damageTextPrefab, enemyInstance.transform);
                DamageText.transform.GetComponent<TextMeshPro>().SetText(textToDisplay);
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
                Debug.Log(hit.collider.gameObject.transform.position);
                Debug.Log(mousePos);
                new Building(hit.collider.gameObject.transform.position, 30);
            }
        }
    }

    private RaycastHit2D CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

        return hit;
    }
}
