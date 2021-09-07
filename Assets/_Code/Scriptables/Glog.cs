using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Glog")]public class Glog : ScriptableObject
{
    public void MessageLog(string message)
    {
        Debug.Log(message);
    }
}
