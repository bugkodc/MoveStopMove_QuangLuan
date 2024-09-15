using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float touchZoomSpeed = 0.1f;  // Tốc độ zoom khi dùng cảm ứng
    public float zoomSpeed = 5f;   // Tốc độ zoom khi dùng chuột
    public float rotationSpeed = 0.5f;       // Tốc độ cuộn khi dùng một ngón tay
    public float minZoom = 20f;          // Độ zoom tối thiểu (FOV)
    public float maxZoom = 60f;
    private Camera cam;
    private Vector3 lastTouchPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // Tính toán sự thay đổi vị trí của ngón tay
                Vector2 deltaPosition = (Vector3)touch.position - lastTouchPosition;

                // Xoay camera dựa trên sự di chuyển của ngón tay
                float rotationX = deltaPosition.y * rotationSpeed;
                float rotationY = deltaPosition.x * rotationSpeed;

                // Xoay camera theo trục X và Y, trục Z giữ nguyên
                cam.transform.eulerAngles += new Vector3(-rotationX, rotationY, 0);

                // Cập nhật vị trí cuối cùng của ngón tay
                lastTouchPosition = touch.position;
            }
        }
        if (Input.touchCount == 2)
        {
            // Lấy vị trí hai ngón tay
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Tính khoảng cách giữa hai ngón tay trong khung hình trước
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            // Tính khoảng cách trước và sau giữa hai ngón tay
            float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;

            // Sự khác biệt khoảng cách (độ zoom)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Điều chỉnh giá trị field of view của camera
            cam.fieldOfView += deltaMagnitudeDiff * zoomSpeed;

            // Đảm bảo giá trị field of view không vượt ngoài phạm vi
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }
}
