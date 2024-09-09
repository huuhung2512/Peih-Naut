using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionCulling : MonoBehaviour
{
    [System.Serializable] public class ObjectSettings
    {
        [HideInInspector] public string title;
        public GameObject theGameObject;

        public Vector2 size = Vector2.one;
        public Vector2 offset = Vector2.zero;
        public bool multiplySizeByTransformScale = true;

        public Vector2 sized { get; set; }
        public Vector2 center { get; set; }
        public Vector2 TopRight { get; set; }
        public Vector2 TopLeft { get; set; }
        public Vector2 BottomLeft { get; set; }
        public Vector2 BottomRight { get; set; }
        public float right { get; set; }
        public float left { get; set; }
        public float top { get; set; }
        public float bottom { get; set; }

        public Color DrawColor = Color.white;
        public bool showBorders = true;
    }

    public ObjectSettings[] objectSettings; // Mảng lưu trữ các thiết lập đối tượng con

    private new Camera camera;
    private float cameraHalfWidth;

    public float updateRateInSeconds = 0.1f;

    private float timer;

    // Biến để lưu GameObject cha (trong trường hợp bạn có đối tượng cha)
    public GameObject parentObject;

    void Awake()
    {
        camera = GetComponent<Camera>();
        cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / (float)Screen.height);

        // Lấy tất cả các đối tượng con và khởi tạo objectSettings
        AddAllChildObjectsToSettings(parentObject);

        // Khởi tạo các thông số của các đối tượng
        InitializeObjectSettings();
    }

    // Hàm lấy tất cả các đối tượng con và thêm vào objectSettings
    void AddAllChildObjectsToSettings(GameObject parent)
    {
        // Lấy số lượng đối tượng con
        int childCount = parent.transform.childCount;

        // Khởi tạo mảng objectSettings với số lượng bằng số đối tượng con
        objectSettings = new ObjectSettings[childCount];

        // Lặp qua tất cả các đối tượng con
        for (int i = 0; i < childCount; i++)
        {
            // Lấy đối tượng con tại vị trí i
            GameObject child = parent.transform.GetChild(i).gameObject;

            // Tạo mới một ObjectSettings và gán đối tượng con
            ObjectSettings newObjectSetting = new ObjectSettings
            {
                theGameObject = child,
                title = child.name
            };

            // Lưu ObjectSettings vào mảng
            objectSettings[i] = newObjectSetting;
        }
    }

    // Khởi tạo các thông số của đối tượng
    void InitializeObjectSettings()
    {
        foreach (ObjectSettings o in objectSettings)
        {
            o.sized = o.size * (o.multiplySizeByTransformScale ? new Vector2(Mathf.Abs(o.theGameObject.transform.localScale.x), Mathf.Abs(o.theGameObject.transform.localScale.y)) : Vector2.one);
            o.center = (Vector2)o.theGameObject.transform.position + o.offset;

            o.TopRight = new Vector2(o.center.x + o.sized.x, o.center.y + o.sized.y);
            o.TopLeft = new Vector2(o.center.x - o.sized.x, o.center.y + o.sized.y);
            o.BottomLeft = new Vector2(o.center.x - o.sized.x, o.center.y - o.sized.y);
            o.BottomRight = new Vector2(o.center.x + o.sized.x, o.center.y - o.sized.y);

            o.right = o.center.x + o.sized.x;
            o.left = o.center.x - o.sized.x;
            o.top = o.center.y + o.sized.y;
            o.bottom = o.center.y - o.sized.y;
        }
    }

    void OnDrawGizmosSelected()
    {
        foreach (ObjectSettings o in objectSettings)
        {
            if (o.theGameObject)
            {
                o.title = o.theGameObject.name;

                if (o.showBorders)
                {
                    o.TopRight = new Vector2(o.center.x + o.sized.x, o.center.y + o.sized.y);
                    o.TopLeft = new Vector2(o.center.x - o.sized.x, o.center.y + o.sized.y);
                    o.BottomLeft = new Vector2(o.center.x - o.sized.x, o.center.y - o.sized.y);
                    o.BottomRight = new Vector2(o.center.x + o.sized.x, o.center.y - o.sized.y);
                    Gizmos.color = o.DrawColor;
                    Gizmos.DrawLine(o.TopRight, o.TopLeft);
                    Gizmos.DrawLine(o.TopLeft, o.BottomLeft);
                    Gizmos.DrawLine(o.BottomLeft, o.BottomRight);
                    Gizmos.DrawLine(o.BottomRight, o.TopRight);
                }
            }
        }
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > updateRateInSeconds) timer = 0;
        else return;

        float cameraRight = camera.transform.position.x + cameraHalfWidth;
        float cameraLeft = camera.transform.position.x - cameraHalfWidth;
        float cameraTop = camera.transform.position.y + camera.orthographicSize;
        float cameraBottom = camera.transform.position.y - camera.orthographicSize;

        foreach (ObjectSettings o in objectSettings)
        {
            if (o.theGameObject)
            {
                bool IsObjectVisibleInCastingCamera = o.right > cameraLeft & o.left < cameraRight & // check horizontal
                                                      o.top > cameraBottom & o.bottom < cameraTop; // check vertical
                o.theGameObject.SetActive(IsObjectVisibleInCastingCamera);
            }
        }
    }
}
