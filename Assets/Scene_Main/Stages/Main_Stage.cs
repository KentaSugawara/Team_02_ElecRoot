using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    [ExecuteInEditMode]
    public class Main_Stage : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _MapSize;

        [SerializeField]
        private Vector2 _ChipSize;

        [SerializeField]
        private Main_StageChipSet _ChipSet;
        public Main_StageChipSet ChipSet
        {
            get { return _ChipSet; }
        }

        [SerializeField]
        private List<GameObjectList> Chips = new List<GameObjectList>();
        [SerializeField]
        private List<IntList> ChipIndexList = new List<IntList>();


        [SerializeField]
        private GameObject _PreviewObjInstance;
        public GameObject PreviewObjInstance
        {
            get { return _PreviewObjInstance; }
            set { _PreviewObjInstance = value; }
        }

        [SerializeField]
        private int _PreviewObjIndex;
        public int PreviewObjIndex
        {
            get { return _PreviewObjIndex; }
            set { _PreviewObjIndex = value; }
        }

        [System.Serializable]
        public class GameObjectList
        {
            [SerializeField]
            private List<GameObject> list = new List<GameObject>();
            public GameObject this[int index]
            {
                get { return list[index]; }
                set { list[index] = value; }
            }

            public void Add(GameObject obj)
            {
                list.Add(obj);
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            public int Count
            {
                get { return list.Count; }
            }
        }

        [System.Serializable]
        public class IntList
        {
            [SerializeField]
            private List<int> list = new List<int>();
            public int this[int index]
            {
                get { return list[index]; }
                set { list[index] = value; }
            }

            public void Add(int value)
            {
                list.Add(value);
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            public int Count
            {
                get { return list.Count; }
            }
        }

        private void Awake()
        {
            if (_PreviewObjInstance != null) _PreviewObjInstance.SetActive(false);
        }

#if UNITY_EDITOR
        public void Editor_Resize()
        {
            transform.localScale = new Vector3(_MapSize.x * _ChipSize.x, 1.0f, _MapSize.y * _ChipSize.y);
            Editor_ResizeList(_MapSize);

            //resize scale
            for (int i = 0; i < Chips.Count; ++i)
            {
                for (int j = 0; j < Chips[i].Count; ++j)
                {
                    if (Chips[i][j] != null)
                    {
                        Chips[i][j].transform.localScale
                            = new Vector3
                            (
                                _ChipSize.x / transform.localScale.x,
                                1.0f,
                                _ChipSize.y / transform.localScale.z
                            );
                        Chips[i][j].transform.position
                            = calcCenterPos(new Vector2Int(i, j));
                    }
                }
            }
        }

        private void Editor_ResizeList(Vector2Int size)
        {
            if (Chips == null)
            {
                Chips = new List<GameObjectList>();
                ChipIndexList = new List<IntList>();
            }

            for (int i = Chips.Count; i < size.x; ++i)
            {
                Chips.Add(new GameObjectList());
                ChipIndexList.Add(new IntList());
            }
            for (int i = 0; i < size.x; ++i)
            {
                if (Chips[i] == null)
                {
                    Chips[i] = new GameObjectList();
                    ChipIndexList[i] = new IntList();
                }
                for (int j = Chips[i].Count; j < size.y; ++j)
                {
                    Chips[i].Add(null);
                    ChipIndexList[i].Add(-1);
                }
            }

            //余分な行の要素をすべてDestroy
            while (0 < Chips.Count - size.x)
            {
                int index = Chips.Count - 1;

                
                for (int i = 0; i < Chips[index].Count; ++i)
                {
                    if (Chips[index][i] != null)
                    {
                        DestroyImmediate(Chips[index][i]);
                    }
                }

                Chips.RemoveAt(index);
                ChipIndexList.RemoveAt(index);
            }

            for (int i = 0; i < Chips.Count; ++i)
            {
                while (0 < Chips[i].Count - size.y)
                {
                    int index = Chips[i].Count - 1;
                    if (Chips[i][index] != null)
                    {
                        DestroyImmediate(Chips[i][index]);
                    }
                    Chips[i].RemoveAt(index);
                    ChipIndexList[i].RemoveAt(index);
                }
            }
        }

        private GameObject getSelectedChip(int SelectIndex)
        {
            if (ChipSet != null)
            {
                if (SelectIndex < 0 ||
                    SelectIndex >= ChipSet.Objects.Count) return null;

                return ChipSet.Objects[SelectIndex];

            }
            return null;
        }

        public Vector3 calcCenterPos(Vector2Int index)
        {
            Vector2 s = new Vector2
                (
                    -_MapSize.x * _ChipSize.x * 0.5f,
                    -_MapSize.y * _ChipSize.y * 0.5f
                );
            return new Vector3
                (
                    s.x + (index.x + 0.5f) * _ChipSize.x,
                    1.0f,
                    s.y + (index.y + 0.5f) * _ChipSize.y
                ) + transform.position;
        }

        public Vector2Int calcMapIndex(Vector3 pos)
        {
            Vector3 s = calcCenterPos(new Vector2Int(0, 0));
            s.x -= _ChipSize.x * 0.5f;
            s.z -= _ChipSize.y * 0.5f;

            Vector3 d = pos - s;

            Vector2Int index = new Vector2Int
                (
                    (int)(d.x / _ChipSize.x),
                    (int)(d.z / _ChipSize.y)
                );
            return index;
        }

        public void setChip(Vector2Int index, int MapChipIndex)
        {
            if (index.x < 0 || index.y < 0 || _MapSize.x <= index.x || _MapSize.y <= index.y) return;

            var prefab = getSelectedChip(MapChipIndex);
            if (prefab == null) return;

            if (Chips[index.x][index.y] != null)
            {
                DestroyImmediate(Chips[index.x][index.y]);
            }

            Chips[index.x][index.y] = Instantiate(prefab);
            Chips[index.x][index.y].transform.position = calcCenterPos(index);
            Chips[index.x][index.y].transform.SetParent(transform);
            ChipIndexList[index.x][index.y] = MapChipIndex;
        }

        public void setChip(Vector3 pos, int MapChipIndex)
        {
            var index = calcMapIndex(pos);
            setChip(index, MapChipIndex);
        }

        public void createPreviewInstance(int MapChipIndex)
        {
            var prefab = getSelectedChip(MapChipIndex);
            if (prefab == null) return;

            if (_PreviewObjInstance != null) DestroyImmediate(_PreviewObjInstance);

            _PreviewObjInstance = Instantiate(prefab);
        }

        public void updatePreviewInstance(Vector3 pos)
        {
            var index = calcMapIndex(pos);
            if (index.x < 0 || index.y < 0 || _MapSize.x <= index.x || _MapSize.y <= index.y) return;
            if (_PreviewObjInstance == null) return;

            _PreviewObjInstance.SetActive(true);
            _PreviewObjInstance.transform.position = calcCenterPos(index);
            _PreviewObjInstance.transform.SetParent(transform);
        }

        public void hidePreviewInstance()
        {
            if (_PreviewObjInstance == null) return;

            _PreviewObjInstance.SetActive(false);
        }

        public void removeChip(Vector3 pos)
        {
            var index = calcMapIndex(pos);
            if (Chips[index.x][index.y] != null)
            {
                DestroyImmediate(Chips[index.x][index.y]);
                ChipIndexList[index.x][index.y] = -1;
            }
        }

        public void resetFromMapChip()
        {
            //すべて削除
            for (int i = 0; i < Chips.Count; ++i)
            {
                for (int j = 0; j < Chips[i].Count; ++j)
                {
                    if (Chips[i][j] != null)
                    {
                        DestroyImmediate(Chips[i][j]);
                    }
                }
            }

            //再生成
            for (int i = 0; i < Chips.Count; ++i)
            {
                for (int j = 0; j < Chips[i].Count; ++j)
                {
                    if (ChipIndexList[i][j] < ChipSet.Objects.Count)
                    {
                        setChip(new Vector2Int(i, j), ChipIndexList[i][j]);
                    }
                }
            }
        }
#endif
    }
}