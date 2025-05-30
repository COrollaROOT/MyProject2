using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] Weaponns;
    public bool[] hasWeapons;
    float hAxis;
    float vAxis;
    bool wDown;
    bool iDown;

    Vector3 moveVec;

    Animator anim;

    GameObject nearObject;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetKeyDown(KeyCode.E);
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();

        Move();

        Interration();
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("IsRun", moveVec != Vector3.zero);
        anim.SetBool("IsWalk", wDown);

        transform.LookAt(transform.position + moveVec);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")

            nearObject = other.gameObject;


        Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }

    void Interration()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIdex = item.value;
                hasWeapons[weaponIdex] = true;

                Destroy(nearObject);
            }
        }
    }
}
