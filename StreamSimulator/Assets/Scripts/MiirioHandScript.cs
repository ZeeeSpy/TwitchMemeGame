using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiirioHandScript : MonoBehaviour
{
    public SpriteRenderer miiriohand;
    private Vector3 mousePosition;
    private Vector3 correction;

    private void Start()
    {
        correction = new Vector3(3.12f, -5.18f, 0.00f);
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        miiriohand.transform.position = mousePosition + correction;
    }
}
