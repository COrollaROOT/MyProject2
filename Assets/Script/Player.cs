using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Player : MonoBehaviour
{

    private Rigidbody rigid;

    int currentWeaponIndex = -1;
    public float speed;
    public GameObject[] Weaponns;  // 무기 오브젝트들
    public bool[] hasWeapons;      // 무기 소지 여부
    public GameObject roadPrefab;     // 길 프리팹
    public int roadCost = 3;         // 필요한 돌 개수
    public float buildDistance = 2f;

    [SerializeField] private NavMeshSurface navMeshSurface;

    float hAxis;
    float vAxis;

    bool wDown;
    bool iDown;
    bool fDown;

    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isFireReady;

    Vector3 moveVec;

    Animator anim;

    GameObject nearObject;
    Inventory inventory;

    //float fireDelay;



    void Awake()
    {
        inventory = GetComponent<Inventory>();

        rigid = GetComponent<Rigidbody>();
        rigid.collisionDetectionMode = CollisionDetectionMode.Continuous; // 터널링 방지용
        rigid.interpolation = RigidbodyInterpolation.Interpolate; // 부드러운 움직임
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        anim.ResetTrigger("doSwing");        // 트리거 초기화
        anim.SetInteger("weaponType", -1);   // 무기 없는 상태
        anim.SetBool("IsRun", false);
        anim.SetBool("IsWalk", false);

        // 모든 무기 비활성화
        for (int i = 0; i < Weaponns.Length; i++)
        {
            Weaponns[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        GetInput();
        
        Interration();
        Swap();
        Attack();
        if (Input.GetKeyDown(KeyCode.R))  // R 키를 눌러서 길 만들기
        {
            BuildRoad();
        }

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetKeyDown(KeyCode.E); // 아이템 줍기

        // 숫자 키 입력 수정
        sDown1 = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
        sDown2 = Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
        sDown3 = Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);

        fDown = Input.GetButtonDown("Fire1"); // 공격
    }

    void Attack()
    {
        if (!fDown) return;

        for (int i = 0; i < Weaponns.Length; i++)
        {
            if (Weaponns[i].activeSelf)
            {
                anim.SetTrigger("doSwing");

                Wepon weapon = Weaponns[i].GetComponent<Wepon>();
                weapon.Use();
                break;
            }
        }
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        Vector3 nextVec = moveVec * speed * (wDown ? 0.3f : 1f) * Time.fixedDeltaTime;

        float maxDistance = 0.1f;
        if (nextVec.magnitude > maxDistance)
            nextVec = nextVec.normalized * maxDistance;

        rigid.MovePosition(rigid.position + nextVec);

        anim.SetBool("IsRun", moveVec != Vector3.zero);
        anim.SetBool("IsWalk", wDown);

        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }

    void Interration()
    {
        
        
            if (iDown && nearObject != null)
            {
                var item = nearObject.GetComponent<Item>();
                if (item == null) return;

                switch (item.itemType)
                {
                    case Item.ItemType.Weapon:
                        int weaponIndex = item.value;
                        hasWeapons[weaponIndex] = true;

                        for (int i = 0; i < Weaponns.Length; i++)
                            Weaponns[i].SetActive(i == weaponIndex);

                        anim.SetInteger("weaponType", weaponIndex);

                        inventory.AddItem(item.itemName);  // 무기 이름으로 추가

                        Destroy(nearObject);
                        break;

                    case Item.ItemType.Resource:
                        inventory.AddItem(item.itemName, 1);
                        Destroy(nearObject);
                        break;

                    case Item.ItemType.Coin:
                        inventory.AddItem(item.itemName, item.value);
                        Destroy(nearObject);
                        break;
                }
            }
        }

    void BuildRoad()
    {
        if (currentWeaponIndex != 2 || Weaponns.Length <= 2 || Weaponns[2] == null)
        {
            Debug.Log("삽을 장착해야 합니다.");
            return;
        }

        Wepon weapon = Weaponns[2].GetComponent<Wepon>();
        if (weapon == null || weapon.kind != Wepon.Weaponkind.Shovel)
        {
            Debug.Log("이 무기는 길을 만들수 없습니다 삽을 사용하세요");
            return ;
        }

        if (inventory.HasItem("Stone", 2) && inventory.HasItem("Wood", 1))
        {
            Vector3 buildPos = transform.position + transform.forward * buildDistance;
            GameObject newRoad = Instantiate(roadPrefab, buildPos, Quaternion.identity);

            inventory.UseItem("Stone", 2);  // 돌 2개 사용
            inventory.UseItem("Wood", 1);
            // RoadManager.Instance?.RegisterRoad(buildPos);

            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.Log("재료가 부족합니다! (필요 : 돌 2개, 나무 1개)");
        }
    }

    void Swap()
    {
        int weaponIndex = -1;

        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if (weaponIndex >= 0 && hasWeapons[weaponIndex])
        {

            currentWeaponIndex = weaponIndex;

            for (int i = 0; i < Weaponns.Length; i++)
            {
                Weaponns[i].SetActive(i == weaponIndex);
            }

            // 무기 애니메이션 전환을 위한 파라미터 전달
            anim.SetInteger("weaponType", weaponIndex);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("ResourceItem"))
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("ResourceItem"))
        {
            if (nearObject == other.gameObject)
                nearObject = null;  // 벗어난 오브젝트가 현재 nearObject라면 null로 초기화
        }
    }
}
