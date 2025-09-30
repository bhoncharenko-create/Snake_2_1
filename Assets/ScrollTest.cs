using DerailedTools.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTest : MonoBehaviour
{
    public ScrollRect scroll;
    public RectTransform target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            scroll.ScrollToObjectY(target, offsetPercentage: -50, duration: 0.1f);
        }
    }
}
