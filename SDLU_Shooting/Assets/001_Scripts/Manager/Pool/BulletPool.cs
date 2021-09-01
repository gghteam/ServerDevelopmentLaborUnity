using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet Pool Manager
// Should be added on every Guns=>Fireposition

public class BulletPool : MonoBehaviour
{
    static public BulletPool inst = null; // for static functions

    [SerializeField] private GameObject bulletPrefab = null;

    [Header("Bulelt instance count")]
    [SerializeField] private int instnaceCount = 31; // common mag bullet count

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        for(int i = 0; i < instnaceCount; ++i)
        {
            pool.Add(MakeBullet());
        }
    }

    ///<summary>
    /// Instanciates bullet
    ///</summary>
    /// <returns>Instance of bullet based bulletPrefab</returns>
    private GameObject MakeBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, GameManager.instance.player.rotation, transform);
        
        bullet.SetActive(false);

        return bullet;
    }


    ///<summary>
    /// Returns bullet object
    ///</summary>
    static public GameObject Get()
    {
        // This code finds not activated gameobject. When null, it means there is no Object passes the condition.
        GameObject bullet = inst.pool.Find(x => !x.activeSelf);

        if(bullet == null) // when every bullets in pool are active.
        {
            bullet = inst.MakeBullet();
            inst.pool.Add(bullet);
        }

        bullet.SetActive(true);

        bullet.transform.rotation = Quaternion.Euler(GameManager.instance.player.rotation.eulerAngles.x + 90.0f, // else, bullet will look up not front.
                                                     GameManager.instance.player.rotation.eulerAngles.y,
                                                     GameManager.instance.player.rotation.eulerAngles.z);

        return bullet;
    }


}
