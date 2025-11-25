using UnityEngine;
using UnityEngine.UI;

public class ExpandButtonManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonData
    {
        public Button button;          // bouton parent
        public RectTransform child;    // enfant à animer
        public Vector2 normalSize = new Vector2(0, 0);
        public Vector2 expandedSize = new Vector2(0, 0);
        public float climbHeight = 30f; 
        [HideInInspector] public bool isExpanded = false;
    }

    public ButtonData[] buttons;
    public float speed = 500f;

    void Start()
    {
        foreach (var b in buttons)
        {
            // assigner listener
            b.button.onClick.AddListener(() => OnButtonClick(b));

            // taille initiale de l’enfant
            b.child.sizeDelta = b.normalSize;

            // pivot bas
            b.child.pivot = new Vector2(0.5f, 0f);
        }
    }

    void Update()
    {
        foreach (var b in buttons)
        {
            // width animée
            float targetWidth = b.isExpanded ? b.expandedSize.x : b.normalSize.x;
            float newWidth = Mathf.MoveTowards(b.child.sizeDelta.x, targetWidth, speed * Time.deltaTime);

            // position Y pour “grimpe”
            float targetY = b.isExpanded ? b.climbHeight : 0f;
            float newY = Mathf.MoveTowards(b.child.anchoredPosition.y, targetY, speed * Time.deltaTime);

            // height fixe
            b.child.sizeDelta = new Vector2(newWidth, b.normalSize.y);
            b.child.anchoredPosition = new Vector2(b.child.anchoredPosition.x, newY);
        }
    }

    void OnButtonClick(ButtonData clicked)
    {
        foreach (var b in buttons)
            b.isExpanded = (b == clicked); // un seul bouton étendu à la fois
    }
}
