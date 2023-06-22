using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugLog : MonoBehaviour
{
    public static debugLog log;

    public TextMeshProUGUI tmp;
    public GameObject img;

    // Start is called before the first frame update
    void Start()
    {
        if(log && log != this)
            Destroy(this);
        else
            log = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeImage(){
        img.SetActive(false);
    }

    public void addText(string txt){
        tmp.text = (txt + '\n');
    }

    public void removeText(){
        tmp.text = "";
    }
}
