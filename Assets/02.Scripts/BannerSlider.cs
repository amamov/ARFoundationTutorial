using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BannerSlider : MonoBehaviour
{
    public GameObject Scrollbar;
    public ScrollRect sr;

    private float scrollPos = 0;
    private float[] pos;
    private int SliderNum;
    private float timeElapsed;

    private void SlideBanner() 
    {
        if (timeElapsed <= 3)
        {
            timeElapsed += Time.deltaTime;
            Scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(Scrollbar.GetComponent<Scrollbar>().value, pos[SliderNum], 10 * Time.deltaTime);

        }
        else
        {
            SliderNum++;
            timeElapsed = 0;
            if (SliderNum >= pos.Length)
            {
                SliderNum = 0;
            }
        }
    }

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        scrollPos = Scrollbar.GetComponent<Scrollbar>().value;
        SlideBanner();
    }
}
