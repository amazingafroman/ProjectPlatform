using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LevelMetaData
{
	
	// all the meta elements wanted for the level
	[XmlElement("LevelName")]
	public string levelName;

	[XmlElement("LevelCompleted")]
	public bool levelCompleted;

	[XmlElement("TimeTrialCompleted")]
	public bool TimeTrialCompleted;

	[XmlElement("AllCratesDestroyed")]
	public bool allCratesDestroyed;

	[XmlElement("NoDeath")]
	public bool noDeath;

	public LevelMetaData()
	{

	}
	

	// for labling meta data files.
	// used for creating a new level meta data if one doesnt exist for the passed string
	public LevelMetaData(string n)
	{
		levelName = n;
	}
}
