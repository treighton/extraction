
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour {

    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
    
    public Sprite buildingConstruction_1;
    public Sprite buildingConstruction_2;
    public Sprite buildingConstruction_3;
    public Sprite buildingConstruction_Built;
    public GameObject damageTextPrefab;
}
