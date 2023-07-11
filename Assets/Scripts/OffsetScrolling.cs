using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    public float scrollSpeed;
    private Renderer rendererManage;
    private Vector2 savedOffset;

    // Start is called before the first frame update
    void Start()
    {
        rendererManage = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        rendererManage.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
