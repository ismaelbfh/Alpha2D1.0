using System.Collections;
using UnityEditor;
using UnityEngine;

public class ResizeScreen : MonoBehaviour {

    private Object[] Sprites;
    private static Vector2 aspectRatio;

    // Use this for initialization
    void Start()
    {
        aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
        Camera.main.orthographicSize = (1080 * (aspectRatio.y / 9f) / 2) / 100;
        Sprites = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject Sprit in Sprites)
        {
            if (Sprit.GetComponent<SpriteRenderer>() && !Sprit.transform.parent)
            {
                Sprit.transform.localScale = new Vector3(Sprit.transform.localScale.x * (aspectRatio.x / 16f), Sprit.transform.localScale.y * (aspectRatio.y / 9f), Sprit.transform.localScale.z);
                Sprit.transform.position = new Vector3(Sprit.transform.position.x * (aspectRatio.x / 16f), Sprit.transform.position.y * (aspectRatio.y / 9f), Sprit.transform.position.z);
            }
        }
    }

    public static Vector2 getTransVel(Vector2 Velocity)
    {
        aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
        return new Vector2(Velocity.x * (aspectRatio.x / 16f), Velocity.y * (aspectRatio.y / 9f));
    }
}

    public static class AspectRatio
    {
        public static Vector2 GetAspectRatio(int x, int y)
        {
            float f = (float)x / (float)y;
            int i = 0;
            while (true)
            {
                i++;
                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                    break;
            }
            return new Vector2((float)System.Math.Round(f * i, 2), i);
        }
        public static Vector2 GetAspectRatio(Vector2 xy)
        {
            float f = xy.x / xy.y;
            int i = 0;
            while (true)
            {
                i++;
                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                    break;
            }
            return new Vector2((float)System.Math.Round(f * i, 2), i);
        }
        public static Vector2 GetAspectRatio(int x, int y, bool debug)
        {
            float f = (float)x / (float)y;
            int i = 0;
            while (true)
            {
                i++;
                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                    break;
            }
            if (debug)
                Debug.Log("Aspect ratio is " + f * i + ":" + i + " (Resolution: " + x + "x" + y + ")");
            return new Vector2((float)System.Math.Round(f * i, 2), i);
        }
        public static Vector2 GetAspectRatio(Vector2 xy, bool debug)
        {
            float f = xy.x / xy.y;
            int i = 0;
            while (true)
            {
                i++;
                if (System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
                    break;
            }
            if (debug)
                Debug.Log("Aspect ratio is " + f * i + ":" + i + " (Resolution: " + xy.x + "x" + xy.y + ")");
            return new Vector2((float)System.Math.Round(f * i, 2), i);
        }
}
