using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using live2d;

namespace Main
{
    public class Main_EventPlayerCharacter : MonoBehaviour
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

        [SerializeField]
        private CharaState _State;
        public CharaState State
        {
            get { return _State; }
            set { _State = value; }
        }

        private CharaState? main = null;

        void Update()
        {
            if (live2DModel == null) return;
            live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

            if (!Application.isPlaying)
            {
                live2DModel.update();
                return;
            }


            if (motionMgr.isFinished() || main != _State)
            {
                //歩き
                if (_State == Main.CharaState.Walk)
                {
                    live2DModel.setParamFloat("Param3", -1);
                    live2DModel.setParamFloat("Param7", 0);

                    motionMgr.startMotion(motions[0]);
                }
                //走り
                else if (_State == Main.CharaState.Run)
                {
                    live2DModel.setParamFloat("Param3", -1);
                    live2DModel.setParamFloat("Param7", 0);

                    motionMgr.startMotion(motions[1]);
                }
                //停止
                if (_State == Main.CharaState.Wait)
                {
                    //live2DModel.setParamFloat("Param3", -1);
                    motionMgr.startMotion(motions[2]);
                    //motionMgr.stopAllMotions();
                }
            }

            main = _State;

            motionMgr.updateParam(live2DModel);
            live2DModel.update();
            live2DModel.draw();
        }

        public void MoveLocalPosition(float time, Vector3 Start, Vector3 End, System.Action callback)
        {
            StartCoroutine(Routine_MoveLocalPosition(time, Start, End, callback));
        }

        private IEnumerator Routine_MoveLocalPosition(float time, Vector3 Start, Vector3 End, System.Action callback)
        {
            for (float t = 0.0f; t < time; t += Time.unscaledDeltaTime)
            {
                transform.localPosition = Vector3.Lerp(Start, End, t / time);
                yield return null;
            }
            transform.localPosition = End;

            callback();
        }
    }
}
