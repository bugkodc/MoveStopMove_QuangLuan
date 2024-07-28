using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKinfe : Weapon
{
    [SerializeField] private Transform[] bulletpoints;

    public override void Attack()
    {
        eWeaponType = character.currentWeaponType;
        bulletPrefab = weaponDatas.GetBulletPrefab(eWeaponType);
        Bullet[] bullets = new Bullet[3];
        character.dirAttact.y = 0;
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = SimplePool.Spawn<Bullet>(bulletPrefab, character.weaponGenTF.position + new Vector3((i - 1) * 0.5f, 0, 0), Quaternion.identity /* bulletPrefab.transform.rotation*/ /*TF.rotation*Quaternion.Euler(0f, (i-1)*10f, 0f) */ /*Quaternion.identity*/);
            bullets[i].tf.localScale = character.TF.localScale;
            bullets[i].meshRenderer.materials = weaponDataa.GetMaterial().ToArray();
            bullets[i].character = character;
            bullets[i].Move(character.dirAttact + new Vector3(0, 0, (i - 1) * 0.2f));
        }
        isActivate = (!bullets[0].IsDead && !bullets[1].IsDead && !bullets[2].IsDead);
    }


}
