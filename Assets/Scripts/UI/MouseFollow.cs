using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private void Update() // Hàm này được gọi mỗi frame
    {
        FaceMouse(); // Gọi hàm FaceMouse để làm cho GameObject quay về phía chuột
    }

    void FaceMouse() // Hàm điều chỉnh hướng của GameObject theo vị trí chuột
    {
        Vector3 mousePos = Input.mousePosition; // Lấy vị trí chuột trên màn hình
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Chuyển đổi vị trí chuột sang không gian thế giới
        Vector2 direction = transform.position - mousePos; // Tính toán hướng từ vị trí GameObject đến vị trí chuột
        transform.right = -direction; // Đặt hướng của GameObject quay về phía chuột
    }
}
