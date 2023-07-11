using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExplosion : MonoBehaviour
{
    //d��man�n layeri ile temas eden skil ile d��man� dondurmay� sa�layan class
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float radius = 0.5f;
    Enemy enemy;
    public bool colided { get; set; }
    

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (Collider hit in hits)
        {
            if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
            {
                enemy = hit.gameObject.GetComponent<Enemy>();
                colided = true;
            }
            if (colided)
            {
                if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
                {
                    if (enemy != null)
                    {
                       enemy.StartCoroutine("Ice");
                       enabled = false;
                    }

                }

            }
        }
    }

    //nas�l bir geni�li�e sahip oldupunu g�rmek i�in
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
