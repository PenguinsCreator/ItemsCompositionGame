using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ScrollListPrepare : MonoBehaviour
{
    // 外部変数
    public GameObject m_prefabObject;           //!< 作成するオブジェクト
    public CommonLinkPrefab m_LinkPrefabObject; //!< リンクプレハブオブジェクト
    public int m_createGameObjectNum;           //!< 作成するオブジェクトの数
    public int m_dataNum;                       //!< 内包するデータの数

    protected GameObject mineObject;  //!< 自分自身

    protected enum ScrollVec
    {
        UP,
        DOWN,
    }

    protected enum ScrollOper
    {
        PLUS,
        MAIN,
    }

    /// <summary>
    /// UIPanelのクリッピング情報
    /// </summary>
    protected UIPanel m_UIPanel;
    protected float m_UIPanelClippingCenterX;
    protected float m_UIPanelClippingCenterY;
    protected float m_UIPanelClippingSizeX;
    protected float m_UIPanelClippingSizeY;

    /// <summary>
    /// UIGridの情報
    /// </summary>
    protected UIGrid m_Grid;
    protected int   m_GridMaxPerLine;
    protected float m_GridCellWidth;
    protected float m_GridCellHeight;

    /// <summary>
    /// リストオブジェクトの大きさ
    /// </summary>
    protected float m_BoxSizeX;
    protected float m_BoxSizeY;

    /// <summary>
    /// スクロールするのに必要な移動量
    /// </summary>
    protected float m_ScrollValue;

    void Awake() {
        mineObject = this.gameObject;

        // リンクプレハブが登録されていなければ探す
        if (m_LinkPrefabObject == null) { m_LinkPrefabObject = this.gameObject.transform.GetComponentInChildren<CommonLinkPrefab>(); }
        // 作成するプレハブが登録されていなければリンクプレハブから持ってくる
        if (m_prefabObject == null) { m_prefabObject = m_LinkPrefabObject.prefabObject; }
        SetBoxData();

        m_UIPanel = this.gameObject.GetComponent<UIPanel>();
        SetUIPanelData();

        m_Grid = CommonUtil.SearchObjectChild("Grid", this.transform).GetComponent<UIGrid>();
        SetUIGridData();

        // スクロール量を算出
        m_ScrollValue = m_BoxSizeY + (m_GridCellHeight * 100);

        // プレハブの作成はこっちでやるのでリンクプレハブを止める
        m_LinkPrefabObject.gameObject.transform.parent = m_Grid.transform.parent;
        m_LinkPrefabObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// UIPanelに関する必要データをセット
    /// </summary>
    void SetUIPanelData() {
        m_UIPanelClippingCenterX = m_UIPanel.clipRange.x;
        m_UIPanelClippingCenterY = m_UIPanel.clipRange.y;
        m_UIPanelClippingSizeX = m_UIPanel.clipRange.z;
        m_UIPanelClippingSizeY = m_UIPanel.clipRange.w;
    }

    /// <summary>
    /// UIGridに関する必要データをセット
    /// </summary>
    void SetUIGridData() {
        m_GridMaxPerLine    = m_Grid.maxPerLine;
        m_GridCellWidth     = m_Grid.cellWidth;
        m_GridCellHeight    = m_Grid.cellHeight;
    }

    /// <summary>
    /// ボックスに関する必要データをセット
    /// </summary>
    void SetBoxData() {
        m_BoxSizeX = m_prefabObject.GetComponent<BoxCollider>().size.x;
        m_BoxSizeY = m_prefabObject.GetComponent<BoxCollider>().size.y;
    }
}

public class CommonScrollList : ScrollListPrepare {

    // 外部変数
    public List<GameObject> m_listObjects = new List<GameObject>(); //!< リストオブジェクトの一覧
    public GameObject m_nowLastObject;                              //!< 現在の最後尾オブジェクト

    // 内部変数
    private Func<int, GameObject, bool> m_func;     //!< デリゲート関数
    private bool init = true;                       //!< 初回判定

    private int m_nextObjectNum = 1;        //!< 次のオブジェクトの数
    private float m_nextScrollValue = 0;    //!< 次にスクロールが発生する移動量
    private int m_prebObjectNum = 0;        //!< 前のオブジェクトの数
    private float m_prebScrollValue = 0;    //!< 前にスクロールが発生した移動量

    /// <summary>
    /// 関数をセット
    /// </summary>
    public void SetFunc(Func<int, GameObject, bool> i_func) {
        m_func = i_func;
    }

    /// <summary>
    /// Start
    /// </summary>
    void Start() {
        if (init) {
            CreateListObject();
            init = false;
        }
        // 初回移動量を設定
        m_nextScrollValue = m_ScrollValue;
    }

    /// <summary>
    /// リストオブジェクトを作成
    /// </summary>
    void CreateListObject() {
        // 作成個数分オブジェクトを作成
        for (int i = 0; i < m_createGameObjectNum; i++) {
            GameObject obj = GameObject.Instantiate(m_prefabObject) as GameObject;
            if (obj != null) {
                // 作成したオブジェクトに情報を乗せる
                obj.transform.parent = m_Grid.transform;
                // 作成したオブジェクトのTransformが壊れる場合があるのでプレハブに合わせる
                obj.transform.position = new Vector3(m_prefabObject.transform.position.x, m_prefabObject.transform.position.y, m_prefabObject.transform.position.z);
                obj.transform.localScale = new Vector3(m_prefabObject.transform.localScale.x, m_prefabObject.transform.localScale.y, m_prefabObject.transform.localScale.z);
                obj.name = m_prefabObject.name + i;

                // 必要情報を保持
                m_listObjects.Add(obj);
                m_nowLastObject = obj;
                m_nextObjectNum++;
                if (m_func != null) { m_func(i + 1, obj); }
            }
        }
        // 作成が終わったらポジションを直す
        m_Grid.Reposition();
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update() {
        // オブジェクトの移動の発生を判定
        if (m_nextObjectNum < m_dataNum && -m_UIPanel.clipRange.y >= m_nextScrollValue) {
            RiseMoveGameObject();
        }
        else if (m_prebObjectNum > 0 && -m_UIPanel.clipRange.y <= m_prebScrollValue) {
            DownMoveGameObject();
        }
    }

    /// <summary>
    /// リストを上昇させた際のオブジェクトの移動
    /// </summary>
    void RiseMoveGameObject() {
        int index = (m_nextObjectNum - 1) % m_createGameObjectNum;
        // オブジェクトの移動
        MoveGameObject(index, ScrollVec.UP);
        // 頭だったオブジェクトを現在の最後尾にする
        m_nowLastObject = m_listObjects[index];
        // 情報の載せ換え
        m_func(m_nextObjectNum, m_nowLastObject);
        ValueTransition(ScrollOper.PLUS);
    }

    /// <summary>
    /// リストを下降させた際のオブジェクトの移動
    /// </summary>
    void DownMoveGameObject() {
        int index = (m_prebObjectNum - 1) % m_createGameObjectNum;
        // オブジェクトの移動
        MoveGameObject(index, ScrollVec.DOWN);
        // 最後尾オブジェクトを変更
        m_nowLastObject = m_listObjects.IndexOf(m_nowLastObject) == 0 ? m_listObjects[m_createGameObjectNum - 1] : m_listObjects[m_listObjects.IndexOf(m_nowLastObject) - 1];
        // 情報の載せ換え
        m_func(m_prebObjectNum, m_listObjects[index]);
        ValueTransition(ScrollOper.MAIN);
    }

    /// <summary>
    /// 前と次の値を変位させる
    /// </summary>
    /// <param name="type">変位タイプ</param>
    void ValueTransition(ScrollOper type) {
        switch (type) {
            case ScrollOper.PLUS:
                m_nextScrollValue += m_BoxSizeY;
                m_nextObjectNum++;
                m_prebScrollValue += m_BoxSizeY;
                m_prebObjectNum++;
                break;
            case ScrollOper.MAIN:
                m_nextScrollValue -= m_BoxSizeY;
                m_nextObjectNum--;
                m_prebScrollValue -= m_BoxSizeY;
                m_prebObjectNum--;
                break;
        }
    }

    /// <summary>
    /// オブジェクトの移動
    /// </summary>
    void MoveGameObject(int i_index, ScrollVec i_type) {
        Vector3 pos = m_listObjects[i_index].transform.position;
        float posX = pos.x;
        float posZ = pos.z;
        float newY = 0;
        switch (i_type) {
            case ScrollVec.UP:
                newY = m_nowLastObject.transform.localPosition.y - m_GridCellHeight;
                break;
            case ScrollVec.DOWN:
                newY = m_listObjects[i_index + 1 == m_createGameObjectNum ? 0 : i_index + 1].transform.localPosition.y + m_GridCellHeight;
                break;
        }
        m_listObjects[i_index].transform.localPosition = new Vector3(posX, newY, posZ);
    }
}
