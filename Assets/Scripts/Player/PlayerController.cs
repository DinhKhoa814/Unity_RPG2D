using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f; 
    [SerializeField] private float dashSpeed = 4f; 
    [SerializeField] TrailRenderer myTrailRenderer; // Renderer cho hieu ung duong di
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls; // Luu tru cac dieu khien cua nguoi choi
    private Vector2 movement; // Luu tru thong tin di chuyen
    private Rigidbody2D rb; // Rigidbody de quan ly vat ly
    private Animator anim; // Animator de dieu khien hoat anh
    private SpriteRenderer spriteRenderer; // Renderer cho hinh anh
    private KnockBack knockBack;
    private float startingMoveSpeed; // Toc do di chuyen ban dau

    bool facingLeft = false; // Trang thai quay ben trai
    bool isDashing = false; // Trang thai da tan

    protected override void Awake() // Khoi tao du lieu
    {
        base.Awake();
        playerControls = new PlayerControls(); // Khoi tao player controls
        rb = GetComponent<Rigidbody2D>(); // Lay Rigidbody2D
        anim = GetComponent<Animator>(); // Lay Animator
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lay SpriteRenderer
        knockBack = GetComponent<KnockBack>();
    }
    private void Start() // Ham khoi tao sau Awake
    {
        // Dang ky su kien cho hanh dong luot
        playerControls.Combat.Dash.performed += _ => Dash(); // Dang ky su kien tan
        startingMoveSpeed = moveSpeed; // Luu toc do di chuyen ban dau
    }

    private void OnEnable() // Thiet lap trang thai hoac dang ky su kien khi doi tuong kich hoat
    {
        playerControls.Enable(); // Bat cac dieu khien
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Update() // Cap nhat cac thao tac thuc hien moi frame
    {
        PlayerInput(); // Lay thong tin nguoi choi
    }

    private void FixedUpdate() // Cap nhat cac thao tac lien quan den vat ly
    {
        AdjustPlayerFacingDirection(); // Dieu chinh huong quay cua nguoi choi
        Move(); // Di chuyen
    }

    public Transform GetWeaponCollider() // Lay collider vu khi
    {
        return weaponCollider; // Tra ve all thong tin collider
    }

    private void PlayerInput() // Nhan dau vao tu nguoi choi
    {
        // Lay thong tin di chuyen tu dieu khien
        movement = playerControls.Movement.Move.ReadValue<Vector2>(); // Doc gia tri di chuyen
        anim.SetFloat("moveX", movement.x); // Cap nhat tham so hoat anh
        anim.SetFloat("moveY", movement.y); // Cap nhat tham so hoat anh
    }

    private void Move()
    {
        if (knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead) { return; }//dang bi day lui return
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime)); // Di chuyen nguoi choi
    }

    private void AdjustPlayerFacingDirection() // Quay mat theo huong chuot
    {
        Vector3 mousePos = Input.mousePosition; // Lay vi tri chuot tren man hinh
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position); // Chuyen doi vi tri tu world space sang screen space
        if (mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true; // Lat hinh neu chuot o ben trai
            facingLeft = true; // Dat huong quay la trai
        }
        else
        {
            spriteRenderer.flipX = false; // Dat hinh dung neu chuot o ben phai
            facingLeft = false; // Dat huong quay la phai
        }
    }

    void Dash() // Ham thuc hien luot
    {
        if (!isDashing && Stamina.Instance.CurrentStamina > 0) // Kiem tra xem co dang tan khong
        {
            Stamina.Instance.UseStamina();
            isDashing = true; // Dat trang thai la dang luot
            moveSpeed *= dashSpeed; // Tang toc do di chuyen
            myTrailRenderer.emitting = true; // Bat dau hieu ung duong di
            StartCoroutine(EndDashRoutine()); // Bat dau coroutine de ket thuc luot
        }
    }

    IEnumerator EndDashRoutine() // Coroutine de ket thuc luot
    {
        float dashTime = 0.2f; // Thoi gian luot
        float dashCD = 0.25f; // Thoi gian cooldown giua cac lan luot
        yield return new WaitForSeconds(dashTime); // Cho thoi gian luot
        moveSpeed = startingMoveSpeed; // Giam toc do ve binh thuong
        myTrailRenderer.emitting = false; // Ngung hieu ung duong di
        yield return new WaitForSeconds(dashCD); // Cho thoi gian cooldown
        isDashing = false; // Dat trang thai la khong con tan
    }
}
