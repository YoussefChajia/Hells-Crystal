using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    [Header("Camera")]
    private Camera mainCamera;
    private Vector2 screenBounds;
    private Vector3 lastScreenPosition;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        lastScreenPosition = transform.position;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in sprites)
        {
            loadChilds(obj);
        }
    }

    void loadChilds(GameObject obj)
    {
        float objHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
        int childs = (int)Mathf.Ceil(screenBounds.y * 2 / objHeight);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childs; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objHeight * i * 0.99f, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChilds(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjHeight = lastChild.GetComponent<SpriteRenderer>().bounds.extents.y;
            if (transform.position.y + screenBounds.y > lastChild.transform.position.y + halfObjHeight)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y + halfObjHeight * 1.99f, lastChild.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < firstChild.transform.position.y - halfObjHeight)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x, firstChild.transform.position.y - halfObjHeight * 1.99f, firstChild.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        foreach (GameObject obj in sprites)
        {
            float parallaxSpeed = 1 - Mathf.Clamp01(Mathf.Abs(transform.position.z / obj.transform.position.z));
            float difference = transform.position.y - lastScreenPosition.y;
            obj.transform.Translate(Vector3.up * difference * parallaxSpeed);
            repositionChilds(obj);
        }
        lastScreenPosition = transform.position;
    }
}
