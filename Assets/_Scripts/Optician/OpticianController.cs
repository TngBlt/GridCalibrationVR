using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpticianController : MonoBehaviour
{

    public Image AlmostCircle;

    public System.Random rand;

    private int[] rota = new int[] { -90, 90, 180, 0 };


    // Use this for initialization
    void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        SetAlmostCirclePosition();
    }

    private void SetAlmostCirclePosition()
    {
        int rand_x = (int)System.Math.Ceiling(System.Convert.ToDouble(rand.Next(-49, 49)));
        int rand_y = (int)System.Math.Ceiling(System.Convert.ToDouble(rand.Next(-49, 49)));
        AlmostCircle.transform.localPosition = new Vector3(rand_x, rand_y, -20f);
        AlmostCircle.transform.localRotation
    }


}
