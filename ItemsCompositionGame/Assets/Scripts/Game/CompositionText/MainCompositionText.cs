using UnityEngine;
using System.Collections;

public class MainCompositionText : ComposGameSys {

    // 外部変数
    public CommonLinkPrefab m_linkMaterialList;
    public GameObject m_materialList;

    public CommonLinkPrefab m_linkCoockwareList;
    public GameObject m_coockwareList;

    public UILabel compItemNameLabel;  //!< 合成先名
    public UILabel cookwareNameLabel;  //!< 合成元名
    public UILabel resultLabel;        //!< 合成結果

    public int setItemId;

    public override void Awake() {
        base.Awake();

        // 材料リスト
        m_materialList = m_linkMaterialList.CreateLinkPrefab();
        if (m_materialList != null) {
            m_materialList.GetComponent<CommonScrollList>().SetFunc(MaterialDataSet);
            m_materialList.GetComponent<CommonScrollList>().m_dataNum = CommonMasterLoader.ItemMasterData.Count;
        }

        // 器具リスト
        m_coockwareList = m_linkCoockwareList.CreateLinkPrefab();
        if (m_coockwareList != null) {
            m_coockwareList.GetComponent<CommonScrollList>().SetFunc(CoockwareDataSet);
            m_coockwareList.GetComponent<CommonScrollList>().m_dataNum = CommonMasterLoader.CoockwareMasterData.Count;
            // 最初は止めておく
            m_coockwareList.SetActive(false);
        }
        compItemNameLabel.text = "";
        cookwareNameLabel.text = "";
        resultLabel.text = "";
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
        obj.GetComponent<SelectMaterialBox>().SetMaterialData(no, CommonMasterLoader.ItemMasterData[no], SelectedListBox);
        return true;
    }

    /// <summary>
    /// リストの内部データをセット
    /// </summary>
    /// <param name="no">オブジェクトの番号</param>
    /// <param name="obj">リストオブジェクト</param>
    /// <returns>bool</returns>
    public bool CoockwareDataSet(int no, GameObject obj) {
        obj.GetComponent<SelectMaterialBox>().SetCookwareData(no, CommonMasterLoader.CoockwareMasterData[no], SelectedCoockwareBox);
        return true;
    }

    /// <summary>
    /// 選択されたアイテムを合成に追加していく
    /// </summary>
    /// <param name="no"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool SelectedListBox(int no, GameObject obj) {
        cookwareNameLabel.text = "";
        resultLabel.text = "";
        if (CommonMasterLoader.ItemMasterData[no].cook) {
            compItemNameLabel.text = CommonMasterLoader.ItemMasterData[no].name;
            setItemId = CommonMasterLoader.ItemMasterData[no].item_id;
            m_materialList.SetActive(false);
            m_coockwareList.SetActive(true);
        }
        else {
            compItemNameLabel.text = "それは調理できません。";
        }
        return true;
    }

    /// <summary>
    /// 選択された器具で調理する
    /// </summary>
    /// <param name="no"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool SelectedCoockwareBox(int no, GameObject obj) {
        cookwareNameLabel.text = CommonMasterLoader.CoockwareMasterData[no].name;
        foreach (var result in CommonMasterLoader.CompositionComboMasterData.Values) {
            if (result.item_id == setItemId && result.cookware_id == no) {
                resultLabel.text = CommonMasterLoader.ItemMasterData[result.result_item_id].name;
                break;
            }
        }
        m_materialList.SetActive(true);
        m_coockwareList.SetActive(false);
        return true;
    }
}
