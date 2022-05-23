using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPointManager : MonoBehaviour
{
    //public List<CheckpointSystem> CheckPoints { get { return checkPoints; } }
    //public CheckpointSystem CurCheckPoint { get { return checkPoints ? checkPoints[curIndex] : null; } }
    //public static CheckPointManager Instance { get { return instance; } }
    //List<CheckpointSystem> checkPoints = new List<CheckpointSystem>();
    //int curIndex = 0;
    //static CheckPointManager instance = null;

    //protected override void Awake()
    //{
    //    instance = this;
    //    // find all my check points children
    //    for (int i = 0; i < transform.childCount; ++i)
    //    {
    //        CheckpointSystem checkpoint = transform.GetChild(i).GetComponent<CheckpointSystem>();
    //        checkpoint.onTrigger += OnCheckPointTriggered;
    //        checkPoints.Add(checkpoint);
    //    }
    //}
    //public void OnCheckPointTriggered(CheckpointSystem newCheckPoint)
    //{
    //    curIndex = checkPoints.IndexOf(newCheckPoint);
    //}
}