﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartHandler : MonoBehaviour
{

    public GridController gridController;
    public GameController gameController;
    private RaycastHit obj;
    public GameObject Menu;
    public GameObject startButton;
    public GameObject approxButton;
    public GameObject shrinkButton;
    public Image shrinkLoader;
    public Image approxLoader;
    public Image startLoader;
    public GameObject countDownText;
    private float timer;
    private float modeTimer;
    private float countDown = 3.0f;

    void Start()
    {
        ResetTimer();
        ResetModeTimer();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I)){
            StartIPDCalib();
            Menu.SetActive(false);
        }
        obj = gridController.GetCurrentCollider();
        if (obj.collider && timer > 0)
        {
            if (ReferenceEquals(obj.collider.gameObject, startButton))
            {
                startLoader.fillAmount =  1.0f - timer;
                timer -= Time.deltaTime;
            }
            else if (ReferenceEquals(obj.collider.gameObject, approxButton))
            {
                approxLoader.fillAmount = 1.0f - modeTimer*2 ;
                modeTimer -= Time.deltaTime;
            }
            else if (ReferenceEquals(obj.collider.gameObject, shrinkButton))
            {
                shrinkLoader.fillAmount = 1.0f - modeTimer*2 ;
                modeTimer -= Time.deltaTime;
            }
            else
            {
                startLoader.fillAmount = 0.0f;
                ResetTimer();
                ResetModeTimer();
            }
        }
        if (timer < 0)
        {
            // Print the timer, the seconds is rounded to have 3,2,1 value like seconds
            countDownText.GetComponent<TextMesh>().text = Math.Ceiling(System.Convert.ToDouble(countDown)).ToString();
            countDown -= Time.deltaTime;
            Menu.transform.position += new Vector3(0, 0, -20.0f); // André : Why ?
            if (countDown < 0)
            {
                print("START");
                gameController.is_started = true;
                ResetTimer();
                Menu.SetActive(false);
                countDownText.SetActive(false);
            }
        }
        if (modeTimer < 0)
        {
            if (ReferenceEquals(obj.collider.gameObject, approxButton))
            {
                gameController.choosenMode = "approx";
                shrinkLoader.fillAmount = 0.0f;
            }
            else if (ReferenceEquals(obj.collider.gameObject, shrinkButton))
            {
                gameController.choosenMode = "shrink";
                approxLoader.fillAmount = 0.0f;
            }
        }
    }

    private void StartIPDCalib()
    {
        
    }

    private void ResetTimer()
    {
        timer = 1.0f;
    }
    private void ResetModeTimer()
    {
        modeTimer = 0.5f;
    }
}
