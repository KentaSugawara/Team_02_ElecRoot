﻿using UnityEngine;
using System;
using live2d;

[ExecuteInEditMode]
public class EnemyModel_2 : MonoBehaviour
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

    public enum Enemy2State
    {
        Wait,
        Walk,
        Attack,
        Stop
    }

    private Enemy2State _State2 = Enemy2State.Wait;

    public Enemy2State State2
    {
        get { return _State2; }
        set { _State2 = value; }
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
    }


    void Update()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }


        if (motionMgr.isFinished())
        {
            //歩き
            if (State2 == Enemy2State.Walk)
            {
                motionMgr.startMotion(motions[0]);
            }
            //攻撃
            else if (State2 == Enemy2State.Attack)
            {
                motionMgr.startMotion(motions[1]);
            }
            //怯み
            else if (State2 == Enemy2State.Stop)
            {
                motionMgr.startMotion(motions[2]);
            }
            //停止
            if (State2 == Enemy2State.Wait)
            {
                motionMgr.startMotion(motions[3]);
            }
        }


        motionMgr.updateParam(live2DModel);
        live2DModel.update();
        live2DModel.draw();
    }
}