using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region MemberFields
    [SerializeField]
    private GameObject target;
    [SerializeField, Range(0.0f, 50.0f)]
    private float speed = 10;
    [SerializeField, Range(0f, 50f)]
    private float offset = 5f;
    #endregion MemberFields

    #region MonoBehaviourMethods
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(this.transform.position, new Vector3(target.transform.position.x,
            this.transform.position.y, target.transform.position.z - offset), speed * Time.deltaTime);
    }

    #endregion MonoBehaviourMethods
}
