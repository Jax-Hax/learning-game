using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class PlaceMonke : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private bool canBePlaced;
    private bool isMoving;
    public GameObject range;
    public Vector3 seeAbleRange;
    public Monke monke;
    public TextMeshProUGUI cost;
    public GameManager gameManager;
    public TextMeshProUGUI monkeName;
    public GameObject monkeReplacement;
    public GameObject movingMonkes;
    private RectTransform rectTransform;
    [SerializeField]
    private Image monkePicture;
    [SerializeField]
    private Canvas canvas;
    private Image rangeImage;
    [SerializeField]
    private Color placeableColor;
    [SerializeField]
    private Color nonPlaceableColor;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    public Transform monkePlacementParent;
    private void Start()
    {
        range.SetActive(false);
        canBePlaced = false;
        cost.text = monke.cost.ToString();
        monkePicture.sprite = monke.monkePicture;
        monkeReplacement.GetComponent<Image>().sprite = monke.monkePicture;
        rectTransform = GetComponent<RectTransform>();
        range.transform.localScale = seeAbleRange;
        rangeImage = range.GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(isMoving)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            transform.position = canvas.transform.TransformPoint(pos);
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
            if (isGrounded)
            {
                rangeImage.color = nonPlaceableColor;
                canBePlaced = false;
            }
            else
            {
                rangeImage.color = placeableColor;
                canBePlaced = true;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canBePlaced && gameManager.moneyInt >= monke.cost)
        {
            gameManager.UpdateMoney(-monke.cost, true);
            range.SetActive(false);
            Vector3 placement = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(monke.monkePrefab, placement, Quaternion.identity, monkePlacementParent);
            transform.SetParent(monkeReplacement.transform);
            rectTransform.anchoredPosition = Vector3.zero;
            transform.SetSiblingIndex(0);
            gameManager.IsShopClickedTrueOrFalse(true);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        monkeName.text = monke.monkeName;
        if (gameManager.moneyInt >= monke.cost)
        {
            gameObject.transform.SetParent(movingMonkes.transform);
            range.SetActive(true);
            isMoving = true;
            gameManager.IsShopClickedTrueOrFalse(false);
        }
        else
        {
            isMoving = false;
        }
    }
}