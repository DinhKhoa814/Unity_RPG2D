using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // hieu ung parallax tao do sau
    [SerializeField]float parallaxoffset = -0.15f;
    Camera cam;
    Vector2 startPos;
    Vector2 travel => (Vector2)cam.transform.position - startPos;//=> tinh bieu thuc ben phai va tra ve gia tri

    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxoffset;
    }

}
