using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickUp : MonoBehaviour
{
    private enum PickUpType{
        GoldCoin,
        StaminaGlobe,
        HealthGlobe
    }

    [SerializeField]private PickUpType pickUpType;
    // Khoảng cách để đối tượng bắt đầu di chuyển về phía người chơi
    [SerializeField] float pickUpDistance = 5f;
    // Tốc độ tăng tốc của đối tượng khi di chuyển
    [SerializeField] float accelartionRate = 0.2f;
    // Tốc độ di chuyển ban đầu của đối tượng
    [SerializeField] float moveSpeed = 3f;
    // AnimationCurve để điều chỉnh cách đối tượng xuất hiện
    [SerializeField] AnimationCurve animationCurve;
    // Độ cao tối đa mà đối tượng đạt được khi xuất hiện
    [SerializeField] float heightY = 1.5f;
    // Thời gian để đối tượng hoàn thành chuyển động xuất hiện
    [SerializeField] float popDuration = 1f;

    // Hướng di chuyển hiện tại của đối tượng
    Vector3 movDir;
    // Tham chiếu tới thành phần Rigidbody2D để điều khiển vật lý
    Rigidbody2D rb;

    private void Awake()
    {
        // Gán thành phần Rigidbody2D từ đối tượng này
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Bắt đầu Coroutine để điều khiển chuyển động xuất hiện
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        // Lấy vị trí của người chơi
        Vector3 playerPos = PlayerController.Instance.transform.position;
        // Kiểm tra nếu người chơi nằm trong khoảng cách pick-up
        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            // Tính hướng di chuyển về phía người chơi và chuẩn hóa nó
            movDir = (playerPos - transform.position).normalized;
            // Tăng tốc độ di chuyển
            moveSpeed += accelartionRate;
        }
        else
        {
            // Nếu không trong phạm vi, đối tượng không di chuyển
            movDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        // Cập nhật vận tốc của đối tượng dựa trên hướng và tốc độ hiện tại
        rb.velocity = movDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đối tượng va chạm với người chơi, phá hủy đối tượng này
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    // Coroutine để điều khiển cách đối tượng xuất hiện một cách sinh động
    IEnumerator AnimCurveSpawnRoutine()
    {
        // Điểm bắt đầu là vị trí ban đầu của đối tượng
        Vector2 startPoint = transform.position;
        // Tạo điểm kết thúc ngẫu nhiên trong khoảng xác định
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        Vector2 endPoint = new Vector2(randomX, randomY);

        // Biến để theo dõi thời gian đã trôi qua
        float timePassed = 0f;

        // Vòng lặp chạy đến khi thời gian đạt popDuration
        while (timePassed < popDuration)
        {
            // Tăng thời gian trôi qua
            timePassed += Time.deltaTime;
            // Tính toán giá trị tỉ lệ tuyến tính
            float linearT = timePassed / popDuration;
            // Đánh giá đường cong animation để lấy hệ số điều chỉnh chiều cao
            float heightT = animationCurve.Evaluate(linearT);
            // Tính toán chiều cao hiện tại dựa trên giá trị từ đường cong
            float height = Mathf.Lerp(0f, heightY, heightT);

            // Cập nhật vị trí đối tượng theo đường cong và chiều cao
            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            // Đợi đến frame tiếp theo
            yield return null;
        }
    }
    private void DetectPickupType()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealthPlayer();
                break;
            case PickUpType.StaminaGlobe:
                Stamina.Instance.RefreshStamina();
                break;
        }
    }
}
