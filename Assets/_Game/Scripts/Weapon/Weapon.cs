using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform TF;
    [SerializeField] protected EWeaponType eWeaponType;
    [SerializeField] protected Bullet bulletPrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] protected bool isActivate = true;

    public WeaponDatas weaponDatas;
    public Vector3 offsetRotation;
    public WeaponDataa weaponDataa;
    public Character character;
    public int indexMat;
    public Bullet bullet;

    void Start()
    {
        indexMat = Random.Range(0, 3);
    }



    public void InitData(int eType, int indexMaterial)
    {
        this.indexMat = indexMaterial;
        if (character != null && weaponDataa != null)
        {
            eWeaponType = character.currentWeaponType;
            weaponDataa.SetEWeaponType((int)eWeaponType);
            weaponDataa.SetMaterial(this.indexMat);
            meshRenderer.materials = weaponDataa.GetMaterial().ToArray();
        }
        else
        {
            eWeaponType = (EWeaponType)eType;
            weaponDataa?.SetEWeaponType((int)eWeaponType);
            weaponDataa?.SetMaterial(this.indexMat);
            meshRenderer.materials = weaponDataa.GetMaterial().ToArray();
        }

    }

    public virtual void Attack()

    {
        bulletPrefab = weaponDatas.GetBulletPrefab(eWeaponType);
        character.dirAttact.y = 0;
        bullet = Instantiate(bulletPrefab, TF.position, Quaternion.identity);
        bullet.meshRenderer.materials = weaponDataa.GetMaterial().ToArray();
        bullet.TF.localScale = character.TF.localScale;
        bullet.character = character;
        bullet.Move(character.dirAttact);
    }

    public void OnDespawn()
    {
        Destroy(gameObject); // fix late
    }
}
