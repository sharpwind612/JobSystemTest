using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XXHashMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Excute()
    {
        var hash = XXHash.RandomHash;
        var rand1 = hash.Value01(1);
        var rand2 = hash.Value01(2);
        Debug.Log("rand1:" + rand1 + ",rand2:" + rand2);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 20, 100, 40), "Calc Hash"))
        {
            Excute();
        }
    }
}
