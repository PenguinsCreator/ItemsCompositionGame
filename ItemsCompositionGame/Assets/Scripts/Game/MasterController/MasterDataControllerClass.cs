using UnityEngine;
using System.Collections;

/// <summary>
/// マスタデータの情報を一元管理するクラス
/// </summary>
public class MasterDataControllerClass {

    public static int IncNum(ref int num){
        return ++num;
    }

    /// <summary>
    /// アイテムマスタ
    /// </summary>
    public class ItemMaster
    {
        public int item_id;
        public string name;
        public int category;
        public bool cook;
        public string icon;
        public int deleted;

        /// <summary>
        /// 文字列をクラス情報に変換する
        /// </summary>
        public static ItemMaster ConvertStrToMaster(string[] i_data) {
            int i = 0;
            ItemMaster master = new ItemMaster();
            master.item_id      = int.Parse(i_data[i]);
            master.name         = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.category     = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.cook         = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]) == 0 ? false : true;
            master.icon         = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.deleted      = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            return master;
        }
    }

    /// <summary>
    /// 器具マスタ
    /// </summary>
    public class CoockwareMaster
    {
        public int cookware_id;
        public string name;
        public int cookable_category;
        public int need_cc;
        public string icon;
        public string deleted;

        /// <summary>
        /// 文字列をクラス情報に変換する
        /// </summary>
        public static CoockwareMaster ConvertStrToMaster(string[] i_data) {
            int i = 0;
            CoockwareMaster master = new CoockwareMaster();
            master.cookware_id          = int.Parse(i_data[i]);
            master.name                 = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.cookable_category    = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.need_cc              = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.icon                 = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.deleted              = i_data[MasterDataControllerClass.IncNum(ref i)];
            return master;
        }
    }

    /// <summary>
    /// 合成組み合わせマスタ
    /// </summary>
    public class CompositionComboMaster
    {
        public int composition_id;
        public int item_id;
        public int cookware_id;
        public int result_item_id;
        public int result_word_num;
        public string result_word_1;
        public int word_1_per;
        public string result_word_2;
        public int word_2_per;
        public string result_word_3;
        public int word_3_per;
        public int deleted;

        public static CompositionComboMaster ConvertStrToMaster(string[] i_data) {
            int i = 0;
            CompositionComboMaster master = new CompositionComboMaster();
            master.composition_id     = int.Parse(i_data[i]);
            master.item_id            = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.cookware_id        = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.result_item_id     = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.result_word_num    = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.result_word_1      = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.word_1_per         = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.result_word_2      = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.word_2_per         = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.result_word_3      = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.word_3_per         = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            return master;
        }
    }

    /// <summary>
    /// 合成率文言マスタ
    /// </summary>
    public class CompositionWordMaster
    {
        public int id;
        public float min;
        public float max;
        public string word;
        public int deleted;
        public static CompositionWordMaster ConvertStrToMaster(string[] i_data) {
            int i = 0;
            CompositionWordMaster master = new CompositionWordMaster();
            master.id         = int.Parse(i_data[i]);
            master.min        = float.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.max        = float.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            master.word       = i_data[MasterDataControllerClass.IncNum(ref i)];
            master.deleted    = int.Parse(i_data[MasterDataControllerClass.IncNum(ref i)]);
            return master;
        }
    }
}
