using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MouseCursor : MonoBehaviour
{
    private Vector2 _mousePosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void Update()
    {
        MouseMove();
        MouseClick();
    }

    private void MouseMove()
    {
        _mousePosition = MainCam.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector2(Mathf.RoundToInt(_mousePosition.x) + 0.5f, Mathf.RoundToInt(_mousePosition.y) + 0.5f);
    }

    private void MouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.zero, 0f);

            if (hit.collider == null) return;

            if(hit.collider.CompareTag("Character"))
            {
                Character myChar = hit.collider.GetComponent<Character>();
                myChar.Picked();
            }
            else if(hit.collider.CompareTag("Field"))
            {
                PoolManager.Instance.Pop("Character", transform.position);
            }
            else
            {

            }
        }
    }
}
