using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CommonMasterLoader {

    // 定数
    const string MASTER_DATA_FOLDER_PATH = "MasterDatas/";

    // enum
    public enum Masters
    {
        ItemMaster,
        CoockwareMaster,
        CompositionComboMaster,
        CompositionWordMaster,
    }

    // マスタ名配列
    private static string[] masterDatasName = new string[] {
        "アイテムマスタ",
        "器具マスタ",
        "合成組み合わせマスタ",
        "合成率文言マスタ",
    };

    // マスタ情報一覧
    private static Dictionary<int, MasterDataControllerClass.ItemMaster> _itemMasterData;
    public static Dictionary<int, MasterDataControllerClass.ItemMaster> ItemMasterData {
        get { return _itemMasterData; }
        private set { _itemMasterData = value; }
    }

    private static Dictionary<int, MasterDataControllerClass.CoockwareMaster> _coockwareMasterData;
    public static Dictionary<int, MasterDataControllerClass.CoockwareMaster> CoockwareMasterData {
        get { return _coockwareMasterData; }
        private set { _coockwareMasterData = value; }
    }

    private static Dictionary<int, MasterDataControllerClass.CompositionComboMaster> _compositionComboMasterData;
    public static Dictionary<int, MasterDataControllerClass.CompositionComboMaster> CompositionComboMasterData {
        get { return _compositionComboMasterData; }
        private set { _compositionComboMasterData = value; }
    }

    private static Dictionary<int, MasterDataControllerClass.CompositionWordMaster> _compositionWordMasterData;
    public static Dictionary<int, MasterDataControllerClass.CompositionWordMaster> CompositionWordMasterData {
        get { return _compositionWordMasterData; }
        private set { _compositionWordMasterData = value; }
    }

    /// <summary>
    /// マスタデータ初期化
    /// </summary>
    public void InitMasterData() {
        for (int i = 0; i < masterDatasName.Length; i++) {
            SetMasterData((Masters)i, Resources.Load(MASTER_DATA_FOLDER_PATH + masterDatasName[i]).ToString().Split(new char[] { '\n' }));
        }
    }

    /// <summary>
    /// 該当するマスタデータの情報をセットする
    /// </summary>
    /// <param name="i_type">マスタデータの型</param>
    /// <param name="i_data">読み出した生マスタデータ</param>
    void SetMasterData(Masters i_type, string[] i_data) {
        switch (i_type) {
            case Masters.ItemMaster:
                ItemMasterData = CreateMasterData<MasterDataControllerClass.ItemMaster>(i_data, MasterDataControllerClass.ItemMaster.ConvertStrToMaster);
                break;
            case Masters.CoockwareMaster:
                CoockwareMasterData = CreateMasterData<MasterDataControllerClass.CoockwareMaster>(i_data, MasterDataControllerClass.CoockwareMaster.ConvertStrToMaster);
                break;
            case Masters.CompositionComboMaster:
                CompositionComboMasterData = CreateMasterData<MasterDataControllerClass.CompositionComboMaster>(i_data, MasterDataControllerClass.CompositionComboMaster.ConvertStrToMaster);
                break;
            case Masters.CompositionWordMaster:
                CompositionWordMasterData = CreateMasterData<MasterDataControllerClass.CompositionWordMaster>(i_data, MasterDataControllerClass.CompositionWordMaster.ConvertStrToMaster);
                break;
        }
    }

    /// <summary>
    /// マスタデータの中身を作成
    /// </summary>
    /// <param name="i_masterData">読み出した生マスタデータ</param>
    /// <returns>実マスタデータの中身</returns>
    private Dictionary<int, T> CreateMasterData<T>(string[] i_masterData, Func<string[], T> func) {
        List<string> columnName = new List<string>();
        Dictionary<int, T> masterData = new Dictionary<int, T>();

        int num;
        // 一行ずつ読み出して整形
        foreach (string lineData in i_masterData) {
            // 先頭が#もしくは数字でないならば読み込まない
            if (lineData != "" && lineData.Substring(0, 1) != "#" && int.TryParse(lineData.Substring(0, 1), out num)) {
                // 一行をさらに細切れにしてクラスに合うように整形
                string[] line = lineData.Split(new char[] { ',' });
                masterData.Add(int.Parse(line[0]), func(line));
            }
        }
        return masterData;
    }
}
