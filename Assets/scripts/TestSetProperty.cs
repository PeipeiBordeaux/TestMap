using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TestSetProperty : MonoBehaviour
{
    [SerializeField]
    AbstractMap _mapManager;
    [SerializeField]
    Camera _referenceCamera;

    [SerializeField] public GameObject _targetBounds;
    // Start is called before the first frame update
    void Start()
    {
        SetZoomToFitBounds(GetTargetBounds(),GetScreenBounds());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("执行前");
        // SetZoomToFitBounds(GetTargetBounds(),GetScreenBounds());
        // Debug.Log("执行后");
    }

    void TryToChangeSomething()
    {
        // _mapManager;
        // Vector2dBounds
        
    }
    
    private Vector2dBounds GetScreenBounds()
    {
        var screenWidth = UnityEngine.Screen.width;
        var screenHeight = UnityEngine.Screen.height;

        var sw_world = _referenceCamera.ScreenToWorldPoint(new Vector3(0, 0, _referenceCamera.transform.position.z));
        var sw = _mapManager.WorldToGeoPosition(sw_world);

        var ne_world = _referenceCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, _referenceCamera.transform.position.z));
        var ne = _mapManager.WorldToGeoPosition(ne_world);

        return new Vector2dBounds(new Vector2d(sw.x, sw.y), new Vector2d(ne.x, ne.y));
    }

    private Vector2dBounds GetTargetBounds()
    {
        var _sizeX = _targetBounds.GetComponent<MeshFilter>().mesh.bounds.size.x;
        var _sizeY = _targetBounds.GetComponent<MeshFilter>().mesh.bounds.size.y;
        var sw_world = _referenceCamera.ScreenToWorldPoint(new Vector3(0, 0, _referenceCamera.transform.position.z));
        var sw = _mapManager.WorldToGeoPosition(sw_world);

        var ne_world = _referenceCamera.ScreenToWorldPoint(new Vector3(_sizeX, _sizeY, _referenceCamera.transform.position.z));
        var ne = _mapManager.WorldToGeoPosition(ne_world);

        return new Vector2dBounds(new Vector2d(sw.x, sw.y), new Vector2d(ne.x, ne.y));
    }
    
    private void SetZoomToFitBounds(Vector2dBounds targetBounds, Vector2dBounds screenBounds)
    {
        // 得到东西差值作为精度差值
        var targetLonDelta = targetBounds.East - targetBounds.West;
        //得到南北差值作为纬度差值
        var targetLatDelta = targetBounds.North - targetBounds.South;

        var screenLonDelta = screenBounds.East - screenBounds.West;
        var screenLatDelta = screenBounds.North - screenBounds.South;
        
        // 维度缩放值
        var zoomLatMultiplier = screenLatDelta / targetLatDelta;
        var zoomLonMultiplier = screenLonDelta / targetLonDelta;

        var latZoom = Math.Log(zoomLatMultiplier, 2);
        var lonZoom = Math.Log(zoomLonMultiplier, 2);

        var zoom = (float)(_mapManager.Zoom + Math.Min(latZoom, lonZoom));
       
        _mapManager.SetZoom(zoom);
        _mapManager.UpdateMap();
        
        Debug.Log(_mapManager.UnityTileSize);
        _mapManager.UseCustomScale(3.3f);
        Debug.Log(_mapManager.UnityTileSize);
    }
    
}
