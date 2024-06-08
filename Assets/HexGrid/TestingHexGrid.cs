using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHexGrid : MonoBehaviour {


    [SerializeField] private Transform pfHex;
    [SerializeField] private Camera mainCamera;
    private GridHexXZ<GridObject> gridHexXZ;
    private GridObject lastGridObject;

    private class GridObject {
        public Transform visualTransform;

        public void Show() {
            visualTransform.Find("Selected").gameObject.SetActive(true);
        }

        public void Hide() {
            visualTransform.Find("Selected").gameObject.SetActive(false);
        }

    }

    private void Awake() {
        int width = 30;
        int height = 30;
        float cellSize = 1f;
        gridHexXZ = 
            new GridHexXZ<GridObject>(width, height, cellSize, Vector3.zero, (GridHexXZ<GridObject> g, int x, int y) => new GridObject());

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                Transform visualTransform = Instantiate(pfHex, gridHexXZ.GetWorldPosition(x, z), Quaternion.identity);
                gridHexXZ.GetGridObject(x, z).visualTransform = visualTransform;
                gridHexXZ.GetGridObject(x, z).Hide();
            }
        }

    }

    private void Update() {
        if (lastGridObject != null) {
            lastGridObject.Hide();
        }

        lastGridObject = gridHexXZ.GetGridObject(Mouse3D.GetMouseWorldPosition());

        if (lastGridObject != null) {
            lastGridObject.Show();
        }

        // if(Input.GetMouseButtonDown(1)){
        //     // gridHexXZ.GetXZ(GetMouseWorldPosition(), out int x, out int z);
        //     // GridObject gridObject = gridHexXZ.GetGridObject(x, z);
        //     // if(gridObject != null){
        //     //     // destroyBuilding.DestroyObject();
        //     // }
        //     Vector3 position = GetMouseWorldPosition();
        //     Debug.Log(position);
        // }
        
    }

    // private Vector3 GetMouseWorldPosition(){
    //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //     if(Physics.Raycast(ray, out RaycastHit raycastHit)){
    //         Debug.Log(raycastHit.point);
    //         return raycastHit.point;
    //     } else {
    //         return Vector3.zero;
    //     }
    // }
}