using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    private RectTransform anc;
    private float _anc;
    private float _sca;

    [SerializeField]
    private GameObject[] tit;

    void Start()
    {
        anc = gameObject.GetComponent<RectTransform>();
        _anc = 1.0f - anc.pivot.x;
        _sca = anc.localScale.x - 1.0f;
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                yield return zoomout();
                break;
            }
            yield return null;
        }
    }

    private IEnumerator zoomout()
    {
        while (true)
        {
            for(int i = 0; i < 2; i++)
            {
                tit[i].SetActive(false);
            }
            var time = Time.deltaTime;
            anc.pivot += new Vector2(time * _anc / _sca, 0);
            anc.localScale -= new Vector3(time, time, 0);
            gameObject.GetComponent<RectTransform>().pivot = anc.pivot;
            gameObject.GetComponent<RectTransform>().localScale = anc.localScale;

            if (anc.pivot.x >= 1.0f)
            {
                for(int i = 2; i < tit.Length; i++)
                {
                    tit[i].SetActive(true);
                }
                anc.pivot = new Vector2(1.0f, 1.0f);
                anc.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                gameObject.GetComponent<RectTransform>().pivot = anc.pivot;
                gameObject.GetComponent<RectTransform>().localScale = anc.localScale;
                break;
            }
            yield return null;
        }
    }
}
