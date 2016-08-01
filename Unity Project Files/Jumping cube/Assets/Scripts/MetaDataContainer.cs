using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("MetaDataContainer")]
public class MetaDataContainer {

	public static MetaDataContainer metaDataContianer;

	
	[XmlArray("MetaDatas")]
	[XmlArrayItem("MD")]

	public List<LevelMetaData> levelMetaDatas = new List<LevelMetaData>();

	string name;
	
	//use for file location
	string filePathName = /*Application.persistentDataPath +*/ "D:/levelGoals.xml";

	void Start () {

	}

	public MetaDataContainer() 
	{	
	
	}

	//function for adding data to the save???
	// function to return metadata for requested level
	// if no metadata is available then it will create metadata for it
	public LevelMetaData GetLevelProgression(string levelName) {
		LevelMetaData lmd = levelMetaDatas.Find (x => x.levelName == levelName);

		if (lmd != null) 
		{
			return lmd;
		} 
		else 
		{	
			levelMetaDatas.Add (new LevelMetaData(levelName));
			Save ();
			return GetLevelProgression(levelName);
		}
	}
	
	// loads the entire metadata file so everything can be accessed
	public void Load(){
		if(File.Exists(filePathName)){
			string meta = File.ReadAllText (filePathName);
			XmlSerializer serializer  = new XmlSerializer (typeof(MetaDataContainer));
			StringReader stringReader = new StringReader(meta);
			MetaDataContainer mdc = serializer.Deserialize(stringReader) as MetaDataContainer;
			levelMetaDatas = mdc.levelMetaDatas;
		} 
		else 
		{
			Save();
		}
	}
	//saves the meta data files 
	public void Save(){
		XmlSerializer serializer = new XmlSerializer (typeof(MetaDataContainer));
		FileStream stream = new FileStream (filePathName, FileMode.Create);
		serializer.Serialize (stream, this);
		stream.Close ();
		Debug.Log ("MetaData Saved");
	}

}
