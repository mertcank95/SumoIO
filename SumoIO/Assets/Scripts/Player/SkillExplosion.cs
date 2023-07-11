using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExplosion : MonoBehaviour
{
    //düþmanýn layeri ile temas eden skil ile düþmaný dondurmayý saðlayan class
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

    //nasýl bir geniþliðe sahip oldupunu görmek için
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
