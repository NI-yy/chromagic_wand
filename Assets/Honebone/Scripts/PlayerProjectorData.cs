using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "p_pjtor", menuName = "ScriptableObject/ProjectorData_P")]


public class PlayerProjectorData : ScriptableObject
{
    public GameObject projectile;

    public AudioClip SE_GengeratePjtor;
    public AudioClip SE_Fire;
    //[Header("projectorがプレイヤーめがけて発射するか")] public bool targetPlayer = true;
    //[Header("発射回数が1以上の時に参照 プレイヤーの現在の位置を対象とするか")] public bool refreshPlayerPos = false;

    //[Header("プレイヤーを追尾するか/方向転換速度")] public float followPlayerSpeed;
    //[Header("現在のプレイヤーの位置を追尾するか falseなら発射時の場所へ")] public bool followCurrentPlayer;

    [Header("一回の発射で射出する弾数")] public int pellets = 1;
    [Header("ランダムな方向に発射するか")] public bool fireRandomly;
    [Header("+-(spread/2)°のブレが生じる")] public float spread;
    [Header("spread上に等間隔に発射するか")] public bool equidistant;

    [Header("発射回数")] public float fireRounds = 1;
    [Header("発射回数が2以上の時に参照 1発発射するごとのインターバル[s] 0なら同時発射")] public float fireRate;

    //[Header("回転しないか")]
    //public bool lockRotation;
    [Header("min～maxの間でランダムに決まる")]
    public float projectileSpeed_min = 10f;
    public float projectileSpeed_max = 10f;
    public float projectileDuration = 1f;
}
