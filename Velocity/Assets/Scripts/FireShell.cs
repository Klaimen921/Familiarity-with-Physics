using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShell : MonoBehaviour
{
    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;
    float speed = 15;
    float rotSpeed = 5;
    float moveSpeed = 1;
    // Start is called before the first frame update
    void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    float? RotateTurret()
    {
        float? angel = CalculateAngle(true);
        if (angel != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angel, 0f, 0f);
        }
        return angel;
    }
    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude - 1;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngel = sSqr + root;
            float lowAngel = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngel, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngel, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }

    void Update()
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRation, Time.deltaTime * rotSpeed);
        float? angel = RotateTurret();
        if (angel != null)
        {
            CreateBullet();
        }
        else
        {
            this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
    }
}
