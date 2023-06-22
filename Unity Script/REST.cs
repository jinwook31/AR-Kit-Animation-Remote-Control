using UnityEngine;
using UnityEditor;
using Models;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;


public class REST : MonoBehaviour{
    private readonly string basePath = "http://143.248.6.81:2023";
	private RequestHelper currentRequest;
    public static REST post;

    private int curChapter = 0, curCourse = 0;
    private bool startPost = false;

    void Start(){
        if(post && post != this)
            Destroy(this);
        else
            post = this;
        
        Post();
    }

    void Update(){
        Post();
    }

	private void LogMessage(string title, string message) {
    #if UNITY_EDITOR
		EditorUtility.DisplayDialog (title, message, "Ok");
    #else
		Debug.Log(message);
    #endif
	}

    private void Post(){
		currentRequest = new RequestHelper {
			Uri = basePath + "/manageAR",
            Method = "POST",
            ContentType = "application/json",
            Headers = new Dictionary<string, string>{
                {"Content-Type", "application/json"}
            },
            Body = new ARStatus {
				Type = "checkChangedStatus",
				Chapter = curChapter.ToString(),
				Course = curCourse.ToString()
			}
			//EnableDebug = true
		};

		RestClient.Post<ARStatus>(currentRequest).Then(res => {
			// And later we can clear the default query string params for all requests
			RestClient.ClearDefaultParams();
            string respStat = JsonUtility.ToJson(res, true);
			//this.LogMessage("Success", respStat);

            ARStatus curStat = JsonUtility.FromJson<ARStatus>(respStat);
            curChapter = int.Parse(curStat.Chapter);
            curCourse = int.Parse(curStat.Course);

		}).Catch(err => this.LogMessage("Error", err.Message));
	}

    public int getCurChapter(){
        return this.curChapter;
    }

    public int getCurCourse(){
        return this.curCourse;
    }
}
