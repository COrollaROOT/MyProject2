using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed;
    public GameObject[] Weaponns;  // ���� ������Ʈ��
    public bool[] hasWeapons;      // ���� ���� ����

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

    float fireDelay;

    

    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        anim.ResetTrigger("doSwing");        // Ʈ���� �ʱ�ȭ
        anim.SetInteger("weaponType", -1);   // ���� ���� ����
        anim.SetBool("IsRun", false);
        anim.SetBool("IsWalk", false);

        // ��� ���� ��Ȱ��ȭ
        for (int i = 0; i < Weaponns.Length; i++)
        {
            Weaponns[i].SetActive(false);
        }
    }

    void Update()
    {
        GetInput();
        Move();
        Interration();
        Swap();
        Attack();

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetKeyDown(KeyCode.E);

        // ���� Ű �Է� ����
        sDown1 = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
        sDown2 = Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);
        sDown3 = Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);

        fDown = Input.GetButtonDown("Fire1");
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

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

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

                        inventory.AddItem(item.itemName);  // ���� �̸����� �߰�

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

    

    void Swap()
    {
        int weaponIndex = -1;

        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if (weaponIndex >= 0 && hasWeapons[weaponIndex])
        {
            for (int i = 0; i < Weaponns.Length; i++)
            {
                Weaponns[i].SetActive(i == weaponIndex);
            }

            // ���� �ִϸ��̼� ��ȯ�� ���� �Ķ���� ����
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
                nearObject = null;  // ��� ������Ʈ�� ���� nearObject��� null�� �ʱ�ȭ
        }
    }
}
