using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObjectOnPlane : MonoBehaviour{


    private ARRaycastManager raycastManager;
    private GameObject spawnedObj;
    private GameObject[] planeMesh;

    [SerializeField]
    private GameObject[] PlaceablePrefab;

    [SerializeField]
    private ARSessionOrigin ARSessionOrigin;

    [SerializeField]
    private ARCameraManager cameraMng;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private int prefabIdx = 0, prevIdx = 0;


    private void Awake(){
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition){
        if(Input.touchCount > 0){
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    private void Update(){
        prefabIdx = REST.post.getCurChapter();  //현재 애니메이션 가져오기

        if(prefabIdx != prevIdx){
            prevIdx = prefabIdx;
            changePrefab();
        }

        if(prefabIdx > 0){
            planeMesh = GameObject.FindGameObjectsWithTag("detectedPlane");
            debugLog.log.removeText();
            debugLog.log.removeImage();
            for(int i=0; i<planeMesh.Length; i++){
                planeMesh[i].SetActive(false);
            }
        }

        if(!TryGetTouchPosition(out Vector2 touchPosition)){
            return;
        }

        if(raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon)){
            var hitPos = s_Hits[0].pose;

            if(spawnedObj == null){
                // Initiate First Object
                spawnedObj = Instantiate(PlaceablePrefab[prefabIdx], hitPos.position, hitPos.rotation);
                //debugLog.log.addText2("Before hitPos: " + hitPos.position.x + "/" + hitPos.position.y + "/" + hitPos.position.z + "/n spawnPos:" + spawnedObj.transform.position.x + "/" + spawnedObj.transform.position.x + "/" + spawnedObj.transform.position.x + "/");
                ARSessionOrigin.MakeContentAppearAt(spawnedObj.transform, hitPos.position, hitPos.rotation);
                //prefabIdx++;

                if(prefabIdx == 0){
                    debugLog.log.removeImage();
                }
                
            }else{
                // Move spawned object to touched Position
                spawnedObj.transform.position = hitPos.position;
            }

            spawnedObj.transform.LookAt(cameraMng.transform);
            spawnedObj.transform.eulerAngles = new Vector3(0f, spawnedObj.transform.eulerAngles.y, 0f);
        }
    }


    public void changePrefab(){
        if(spawnedObj == null){
            Debug.Log("Not Initiated yet");
            return;

        }else{
            if(prefabIdx >= PlaceablePrefab.Length){
                prefabIdx = 0;
            }

            Destroy(spawnedObj);

            spawnedObj = Instantiate(PlaceablePrefab[prefabIdx], spawnedObj.transform.position, spawnedObj.transform.rotation);
            //debugLog.log.addText2("Before hitPos: " + hitPos.position.x + "/" + hitPos.position.y + "/" + hitPos.position.z + "/n spawnPos:" + spawnedObj.transform.position.x + "/" + spawnedObj.transform.position.x + "/" + spawnedObj.transform.position.x + "/");
            //ARSessionOrigin.MakeContentAppearAt(spawnedObj.transform, hitPos.position, hitPos.rotation);spawnedObj.transform.position = hitPos.position;

            //prefabIdx++;
        }

        spawnedObj.transform.LookAt(cameraMng.transform);
        spawnedObj.transform.eulerAngles = new Vector3(0f, spawnedObj.transform.eulerAngles.y, 0f);
    }
}
