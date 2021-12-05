using UnityEngine;

public class Painter : MonoBehaviour
{
    /// <summary>
    public Color32 penColor;

    public Transform rayOrigin;

    private RaycastHit hitInfo;
    //这个画笔是不是正在被手柄抓着
    private bool IsGrabbing;
    private static Board board;
    public LayerMask mask;

    private void Start()
    {
        //将画笔部件设置为画笔的颜色，用于识别这个画笔的颜色
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer.transform == transform)
            {
                continue;
            }
            renderer.material.color = penColor;
        }
        if (!board)
        {
            board = FindObjectOfType<Board>();
        }

        mask = 1 << LayerMask.NameToLayer("Page");
    }

    private void Update()
    {
        Ray r = new Ray(rayOrigin.position, rayOrigin.forward);
        // Debug.Log(mask.value);
        // Debug.Log(Physics.Raycast(r, out hitInfo, Mathf.Infinity, mask.value));
        if (Physics.Raycast(r, out hitInfo, 0.05f, mask))
        {
            // Debug.Log(hitInfo.collider.gameObject.layer);
            // Debug.Log(hitInfo.collider);
            if (hitInfo.collider.tag == "Board")
            {
                // Debug.Log(333333);
                board.SetPainterPositon(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
                //当前笔的颜色
                // Debug.Log(penColor);
                board.SetPainterColor(penColor);
                board.IsDrawing = true;
                IsGrabbing = true;
                // Debug.Log("x" + hitInfo.textureCoord.x);
                // Debug.Log("y" + hitInfo.textureCoord.y);
            }
        }
        else if (IsGrabbing)
        {
            board.IsDrawing = false;
            IsGrabbing = false;
            // Debug.Log(55555);
        }
    }

}