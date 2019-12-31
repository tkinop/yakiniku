using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class DragAndDropController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // ドラッグ前の位置
    private Vector2 prevPos;
    private MeatController MeatController;
    private PlateController PlateController;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        MeatController = transform.GetComponent<MeatController>();
        PlateController = GameObject.Find("Plate").GetComponent<PlateController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶しておく
        prevPos = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        // ドラッグ対象追従
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
        TargetPos.z = 0;
        transform.position = TargetPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置に戻す
        //transform.position = prevPos;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            // ドロップ可能エリアなら配置
            if (hit.gameObject.CompareTag("RoastedField"))
            {
                Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
                TargetPos.z = 0;
                transform.position = TargetPos;
                this.enabled = false;

                MeatController.IsNotOnPlate = true;
                MeatController.IsRoasted = true;

                // お皿上に物があるかの状態更新
                PlateController.OnObject();

                return;
            }
        }

        // ドロップできなかったら戻す
        transform.position = prevPos;
    }
}
