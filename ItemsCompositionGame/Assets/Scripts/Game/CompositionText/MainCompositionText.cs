using UnityEngine;
using System.Collections;

public class MainCompositionText : ComposGameSys {

    // 外部変数
    public CommonLinkPrefab m_materialList;

    // 内部変数
    private CommonScrollList m_scrollList;

    public override void Awake() {
        base.Awake();
        GameObject obj = m_materialList.CreateLinkPrefab();
        if (obj != null) {
            m_scrollList = obj.GetComponent<CommonScrollList>();
            m_scrollList.SetFunc(MaterialDataSet);
            m_scrollList.m_dataNum = CommonMasterLoader.ItemMasterData.Count;
        }
    }

    public override void Start() {
        base.Start();
    }

    /// <summary>
    /// リストの内部データをセット
    /// </summary>
    /// <param name="no">オブジェクトの番号</param>
    /// <param name="obj">リストオブジェクト</param>
    /// <returns>bool</returns>
    public bool MaterialDataSet(int no, GameObject obj) {
        obj.GetComponent<SelectMaterialBox>().SetData(no, CommonMasterLoader.ItemMasterData[no], SelectedListBox);
        return true;
    }

    public bool SelectedListBox(int no, GameObject obj) {
        Debug.Log("選択されたアイテムは " + CommonMasterLoader.ItemMasterData[no].name + " です。");
        return true;
    }
}
