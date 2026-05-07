using System.IO;
using UnityEngine;
using UnityEditor.AssetImporters;

[ScriptedImporter(1, "j2")]
public class J2Importer : ScriptedImporter
{
	public override void OnImportAsset(AssetImportContext ctx)
	{
		string contents = File.ReadAllText(ctx.assetPath);
		TextAsset textAsset = new TextAsset(contents);
		ctx.AddObjectToAsset("main", textAsset);
		ctx.SetMainObject(textAsset);
	}
}
