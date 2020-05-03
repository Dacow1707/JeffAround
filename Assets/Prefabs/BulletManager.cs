using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    List<OrbitAround> bullets;

    public BulletManager()
    {
        bullets = new List<OrbitAround>();
    }

    public void AddBullet(GameObject bullet)
    {
        // TODO: calc angle
        GameObject newBullet = Instantiate(bullet, new Vector3(0, 0, 1), Quaternion.identity);
        OrbitAround bulletComponent = (OrbitAround)newBullet.GetComponent<OrbitAround>();
        GameObject newBullet2 = Instantiate(bullet, new Vector3(0, 0, 1), Quaternion.identity);
        OrbitAround bulletComponent2 = (OrbitAround)newBullet2.GetComponent<OrbitAround>();
        /*CBACBA
        CBACBA*/
        int index = bullets.Count / 2;
        bullets.Insert(index, bulletComponent);
        bullets.Insert(0, bulletComponent2);

        float angleBetweenBullets = Mathf.PI * 2 / bullets.Count;
        Debug.Log("My Name is jeff");
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].currentAngle = i * angleBetweenBullets;
        }
        /*GameObject newBullet = Instantiate(bullet, new Vector3(0, 0, 1), Quaternion.identity);
        OrbitAround bulletComponent = (OrbitAround)newBullet.GetComponent<OrbitAround>();
        bullets.Add(bulletComponent);
        float angleBetweenBullets = Mathf.PI * 2 / bullets.Count;
        Debug.Log("My Name is jeff");
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].currentAngle = i * angleBetweenBullets;
        }*/
    }
}
