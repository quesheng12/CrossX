using System.Linq;
using UnityEngine;

/// <summary>
/// 画板
/// </summary>
public class Board : MonoBehaviour
{
    [Range(0, 1)]
    public float lerp = 0.05f;
    private Texture2D currentTexture;
    private Vector2 paintPos;

    private bool isDrawing = false;
    private int lastPaintX;
    private int lastPaintY;
    private int painterTipsWidth = 20;
    private int painterTipsHeight = 20;
    private int textureWidth;
    private int textureHeight;

    private Color32[] painterColor;

    private Color32[] currentColor;
    private Color32[] originColor;


    private void Start()
    {
        Texture2D originTexture = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
        textureWidth = originTexture.width;
        textureHeight = originTexture.height;

        currentTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false, true);
        currentTexture.SetPixels32(originTexture.GetPixels32());
        currentTexture.Apply();

        GetComponent<MeshRenderer>().material.mainTexture = currentTexture;

        painterColor = Enumerable.Repeat<Color32>(new Color32(255, 0, 0, 255), painterTipsWidth * painterTipsHeight).ToArray<Color32>();
    }

    private void LateUpdate()
    {
        int texPosX = (int)(paintPos.x * (float)textureWidth - (float)(painterTipsWidth / 2));
        int texPosY = (int)(paintPos.y * (float)textureHeight - (float)(painterTipsHeight / 2));
        // Debug.Log("LateUpdate: texPosY " + (textureHeight - texPosY) + " textureHeight " + textureHeight + " paintPos " + paintPos.y);
        if (isDrawing)
        {
            //Change the Pixel under the pen
            currentTexture.SetPixels32(textureWidth - texPosX, textureHeight - texPosY, painterTipsWidth, painterTipsHeight, painterColor);
            //Using lerp to make the track continue
            if (lastPaintX != 0 && lastPaintY != 0)
            {
                int lerpCount = (int)(1 / lerp);
                for (int i = 0; i <= lerpCount; i++)
                {
                    int x = (int)Mathf.Lerp((float)lastPaintX, (float)texPosX, lerp);
                    int y = (int)Mathf.Lerp((float)lastPaintY, (float)texPosY, lerp);
                    currentTexture.SetPixels32(textureWidth - x, textureHeight - y, painterTipsWidth, painterTipsHeight, painterColor);
                }
            }
            currentTexture.Apply();
            lastPaintX = texPosX;
            lastPaintY = texPosY;
        }
        else
        {
            lastPaintX = lastPaintY = 0;
        }

    }

    public void SetPainterPositon(float x, float y)
    {
        // Debug.Log("SetPainterPosition: x" + x + " y" + y);
        paintPos.Set(x, y);
    }

    public bool IsDrawing
    {
        get
        {
            return isDrawing;
        }
        set
        {
            isDrawing = value;
        }
    }

    public void SetPainterColor(Color32 color)
    {
        if (!IsEqual(color, painterColor[0]))
        {
            for (int i = 0; i < painterColor.Length; i++)
            {
                painterColor[i] = color;
            }
        }
    }

    public static bool IsEqual(Color32 origin, Color32 compare)
    {
        if (origin.g == compare.g && origin.r == compare.r)
        {
            if (origin.a == compare.a && origin.b == compare.b)
            {
                return true;
            }
        }
        return false;
    }
}
