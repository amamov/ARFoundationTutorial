using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPlacment : MonoBehaviour
{
    public GameObject ObjToPlace;
    public Transform ObjPosition;

    private GameObject obj;
    private Vector3 MovePos, Offset;
    private bool isDrag; // Update is called once per frame


    public void MakeObject()
    {
        GameObject obj = Instantiate(ObjToPlace, ObjPosition.position, Quaternion.identity);
    }

    private void TouchDrag()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    obj = hit.transform.gameObject;
                    MovePos = new Vector3(touch.position.x, touch.position.y, obj.transform.position.z - Camera.main.transform.position.z);
                    Offset = obj.transform.position - Camera.main.ScreenToWorldPoint(MovePos);
                    isDrag = true;
                }
            }

            if (touch.phase == TouchPhase.Moved && isDrag)
            {
                MovePos = new Vector3(touch.position.x, touch.position.y, obj.transform.position.z - Camera.main.transform.position.z);
                MovePos = Camera.main.ScreenToWorldPoint(MovePos);
                obj.transform.position = MovePos + Offset;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                obj = null;
                isDrag = false;
            }

        }
    }


    private void PinchScale()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDelta = (touch0.position - touch1.position).magnitude;

            float zoomDelta = prevTouchDelta - touchDelta;

            Ray ray = Camera.main.ScreenPointToRay(touch0.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                obj = hit.transform.gameObject;
                obj.transform.localScale = new Vector3(obj.transform.localScale.x + zoomDelta * -0.001f, obj.transform.localScale.y + zoomDelta * -0.001f, obj.transform.localScale.z + zoomDelta * -0.001f);
            }

        }
    }

    private void Update()
    {
        TouchDrag();
        PinchScale();
    }
}
