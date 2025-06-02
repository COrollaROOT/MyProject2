using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;



public class Wepon : MonoBehaviour
{

    public enum Type { Melee }
    public enum Weaponkind { Axe, Hammer }
    public Type type;
    public Weaponkind kind; // ���� ���� �߰�

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
        yield return new WaitForSeconds(0.1f); // 1������ ���
        meleeArea.enabled = true;
        trailRenderer.enabled = true;

        //2
        yield return new WaitForSeconds(0.3f); // 1������ ���
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f); // 1������ ���
        trailRenderer.enabled = false;
    }

    // Use() ���η�ƾ -> Swing() ���η�ƾ -> Use() ���η�ƾ
    // Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)

    private void OnTriggerEnter(Collider other)
    {
        

        // �ڿ�(ResourceObject)�� ������ �ֱ�
        if (other.CompareTag("Resource"))
        {
            ResourceObject resource = other.GetComponent<ResourceObject>();
            if (resource != null)
            {
                if ((resource.resourceType == ResourceType.Tree && kind == Weaponkind.Axe) ||
                    (resource.resourceType == ResourceType.Rock && kind == Weaponkind.Hammer))
                {
                    Debug.Log("�� ����� �� �ڿ��� ȿ���� �����ϴ�.");
                    resource.TakeDamage(damage);
                }
            }
                
        }
    }

}
