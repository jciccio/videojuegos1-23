using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointViewer : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        //GasolineDataList StageData =  JsonUtility.FromJson<GasolineDataList>(jsonFile.text);
        Gizmos.color = Color.green;
        //foreach (GasolineData lapData in StageData.Stage)
        //{
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1);
        //}
    }
}
