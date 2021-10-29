using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform target;
    private Vector3 playerPos;
    public Vector2 maxPos;
    public Vector2 minPos;
    private float cameraSpeed = 0.1f;

    [Header("Start Location Name")]
    public string locName;
    public GameObject text;
    public Text locText;

    [Header("Position Reset")]
    public VectorValue cameraMin;
    public VectorValue cameraMax;


    private void Awake()
    {
        StartCoroutine(PlaceName());
    }

    // Start is called before the first frame update
    void Start()
    {
        maxPos = cameraMax.initValue;
        minPos = cameraMin.initValue;

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != playerPos)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSpeed);
        }
    }

    private IEnumerator PlaceName()
    {

        text.SetActive(true);
        locText.text = locName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
