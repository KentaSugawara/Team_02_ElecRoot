using System.Collections;
using UnityEngine;
using System;
using live2d;

[ExecuteInEditMode]
public class EnemyModel : MonoBehaviour
{

    [SerializeField]
    private TextAsset mocFile;
    [SerializeField]
    private Texture2D[] textureFiles;

    [SerializeField]
    private TextAsset[] motionFiles;

    private Live2DMotion[] motions;

    private Live2DModelUnity live2DModel;
    private Matrix4x4 live2DCanvasPos;
    private MotionQueueManager motionMgr;

    public enum EnemyState
    {
        Wait,
        Walk,
        Attack,
        Stop
    }

    private EnemyState _State = EnemyState.Wait;
    public EnemyState State
    {
        get { return _State; }
        set { _State = value; }
    }

    void Start()
    {
        motions = new Live2DMotion[motionFiles.Length];

        if (live2DModel != null) return;
        Live2D.init();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        live2DModel.setRenderMode(Live2D.L2D_RENDER_DRAW_MESH);

        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);

        for (int i = 0; i < motionFiles.Length; i++)
        {
            motions[i] = Live2DMotion.loadMotion(motionFiles[i].bytes);
        }

        motions[0].setLoop(true);
        motions[0].setLoopFadeIn(false);

        motionMgr = new MotionQueueManager();

        StartCoroutine(Routine_Main());
    }

    private EnemyState? main = null;

    private IEnumerator Routine_Main()
    {
        while(true)
        {
            while (true)
            {
                if (live2DModel == null) break;
                live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

                if (!Application.isPlaying)
                {
                    live2DModel.update();
                    break;
                }

                if (motionMgr.isFinished() || main != _State)
                {
                    //歩き
                    if (_State == EnemyState.Walk)
                    {
                        motionMgr.startMotion(motions[0]);
                    }
                }

                if (main != _State)
                {
                    //噛みつき
                    if (_State == EnemyState.Attack)
                    {
                        motionMgr.startMotion(motions[1]);
                    }
                    //怯み
                    else if (_State == EnemyState.Stop)
                    {
                        motionMgr.startMotion(motions[2]);
                    }
                    //停止
                    if (_State == EnemyState.Wait)
                    {
                        motionMgr.startMotion(motions[3]);
                    }
                }

                main = _State;

                motionMgr.updateParam(live2DModel);
                live2DModel.update();
                live2DModel.draw();

                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator ColorRoutine;
    public void SetColor(Color color, float Seconds)
    {
        if (ColorRoutine != null) StopCoroutine(ColorRoutine);
        ColorRoutine = Routine_Color(color, Seconds);
        StartCoroutine(ColorRoutine);
    }

    private IEnumerator Routine_Color(Color color, float Seconds)
    {
        live2DModel.setTextureColor(0, color.r, color.g, color.b);
        yield return new WaitForSeconds(Seconds);
        live2DModel.setTextureColor(0, 1.0f, 1.0f, 1.0f);
    }
}