using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    private Player player;
    private JoyStick joyStick;
    private TileMaker tileMaker;

    private void Awake()
    {
        Instance = this;

        player = GameObject.FindObjectOfType<Player>();
        joyStick = GameObject.FindObjectOfType<JoyStick>();
        tileMaker = GameObject.FindObjectOfType<TileMaker>();
    }

    private void Start()
    {
        joyStick.SetTarget(player.transform);
        tileMaker.SetTarget(player.transform);
//#if UNITY_EDITOR
//        player.gameObject.AddComponent<PlayerMoveController>().Init(player);
//        joyStick.gameObject.SetActive(false);
//#elif UNITY_ANDROID
//        joyStick.SetTarget(player.transform);
//#endif
    }
}