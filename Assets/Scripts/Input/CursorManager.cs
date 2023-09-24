using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float visibilityDuration = 2f;

    private Coroutine hideCursor;

    void Update()
    {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            if (hideCursor == null)
            {
                hideCursor = StartCoroutine(HideCursor());
            }
        }
        else
        {
            if (hideCursor != null)
            {
                StopCoroutine(hideCursor);
                hideCursor = null;
                Cursor.visible = true;
            }
        }
    }

    private IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(visibilityDuration);
        Cursor.visible = false;
    }
}
