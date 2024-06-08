using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

namespace HexGrid{
    public class GridBuildingSystem : MonoBehaviour {
        [SerializeField] private Transform testBuilding;
        private Transform builtTransform;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;
        private GridHexXZ<GridObject> gridHexA, gridB, gridC, gridD, gridE, gridF, gridG, gridH;

        private void Awake(){
            int gridWidth = 15;
            int gridHeight = 15;
            float cellSize = 1.75f;
            // int verticalGridWidth = 1;
            // int verticalGridHeight = 2;
            gridHexA = new GridHexXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0 ,0 ,0 ), (GridHexXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridB = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0,0,30), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridC = new GridXZ<GridObject>(verticalGridWidth, verticalGridHeight, cellSize, new Vector3(30,0,0), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridD = new GridXZ<GridObject>(verticalGridWidth, verticalGridHeight, cellSize, new Vector3(75,0,0), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridE = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0,0,75), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridF = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(75,0,75), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridG = new GridXZ<GridObject>(verticalGridWidth, verticalGridHeight, cellSize, new Vector3(30,0,90), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
            // gridH = new GridXZ<GridObject>(verticalGridWidth, verticalGridHeight, cellSize, new Vector3(75,0,90), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));  

            for(int x = 0; x < gridWidth; x++){
                for(int z = 0; z < gridHeight; z++){
                    Instantiate(testBuilding, gridHexA.GetWorldPosition(x, z), Quaternion.identity);
                }

            }
                
        }

        public class GridObject {

            private GridHexXZ<GridObject> gridHex;
            private int x;
            private int z;
            private Transform transform;

            public GridObject(GridHexXZ<GridObject> gridHex, int x, int z) {
                this.gridHex = gridHex;
                this.x = x;
                this.z = z;
            }

            public void SetTransform(Transform transform){
                this.transform = transform;
                gridHex.TriggerGridObjectChanged(x, z);
            }

            public void ClearTransform(){
                transform = null;
                gridHex.TriggerGridObjectChanged(x, z);
            }

            public bool CanBuild(){
                return transform == null;
            }

            public override string ToString()
            {
                return x + ", " + z + "\n" + transform;
            }
        }

        // private void Update(){
        //     if(Input.GetMouseButtonDown(0)){
                
        //         gridA.GetXZ(GetMouseWorldPosition(), out int xA, out int zA);
        //         // gridB.GetXZ(GetMouseWorldPosition(), out int xB, out int zB);
        //         // gridC.GetXZ(GetMouseWorldPosition(), out int xC, out int zC);
        //         // gridD.GetXZ(GetMouseWorldPosition(), out int xD, out int zD);
        //         // gridE.GetXZ(GetMouseWorldPosition(), out int xE, out int zE);
        //         // gridF.GetXZ(GetMouseWorldPosition(), out int xF, out int zF);
        //         // gridG.GetXZ(GetMouseWorldPosition(), out int xG, out int zG);
        //         // gridH.GetXZ(GetMouseWorldPosition(), out int xH, out int zH);

        //         GridObject gridObjectA = gridA.GetGridObject(xA, zA);
        //         // GridObject gridObjectB = gridB.GetGridObject(xB, zB);
        //         // GridObject gridObjectC = gridC.GetGridObject(xC, zC);
        //         // GridObject gridObjectD = gridD.GetGridObject(xD, zD);
        //         // GridObject gridObjectE = gridE.GetGridObject(xE, zE);
        //         // GridObject gridObjectF = gridF.GetGridObject(xF, zF);
        //         // GridObject gridObjectG = gridG.GetGridObject(xG, zG);
        //         // GridObject gridObjectH = gridH.GetGridObject(xH, zH);

        //         if(gridObjectA != null){
        //             if(gridObjectA.CanBuild()){
        //                 builtTransform = Instantiate(testBuilding, gridA.GetWorldPosition(xA, zA), Quaternion.identity);
        //                 Debug.Log(xA + ", "+ zA);
        //                 gridObjectA.SetTransform(builtTransform);
        //             } else {
        //                 UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //             }
        //         }
        //         // } else if(gridObjectB != null){
        //         //     if(gridObjectB.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridB.GetWorldPosition(xB, zB), Quaternion.identity);
        //         //         Debug.Log(xB + ", "+ zB);
        //         //         gridObjectB.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // } else if(gridObjectC != null){
        //         //     if(gridObjectC.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridC.GetWorldPosition(xC, zC), Quaternion.identity);
        //         //         Debug.Log(xC + ", "+ zC);
        //         //         gridObjectC.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // }
        //         // else if(gridObjectD != null){
        //         //     if(gridObjectD.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridD.GetWorldPosition(xD, zD), Quaternion.identity);
        //         //         Debug.Log(xD + ", "+ zD);
        //         //         gridObjectD.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // }
        //         // else if(gridObjectE != null){
        //         //     if(gridObjectE.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridE.GetWorldPosition(xE, zE), Quaternion.identity);
        //         //         Debug.Log(xE + ", "+ zE);
        //         //         gridObjectE.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // }
        //         // else if(gridObjectF != null){
        //         //     if(gridObjectF.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridF.GetWorldPosition(xF, zF), Quaternion.identity);
        //         //         Debug.Log(xF + ", "+ zF);
        //         //         gridObjectF.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // }
        //         // else if(gridObjectG != null){
        //         //     if(gridObjectG.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridG.GetWorldPosition(xG, zG), Quaternion.identity);
        //         //         Debug.Log(xG + ", "+ zG);
        //         //         gridObjectG.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // }
        //         // else if(gridObjectH != null){
        //         //     if(gridObjectH.CanBuild()){
        //         //         builtTransform = Instantiate(testBuilding, gridH.GetWorldPosition(xH, zH), Quaternion.identity);
        //         //         Debug.Log(xH + ", "+ zH);
        //         //         gridObjectH.SetTransform(builtTransform);
        //         //     } else {
        //         //         UtilsClass.CreateWorldTextPopup("Cannot build here!!", GetMouseWorldPosition());
        //         //     }
        //         // } 
        //         // else {
        //         //     Debug.Log(GetMouseWorldPosition());
        //         //     UtilsClass.CreateWorldTextPopup("Cannot place outside the grid!!", GetMouseWorldPosition());
        //         // }
                
        //     }
        //     // if(Input.GetMouseButtonDown(1)){
        //     //     grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
        //     //     GridObject gridObject = grid.GetGridObject(x, z);
        //     //     if(gridObject != null){
        //     //         // destroyBuilding.DestroyObject();
        //     //         gridObject.ClearTransform();
        //     //     }
        //     // }
        // }
        // private Vector3 GetMouseWorldPosition(){
        //     Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //     if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask)){
        //         return raycastHit.point;
        //     } else {
        //         return Vector3.zero;
        //     }
        // }
    }
}

