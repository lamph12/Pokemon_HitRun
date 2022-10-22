using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_left : MonoBehaviour
{
    Rigidbody rg;
   
    private void OnTriggerEnter(Collider other)
    {
       
        rg = other.GetComponent<Rigidbody>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("da nhay");
            PlayerManager.PlayerManagerIstance.thewall = false;
            PlayerManager.PlayerManagerIstance.therotation = true;
            other.gameObject.transform.rotation = Quaternion.Slerp(other.transform.rotation, Quaternion.Euler(0, 0, 0), 50 * Time.deltaTime);
            rg.velocity = new Vector3(4.5f, -0.5f, -7f);
            PlayerManager.PlayerManagerIstance.anim.SetBool("IsJumpWall", true);
            PlayerManager.PlayerManagerIstance.anim.SetBool("IsRun", false);
            rg.isKinematic = false;
            Cameractl.CameractlIstance.rotationcamera = false;

        }

    }
}
