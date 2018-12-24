using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class ResourceTest : MonoBehaviour {
	// Use this for initialization
	void Start () {
//		AssetBundle assetbundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/attack");
//		GameObject go = Instantiate(assetbundle.LoadAsset<GameObject>("Attack"));
//		GameObject go = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Attack.prefab");
//		Instantiate(go);
//		SerializeTest();
//		DeSerializeTest();
//		XmlDeSerilize();
		BinaryDeSerializeTest();
	}

	void BinaryDeSerializeTest() {
		TestSerialize testSerialize = BinaryDeSerialize();
		Debug.Log(testSerialize.Id);
		Debug.Log(testSerialize.Name);
		for (int i = 0; i < testSerialize.ListTest.Count; i++) {
			Debug.Log(testSerialize.ListTest[i]);
		}
	}

	void DeSerializeTest() {
		TestSerialize testSerialize = XmlDeSerilize();
		Debug.Log(testSerialize.Id);
		Debug.Log(testSerialize.Name);
		for (int i = 0; i < testSerialize.ListTest.Count; i++) {
			Debug.Log(testSerialize.ListTest[i]);
		}
	}
	
	void SerializeTest() {
		TestSerialize testSerialize = new TestSerialize {Id = 1, Name = "测试", ListTest = new List<int> {1, 2, 3}};
		testSerialize.SetPrivateId(999);
		testSerialize.SetPrivateList(888);
		testSerialize.SetPrivateList(777);
		testSerialize.SetPrivateList(666);
		testSerialize.SetPrivateName("神秘人");
//		XmlSerialize(testSerialize);
		BinarySerialize(testSerialize);
	}

	void XmlSerialize(TestSerialize _testSerialize) {
		// 1.打开文件流 2.创建写入流 3.创建xml序列化类
		FileStream fileStream = new FileStream(Application.dataPath + "/test.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
		StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8);
		XmlSerializer xml = new XmlSerializer(_testSerialize.GetType());
		// 把_testSerilize序列化到写入流中
		xml.Serialize(sw, _testSerialize);
		sw.Close();
		fileStream.Close();
	}

	TestSerialize XmlDeSerilize() {
		FileStream fs = new FileStream(Application.dataPath + "/test.xml", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		XmlSerializer xs = new XmlSerializer(typeof(TestSerialize));
		TestSerialize testSerialize = (TestSerialize) xs.Deserialize(fs);
		fs.Close();
		return testSerialize;
	}

	void BinarySerialize(TestSerialize _testSerialize) {
		FileStream fs = new FileStream(Application.dataPath + "/test.bytes", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(fs, _testSerialize);
		fs.Close();
	}
	
	TestSerialize BinaryDeSerialize() {
		TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/test.bytes");
		MemoryStream ms = new MemoryStream(textAsset.bytes);
		BinaryFormatter bf = new BinaryFormatter();
		TestSerialize testSerialize = (TestSerialize) bf.Deserialize(ms);
		ms.Close();
		return testSerialize;
	}
}
