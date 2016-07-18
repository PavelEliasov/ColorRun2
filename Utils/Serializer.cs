using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Serializer <Tkey,Tvalue> {
    class Key {
   
     public   Tkey key;
    }

    class Value {
       public Tvalue value;
    }


    [SerializeField]
    List<Key> keyList=new List<Key>();
    List<Value> valueList = new List<Value>();
    string _jSonvaluesString;
    string _jSonKeysString;
    string[] jSonvalues;
    string[] jSonkeys;

    public void SerializeDictionary(Dictionary<Tkey,Tvalue> dic) {

       
        foreach (var obj in dic ) {

            Key key = new Key();
            key.key = obj.Key;
            keyList.Add(key);

            Value val = new Value();
            val.value = obj.Value;
            valueList.Add(val);
        }
        ToJSon();

        Debug.Log(_jSonvaluesString);
        Debug.Log(_jSonKeysString);
    }

    public Dictionary<Tkey, Tvalue> Deserialize() {
        Dictionary<Tkey, Tvalue> dic = new Dictionary<Tkey, Tvalue>();

        keyList.Clear();
        valueList.Clear();

        jSonkeys = _jSonKeysString.Split('|');
        jSonvalues = _jSonvaluesString.Split('|');
        Debug.Log(jSonvalues.Length-1);

        for (int i=0; i<jSonvalues.Length-1;++i) {
            valueList.Add(JsonUtility.FromJson<Value>(jSonvalues[i]));
            keyList.Add(JsonUtility.FromJson<Key>(jSonkeys[i]));
            dic.Add(JsonUtility.FromJson<Key>(jSonkeys[i]).key, JsonUtility.FromJson<Value>(jSonvalues[i]).value);
        }

        return dic;
        //foreach (var obj in valueList) {
        //    dic.Add(1, obj.value);
        //    Debug.Log(obj.value);
        //}
    }


    void ToJSon() {
        foreach (var obj in keyList) {

            _jSonKeysString += JsonUtility.ToJson(obj) + "|";
        }

        foreach (var obj in valueList) {
            _jSonvaluesString +=JsonUtility.ToJson(obj)+ "|";
        }
     

   
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
