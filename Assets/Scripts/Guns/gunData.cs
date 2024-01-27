using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject{
    public float fireRate;
    public float bulletSpeed;
    public int mag;
    public float reloadSpeed;
    public int projectileCount;
    public float spread;
}