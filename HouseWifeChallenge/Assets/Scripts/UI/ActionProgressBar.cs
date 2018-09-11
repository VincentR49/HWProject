using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ActionProgressBar : MonoBehaviour {

    public ActionTracker actionTraker;
    public Image progressBar; // should be inside a container
    private Canvas canvas;
    public float offsetX = 0;
    public float offsetY = 0.5f;
    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionTraker.action != null)
        {
            Show();
            progressBar.fillAmount = actionTraker.currentProgress / actionTraker.action.duration;
            progressBar.transform.parent.transform.position = actionTraker.interactible.transform.position + Vector3.right * offsetX + Vector3.up * offsetY;
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        canvas.enabled = false;
    }

    private void Show()
    {
        canvas.enabled = true;
    }
}
