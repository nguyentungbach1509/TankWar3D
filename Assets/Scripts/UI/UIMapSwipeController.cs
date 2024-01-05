using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMapSwipeController : MonoBehaviour
    {
        [SerializeField] private GameObject scrollBar;
        float scrollBarValue;
        Scrollbar scroll;
        float[] scrollPos;
        public int MapIndex { get; set; }

        private void Start()
        {
            scroll = scrollBar.GetComponent<Scrollbar>();
        }

        // Update is called once per frame
        void Update()
        {
            scrollPos = new float[transform.childCount];
            float distance = 1f / scrollPos.Length + ((1f / scrollPos.Length) / (scrollPos.Length - 1));
            Debug.Log(distance);
            for (int i = 0; i < scrollPos.Length; i++)
            {
                scrollPos[i] = distance * i;
                Debug.Log(scrollPos[i]);
            }

            if (Input.GetMouseButton(0))
            {
                scrollBarValue = scroll.value;
            }
            else
            {
                for (int i = 0; i < scrollPos.Length; i++)
                {
                    if (scrollBarValue < scrollPos[i] + (distance / 2) &&
                        scrollBarValue > scrollPos[i] - (distance / 2))
                    {
                        scroll.value = Mathf.Lerp(scroll.value, scrollPos[i], 0.1f);
                    }
                }
            }

            for (int i = 0; i < scrollPos.Length; i++)
            {
                if (scrollBarValue < scrollPos[i] + (distance / 2) &&
                    scrollBarValue > scrollPos[i] - (distance / 2))
                {
                    transform.GetChild(i).localScale =
                        Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1, 1), 0.1f);
                    MapIndex = int.Parse(transform.GetChild(i).name);
                    for (int j = 0; j < scrollPos.Length; j++)
                    {
                        if (j != i)
                        {
                            transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale,
                                new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }
    }
}