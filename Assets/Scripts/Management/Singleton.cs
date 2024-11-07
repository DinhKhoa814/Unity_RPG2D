using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp Singleton<T> kế thừa từ MonoBehaviour và chỉ cho phép một phiên bản của lớp T (lớp kế thừa Singleton<T>) tồn tại trong suốt vòng đời của ứng dụng.
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // Biến instance lưu trữ một tham chiếu tĩnh duy nhất đến phiên bản của lớp T.
    private static T instance;

    // Thuộc tính tĩnh Instance trả về phiên bản duy nhất của lớp T, có thể truy cập từ bất cứ đâu.
    public static T Instance
    {
        get { return instance; }
    }
    //Phương thức Awake được gọi khi đối tượng được tạo, giúp khởi tạo hoặc hủy đối tượng nếu đã tồn tại một phiên bản.
    protected virtual void Awake()
    {
        // Kiểm tra nếu instance đã tồn tại và đối tượng hiện tại không phải là null,
        // thì hủy đối tượng hiện tại để đảm bảo chỉ có một phiên bản duy nhất.
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Nếu chưa có instance nào, gán đối tượng hiện tại làm instance.
            instance = (T)this;
        }

        // Kiểm tra nếu đối tượng không có cha, tức là không thuộc một đối tượng khác trong Hierarchy,
        // thì sử dụng DontDestroyOnLoad để giữ đối tượng qua các cảnh khác nhau.
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
