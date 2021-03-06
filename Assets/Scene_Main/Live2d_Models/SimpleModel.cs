using UnityEngine;
using System;
using live2d;

[ExecuteInEditMode]
public class SimpleModel : MonoBehaviour
{

    [SerializeField]
    private Main.Main_PlayerCharacter Player;

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


    void Start()
    {
        motions = new Live2DMotion[motionFiles.Length];

        if (live2DModel != null) return;
        Live2D.init();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        // 描画モードの設定
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

        motions[1].setLoop(true);
        motions[1].setLoopFadeIn(false);
        motions[0].setLoop(true);
        motions[0].setLoopFadeIn(false);

        motionMgr = new MotionQueueManager();
    }

    private Main.CharaState? main = null;

    void Update()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        if (motionMgr.isFinished() || main != Player.State)
        {
            //歩き
            if (Player.State == Main.CharaState.Walk)
            {
                live2DModel.setParamFloat("Param3", -1);
                live2DModel.setParamFloat("Param7", 0);

                motionMgr.startMotion(motions[0]);
            }
            //走り
            else if (Player.State == Main.CharaState.Run)
            {
                live2DModel.setParamFloat("Param3", -1);
                live2DModel.setParamFloat("Param7", 0);

                motionMgr.startMotion(motions[1]);
            }
            //素手殴り
            else if (Player.State == Main.CharaState.HandAttack)
            {
                //live2DModel.setParamFloat("Param3", -1);
                motionMgr.startMotion(motions[2]);
            }
            //鉄棒殴り
            else if (Player.State == Main.CharaState.BarAttack)
            {
                motionMgr.startMotion(motions[3]);
            }
            //銃攻撃
            else if (Player.State == Main.CharaState.Shot)
            {
                motionMgr.startMotion(motions[4]);
            }
            //通常ダメージ
            else if (Player.State == Main.CharaState.Damage)
            {
                motionMgr.startMotion(motions[8]);
            }
            //銃攻撃
            else if (Player.State == Main.CharaState.Down)
            {
                motionMgr.startMotion(motions[9]);
            }
            //停止
            if (Player.State == Main.CharaState.Wait)
            {
                //live2DModel.setParamFloat("Param3", -1);
                motionMgr.startMotion(motions[10]);
                //motionMgr.stopAllMotions();
            }
        }

        main = Player.State;

        motionMgr.updateParam(live2DModel);
        live2DModel.update();
        live2DModel.draw();
    }
}