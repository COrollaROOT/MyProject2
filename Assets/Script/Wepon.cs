using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;



public class Wepon : MonoBehaviour
{

    public enum Type { Melee }
    public enum Weaponkind { Axe, Hammer }
    public Type type;
    public Weaponkind kind; // 무기 종류 추가

    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailRenderer;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }


    }

    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f); // 1프레임 대기
        meleeArea.enabled = true;
        trailRenderer.enabled = true;

        //2
        yield return new WaitForSeconds(0.3f); // 1프레임 대기
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f); // 1프레임 대기
        trailRenderer.enabled = false;
    }

    // Use() 메인루틴 -> Swing() 서부루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)

    private void OnTriggerEnter(Collider other)
    {
        

        // 자원(ResourceObject)에 데미지 주기
        if (other.CompareTag("Resource"))
        {
            ResourceObject resource = other.GetComponent<ResourceObject>();
            if (resource != null)
            {
                if ((resource.resourceType == ResourceType.Tree && kind == Weaponkind.Axe) ||
                    (resource.resourceType == ResourceType.Rock && kind == Weaponkind.Hammer))
                {
                    Debug.Log("이 무기는 이 자원에 효과가 없습니다.");
                    resource.TakeDamage(damage);
                }
            }
                
        }
    }

}
