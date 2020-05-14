using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TestTile : MonoBehaviour
{
    [SerializeField]
    public AbstractMap _map;

    private float _myconstant;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(_map.UnityTileSize);
        //默认tile初始宽度为100f
        _myconstant = _map.UnityTileSize * _map.transform.localScale.x * 100f;
        Debug.Log(_map.UnityTileSize);
        //_map.UseCustomScale(3.3f);
        Debug.Log(_map.UnityTileSize);
       // _map.transform.localScale = new Vector3(2,2,2);
        Debug.Log(_map.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        DecideTileSize();
        _map.UpdateMap();
       // Debug.Log("test1");
        
    }

    void DecideTileSize()
    {
        _map.UseCustomScale(_myconstant/(_map.transform.localScale.x*_map.UnityTileSize));
    }
}
