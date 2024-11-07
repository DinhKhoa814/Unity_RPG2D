using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] AnimationCurve animCurve;
    [SerializeField] float heightY = 3f;
    [SerializeField] GameObject grapeProjectileShadow;
    [SerializeField] GameObject splatterPrefab;
    private void Start()
    {
        GameObject grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3 (0,-0.3f,0),Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPos = grapeShadow.transform.position;
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MovGrapeShadowRoutine(grapeShadow,grapeShadowStartPos,playerPos));
    }
    IEnumerator ProjectileCurveRoutine(Vector3 startPos,Vector3 endPos)
    {
        float timePassed = 0f;
        while (timePassed < duration) 
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f,height);
            yield return null;
        }
        Instantiate(splatterPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    IEnumerator MovGrapeShadowRoutine(GameObject grapeShadow,Vector3 startPos,Vector3 endPos)
    {
        float timePassed = 0f;
        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            grapeShadow.transform.position = Vector2.Lerp(startPos, endPos, linearT);
            yield return null;
        }
        Destroy(grapeShadow);
    }
}
