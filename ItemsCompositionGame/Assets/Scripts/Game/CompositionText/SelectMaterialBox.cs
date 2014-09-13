using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SelectMaterialBox : MonoBehaviour {

    // 外部変数
    public UISprite m_material_icon;    //!< 素材のアイコン
    public UILabel m_name_label;        //!< 名前のラベル
    public UILabel m_explain_label;     //!< 説明のラベル

    // 内部変数
    private int m_No;   //!< 自身のID
    private Func<int, GameObject, bool> m_SelectFunc;

    /// <summary>
    /// データをセット
    /// </summary>
    /// <param name="no">自身の番号</param>
    /// <param name="i_itemData">アイテムデータ</param>
    /// <param name="func">デリゲート関数</param>
    public void SetData(int no, MasterDataControllerClass.ItemMaster i_itemData, Func<int, GameObject, bool> func) {
        m_No = no;
        m_SelectFunc = func;

        m_name_label.text           = i_itemData.name;
        m_explain_label.text        = i_itemData.name;

        m_material_icon.atlas       = ComposGameSys.GetUIAtlus(i_itemData.category);
        m_material_icon.spriteName  = i_itemData.icon;
    }

    /// <summary>
    /// 材料が選択された際に発動
    /// </summary>
    public void SelectBox(GameObject obj) {
        m_SelectFunc(m_No, obj);
    }
}
