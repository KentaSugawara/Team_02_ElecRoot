using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


namespace Main
{
    [CustomEditor(typeof(Main_Stage))]
    public class Editor_Main_Stage : Editor
    {
        private int _SelectIndex = 0;

        RaycastHit hit;
        private int Mask_Stage = 1 << 20;
        void OnSceneGUI()
        {
            Vector3 mousePosition = Event.current.mousePosition;
            //座標軸を修正
            mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;

            var ray = Camera.current.ScreenPointToRay(mousePosition);
            bool result = Physics.Raycast(ray, out hit, 1000.0f, Mask_Stage);

            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.character == '1')
                {
                    if (result)
                    {
                        ((Main_Stage)target).setChip(hit.point, _SelectIndex);
                    }
                }
                else if (Event.current.character == '3')
                {
                    if (result)
                    {
                        ((Main_Stage)target).removeChip(hit.point);
                    }
                }

                if (Event.current.character == '5')
                {
                    IncrementSelectIndex();
                    ((Main_Stage)target).PreviewObjIndex = _SelectIndex;
                    ((Main_Stage)target).createPreviewInstance(_SelectIndex);
                }
                else if (Event.current.character == '4')
                {
                    DecrementSelectIndex();
                    ((Main_Stage)target).PreviewObjIndex = _SelectIndex;
                    ((Main_Stage)target).createPreviewInstance(_SelectIndex);
                }
            }

            if (((Main_Stage)target).PreviewObjInstance == null || ((Main_Stage)target).PreviewObjIndex != _SelectIndex)
            {
                ((Main_Stage)target).createPreviewInstance(_SelectIndex);
            }

            if (result) ((Main_Stage)target).updatePreviewInstance(hit.point);
            else ((Main_Stage)target).hidePreviewInstance();

        }

        private void IncrementSelectIndex()
        {
            var ChipSet = ((Main_Stage)target).ChipSet;
            if (ChipSet != null)
            {
                ++_SelectIndex;
                if (_SelectIndex >= ChipSet.Objects.Count)
                    _SelectIndex = ChipSet.Objects.Count - 1;
                Debug.Log("Index : " + _SelectIndex);
            }
        }

        private void DecrementSelectIndex()
        {
            var ChipSet = ((Main_Stage)target).ChipSet;
            if (ChipSet != null)
            {
                --_SelectIndex;
                if (_SelectIndex < 0)
                    _SelectIndex = 0;
                Debug.Log("Index : " + _SelectIndex);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Resize"))
            {
                ((Main_Stage)target).Editor_Resize();
            }

            if (GUILayout.Button("resetFromMapChip"))
            {
                ((Main_Stage)target).resetFromMapChip();
            }
        }
    }
}
#endif