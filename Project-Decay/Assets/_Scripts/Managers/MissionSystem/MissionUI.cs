using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour {

    public GameObject missionLog;
    public Image[] missionLogUI;
    bool missionUIActive = false;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(test());
        }
        
	}

    public void OpenMissionLog()
    {
        missionLog.SetActive(true);
        for (int i = 0; i < missionLogUI.Length; i++)
        {
            missionLogUI[i].fillAmount += 0.1f;
        }
    }

    public void CloseMissionLog()
    {
        for (int i = 0; i < missionLogUI.Length; i++)
        {
            missionLogUI[i].fillAmount -= 0.1f;
        }
        if (missionLogUI[missionLogUI.Length - 1].fillAmount == 0f)
        {
            missionLog.SetActive(false);
        }
    }

    IEnumerator test()
    {
        float time = missionLogUI[missionLogUI.Length - 1].fillAmount;

        if (missionUIActive == true)
        {
            while (time > 0f)
            {
                CloseMissionLog();
                time = missionLogUI[missionLogUI.Length - 1].fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        else
        {
            while (time != 1f)
            {
                OpenMissionLog();
                time = missionLogUI[missionLogUI.Length - 1].fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        if(time == 0f)
        {
            missionUIActive = false;
            StopCoroutine(test());                
        }

        else if(time == 1f)
        {
            missionUIActive = true;
            StopCoroutine(test());
        }

    }

}

    

