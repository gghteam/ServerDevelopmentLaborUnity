using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Shootable : MonoBehaviour
{
    [SerializeField] protected float fireVecocity = 20.0f; // 발사 힘
    [SerializeField] protected int   ammoCount    = 31;    // 총알 수
                     protected int   curAmmo;              // 현제 총알 수
    [SerializeField] protected float reloadDelay  = 1.0f;  // 재장전 딜레이
    [SerializeField] protected float fireDelay    = 0.08f; // 750rpm
    [SerializeField] Transform       firepos      = null;  // 발사 위치

    public event System.Action OnFire;

    protected bool bReloading = false;

    protected virtual void Awake()
    {
        curAmmo = ammoCount;

        OnFire += () => { };
    }

    protected virtual void Start()
    {

        InputManager.Instance.OnKeyReload += () => {
            bReloading = true;
            Invoke(nameof(Reload), reloadDelay);
        };
    }

    protected virtual void Shoot()
    {
        --curAmmo; // 총알 하나 줄임

        GameObject bullet = BulletPool.Instance.Get();

        // transform 설정
        bullet.transform.position = firepos.position;
        bullet.transform.rotation = GameManager.Instance.Player.transform.rotation;

        Vector3 vector = firepos.forward * fireVecocity;

        bullet.GetComponent<Bullet>().Fire(vector, () => {
            bullet.SetActive(false);
        });

        OnFire();
    }

    protected virtual bool IsShootable()
    {
        return CheckAmmo() && !bReloading;
    }

    protected virtual bool CheckAmmo()
    {
        return curAmmo > 0;
    }

    protected virtual void Reload()
    {
        curAmmo = ammoCount;
        bReloading = false;
    }

}
