using UnityEngine;

public class Player3d_Planet : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyCodeFire = KeyCode.Space;
    [SerializeField]
    private GameObject projectilePrefab;
    private float moveSpeed = 3;
    private Vector3 lastMoveDirection = Vector3.right;

    public bool IsMoved { set; get; } = true;  // �̵� ���� ����
    public bool IsAttacked { set; get; } = false;   // ���� ���� ����

    [SerializeField] private new Camera camera;

    [SerializeField] private float fov = 0;

    [SerializeField] private float wheelSpeed = 10f;

    [SerializeField] private float roll;
    [SerializeField] private float pitch;
    [SerializeField] private float mouseSpeed = 10f;

    [SerializeField] private float autoTime = 0;

    [SerializeField] private int loveScore_ = 0;

    public int LoveScore
    {
        get
        {
            return loveScore_;
        }
        set
        {
            loveScore_ = value;
        }
    }
    private void Awake()
    {
        fov = camera.fieldOfView;
    }
    private void Update()
    {
        if (IsMoved == true)
        {
            //���콺 ��ũ������ ���� ī�޶� ȭ�� ���ξƿ�ȿ��
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(wheel) > 0)
            {
                Debug.Log("Wheel");
                fov -= wheel * wheelSpeed;
                fov = Mathf.Clamp(fov, 25, 80);//25~80����
                camera.fieldOfView = fov;
            }

            //���콺�� ������ �ִ� ���¿��� �¿�� �������� ȸ���Ѵ�.ī�޶� ȸ���ؾ��Ѵ�.
            if (Input.GetMouseButton(1))
            {
                autoTime = 0;

                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                roll -= mouseY * Time.deltaTime * mouseSpeed;//ī�޶� x�� ȸ��(���Ʒ�ȸ��)
                pitch -= mouseX * Time.deltaTime * mouseSpeed;//ī�޶� �¿� ȸ��

                roll = Mathf.Clamp(roll, -30, 60);//���Ʒ� ȸ�� ����
            }
            else
            {
                autoTime += Time.deltaTime;
            }

            if (pitch >= 360f)
            {
                pitch -= 360f;
            }
            else
            {
                pitch += 360f;
            }

            camera.transform.eulerAngles = new Vector3(roll, pitch, 0);//x��ȸ��,y��ȸ�� �ݿ�
        }

        if (IsAttacked == true)
        {
            // �����̽� Ű�� ���� �߻�ü ����
            if (Input.GetKeyDown(keyCodeFire))
            {
                GameObject clone = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                clone.GetComponent<Projectile>().Setup(lastMoveDirection);
            }
        }
    }
}
