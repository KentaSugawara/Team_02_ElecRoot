using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    private RectTransform anc;
    private float _anc;
    private float _sca;

    private AudioSource souce;

    [SerializeField]
    private AudioClip Title_BGM;
    [SerializeField]
    private AudioClip Menu_BGM;

    [SerializeField]
    private GameObject[] tit;
    [SerializeField]
    private GameObject[] clearstage;
    [SerializeField]
    private GameObject circle;

    [SerializeField]
    private int title_objnum;
    [SerializeField]
    private float drain;

    public bool[] stage_clear;

    void Start()
    {
        souce = GetComponent<AudioSource>();
        souce.clip = Title_BGM;
        souce.Play();
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
                StartCoroutine(Audio());
                yield return StartCoroutine(zoomout());
                break;
            }
            yield return null;
        }
    }

    private IEnumerator Audio()
    {
        var title = true;
        while (true)
        {
            var time = Time.deltaTime;
            souce = GetComponent<AudioSource>();
            if(title)souce.volume -= time;
            if (souce.volume <= 0)
            {
                souce.clip = Menu_BGM;
                souce.Play();
                souce.volume = 0.0f;
                title = false;
            }
            if (!title && anc.pivot.x >= 1.0f) { souce.volume += time / 3; }
            if (!title && souce.volume >= 1) { break; }
            yield return null;
        }
    }

    private IEnumerator zoomout()
    {
        var start_pos = circle.transform.position;
        float t = 0;

        while (true)
        {
            for (int i = 0; i < title_objnum; i++)
            {
                tit[i].SetActive(false);
            }
            var time = Time.deltaTime;
            anc.pivot += new Vector2(time * _anc / _sca, 0);
            anc.localScale -= new Vector3(time, time, 0);
            gameObject.GetComponent<RectTransform>().pivot = anc.pivot;
            gameObject.GetComponent<RectTransform>().localScale = anc.localScale;
            circle.transform.position = Vector3.Lerp(start_pos, start_pos + new Vector3(Screen.width / 2, 0.0f, 0.0f), t);
            t += time;

            if (anc.pivot.x >= 1.0f)
            {
                tit[title_objnum].SetActive(true);

                for (int i = title_objnum; i < tit.Length; i++)
                {
                    if (stage_clear[i - title_objnum])
                    {
                        clearstage[i - title_objnum].SetActive(true);
                        tit[i].SetActive(true);
                    }
                }
                anc.pivot = new Vector2(1.0f, 1.0f);
                anc.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                gameObject.GetComponent<RectTransform>().pivot = anc.pivot;
                gameObject.GetComponent<RectTransform>().localScale = anc.localScale;
                circle.SetActive(false);
                break;
            }
            yield return null;
        }
    }
}
