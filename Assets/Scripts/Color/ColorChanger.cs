using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "red")
        {
            base.gameObject.GetComponent<Collider>().enabled = false;
            target.gameObject.GetComponent<MeshRenderer>().enabled = true;
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            base.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
            Destroy(base.gameObject, .5f);
            print("Game Over!");
            FindObjectOfType<BallHandler>().FailGame();
        }
        else
        {
            base.gameObject.GetComponent<Collider>().enabled = false;
            GameObject gameObject = Instantiate(Resources.Load("splash1")) as GameObject;
            gameObject.transform.parent = target.gameObject.transform;
            Destroy(gameObject, 0.1f);
            target.gameObject.name = "color";
            target.gameObject.tag = "red";
            StartCoroutine(ChangeColor(target.gameObject));
        }
    }

    private IEnumerator ChangeColor(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.gameObject.GetComponent<MeshRenderer>().material.color = BallHandler.oneColor;
        Destroy(base.gameObject);
    }
}
