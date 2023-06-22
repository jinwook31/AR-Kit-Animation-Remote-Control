using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARBodyTracker : MonoBehaviour{
    [SerializeField] GameObject bodyPrefab;
    [SerializeField] Vector3 offset;

    [SerializeField]
    private ARSessionOrigin ARSessionOrigin;

    ARHumanBodyManager humanBodyManager;
    
    GameObject bodyObject;

    public static ARBodyTracker bodyTracker;
    private ARHumanBody trackedPosition = null;
    private bool isTracked = false;

    void Awake(){
        humanBodyManager = (ARHumanBodyManager)GetComponent<ARHumanBodyManager>();

        if(bodyTracker && bodyTracker != this)
            Destroy(this);
        else
            bodyTracker = this;
    }

    private void OnEnable(){
        humanBodyManager.humanBodiesChanged += OnHumanBodiesChanged;
    }

    private void OnDisable(){
        humanBodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
    }

    void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs){
        foreach(ARHumanBody humanBody in eventArgs.added){
            bodyObject = Instantiate(bodyPrefab, humanBody.transform);
            //ARSessionOrigin.MakeContentAppearAt(bodyObject.transform, bodyObject.transform.position, bodyObject.transform.rotation);
            trackedPosition = humanBody;
            isTracked = true;
        }

        foreach(ARHumanBody humanBody in eventArgs.updated){
            //Delay 넣어주기 !! @@@

            if(bodyObject != null){
                continue;
                //bodyObject.transform.position = humanBody.transform.position;
                bodyObject.transform.position = Vector3.MoveTowards(bodyObject.transform.position, humanBody.transform.position, 0.5f);
                //bodyObject.transform.rotation = humanBody.transform.rotation;
                bodyObject.transform.rotation = Quaternion.RotateTowards(bodyObject.transform.rotation, humanBody.transform.rotation, 0.1f);
                bodyObject.transform.localScale = humanBody.transform.localScale;

                // Position 값만 넘겨주기 -> 물체 증강은 occlusion, environment가 되는 기능으로 사용!
                trackedPosition = humanBody;
                isTracked = true;
            }
        }

        foreach(ARHumanBody humanBody in eventArgs.removed){
            if(bodyObject != null){
                Destroy(bodyObject);
                continue;
            }
        }
    }




    public bool checkTracking(){
        if(isTracked){
            isTracked = false;
            return true;
        }

        return isTracked;
    }

    public Vector3 getHumanScale(){
        if(trackedPosition != null){
            return trackedPosition.transform.localScale;
        }else{
            return new Vector3(0,0,0);
        }
    }


    public Vector3 getHumanPosition(){
        if(trackedPosition != null){
            return trackedPosition.transform.position;
        }else{
            return new Vector3(0,0,0);
        }
    }

    public Quaternion getHumanRotation(){
        if(trackedPosition != null){
            trackedPosition.transform.rotation *= Quaternion.Euler(0, 180f, 0);
            return trackedPosition.transform.rotation;
        }else{
            return new Quaternion();
        }
    }
}

