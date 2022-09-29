using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotSpeed = 1f;

    private Player player;
    private float currentMoveSpeed;
    private float currentRotSpeed;

    public void Init(Player p)
    {
        player = p;
    }

    void Update()
    {
        LookAtTarget();

        MoveDir();
    }

    void MoveDir()
    {
        currentMoveSpeed = player.state.moveSpeed * Time.deltaTime * moveSpeed;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;

        player.transform.position = player.transform.position + input * currentMoveSpeed;
    }

    void LookAtTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Vector3 target = hit.point - player.transform.position;
            target.y = 0;

            currentRotSpeed = player.state.rotSpeed * Time.deltaTime * rotSpeed;

            player.transform.rotation = 
                Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(target), currentRotSpeed);
        }
    }
}
