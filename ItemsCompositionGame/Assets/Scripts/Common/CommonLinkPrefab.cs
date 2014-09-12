﻿using UnityEngine;
using System.Collections;

public class CommonLinkPrefab : MonoBehaviour {

    // 外部変数
    public GameObject LinkPrefabObject; //!< リンクプレハブ
    public bool sameName = false;       //!< 同じ名前を許可するか
    public bool destroyMine = true;   //!< 自身を削除するか

    // 内部データ
    private static GameObject instanceObject;  //!< 生成されたオブジェクト

    void Awake() {
        CreateLinkPrefab();
        if (destroyMine) {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// リンクプレハブを作成
    /// </summary>
    /// <returns></returns>
    public GameObject CreateLinkPrefab() {
        if (LinkPrefabObject == null) {
            Debug.Log("リンクプレハブが設定されていません。");
            return null;
        }
        if (instanceObject == null) {
            // 同じ名前が許可されていなければ探索する
            if (!sameName) {
                // あればそれを返す
                GameObject alreadyGameObject = CommonUtil.SearchObjectChild(LinkPrefabObject.name, this.gameObject.transform.parent);
                if (alreadyGameObject != null) {
                    return alreadyGameObject;
                }
            }
            // リンクされたプレハブを生成
            GameObject obj = GameObject.Instantiate(LinkPrefabObject) as GameObject;
            obj.name = LinkPrefabObject.name;
            obj.transform.parent = this.gameObject.transform.parent;

            // 親を設置するとGrobalの値を参照してしまうようなので、ここでローカルな値に直してやる
            obj.transform.position = new Vector3(LinkPrefabObject.transform.position.x, LinkPrefabObject.transform.position.y, LinkPrefabObject.transform.position.z);
            obj.transform.localScale = new Vector3(LinkPrefabObject.transform.localScale.x, LinkPrefabObject.transform.localScale.y, LinkPrefabObject.transform.localScale.z);
            instanceObject = obj;
        }
        return instanceObject;
    }

    /// <summary>
    /// リンクされたプレハブを取得
    /// </summary>
    /// <returns>GameObject</returns>
    private GameObject GetLinkPrefab() {
        return LinkPrefabObject;
    }

    /// <summary>
    /// 生成されたオブジェクトを取得
    /// </summary>
    /// <returns>GameObject</returns>
    public static GameObject GetInstanceObject() {
        return instanceObject;
    }
}