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
    private RoastController roastController;
    private PlateController plateController;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        roastController = transform.GetComponent<RoastController>();
        plateController = GameObject.Find("Plate").GetComponent<PlateController>();
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

        var onRoastedField = false;
        var onRoastedObject = false;

        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("RoastedField"))
            {
                // ドロップ可能エリア判定
                onRoastedField = true;
            } else if (hit.gameObject == gameObject)
            {
                // ドロップ対象と他の焼き物オブジェクトと重なるか判定
                // TODO gameObjectからraycast(仮)を取得して判定
            }
        }

        if (onRoastedField && !onRoastedObject)
        {
            Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
            TargetPos.z = 0;
            transform.position = TargetPos;
            this.enabled = false;

            roastController.IsNotOnPlate = true;
            roastController.IsRoasted = true;

            // お皿上に物があるかの状態更新
            plateController.OnObject();

            return;
        }

        // ドロップできなかったら戻す
        transform.position = prevPos;
    }
}
