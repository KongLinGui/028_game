using UnityEditor;
using System.Collections;
using InaneGames;
using UnityEngine;
[CanEditMultipleObjects] 

[CustomEditor(typeof(InaneGames.Weapon))] 
public class WeaponEditor : Editor {
	
	public override void OnInspectorGUI() {
		InaneGames.Weapon myTarget = (InaneGames.Weapon) target;
		
		EditorGUILayout.Separator();

		serializedObject.Update();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponType"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("gunSlotIndex"), true);

		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(serializedObject.FindProperty("muzzleFlashGO"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletSpawnPoint"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("rigidBodyForce"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("active"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("knockBackForce"), true);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("gunRange"), true);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponObject"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponTexture"));

		EditorGUILayout.PropertyField(serializedObject.FindProperty("gunMask"));
		EditorGUILayout.Separator();

		
		//EditorGUILayout.BeginVertical("toolbar3");


			myTarget.useAmmo = EditorGUILayout.Foldout(myTarget.useAmmo, "Ammo");
		//	EditorGUILayout.EndVertical();
			if(myTarget.useAmmo)
			{

			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxNomBullets"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("currentNomBullets"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("cooldownTime"), true);

			EditorGUILayout.PropertyField(serializedObject.FindProperty("reloadTime"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("isAutomatic"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("projectilesPerShot"), true);

			
			EditorGUILayout.PropertyField(serializedObject.FindProperty("spread"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("damagePerHit"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("maxNomClips"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("infiniteAmmo"), true);

				//EditorGUILayout.EndVertical();
			}



		//GUILayout.BeginVertical("box");
		myTarget.useSound = EditorGUILayout.Foldout(myTarget.useSound, "Sounds");
	//	EditorGUILayout.EndVertical();

		

		

		if(myTarget.useSound)
		{
		//	GUILayout.BeginVertical("box");
			EditorGUILayout.PropertyField(serializedObject.FindProperty("fireAC"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("reloadAC"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty("emptyAC"), true);
		//	EditorGUILayout.EndVertical();
		}


		if(myTarget.isAutomatic)
		{
			if(myTarget.weaponType == InaneGames.Weapon.WeaponType.WeaponTypeRay)
			{
				//GUILayout.BeginVertical("box");
				myTarget.useDisperse = EditorGUILayout.Foldout(myTarget.useDisperse, "Disperse");
				//EditorGUILayout.EndVertical();

				if(myTarget.useDisperse)
				{

				//	EditorGUILayout.BeginVertical("textField1");
					EditorGUILayout.PropertyField(serializedObject.FindProperty("disperseScalar"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("disperse"), true);
					EditorGUILayout.PropertyField(serializedObject.FindProperty("disperseMax"), true);







				//	EditorGUILayout.EndVertical();
				}
			}
		}

		if(myTarget.weaponType == InaneGames.Weapon.WeaponType.WeaponTypeProjectile)
		{
			//GUILayout.BeginVertical("box");
			myTarget.useProjectile = EditorGUILayout.Foldout(myTarget.useProjectile, "Projectiles");
			//EditorGUILayout.EndVertical();

			if(myTarget.useProjectile)
			{
				//EditorGUILayout.BeginVertical("textField2");
				EditorGUILayout.PropertyField(serializedObject.FindProperty("projectileSpeed"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("projectilePrefab"), true);



				
				//EditorGUILayout.EndVertical();
			}
		}


		if(myTarget.weaponType==Weapon.WeaponType.WeaponTypeRay)
		{
			//GUILayout.BeginVertical("box");
			myTarget.useShell = EditorGUILayout.Foldout(myTarget.useShell, "Shells");
			//EditorGUILayout.EndVertical();
			if(myTarget.useShell)
			{
//				EditorGUILayout.BeginVertical("textField2");
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellGameObject"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellSpawn"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellMinForce"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellUpForce"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellTorque"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("shellTorqueRandom"), true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("hitEffectGO"), true);


			//	EditorGUILayout.EndVertical();
			}
		}
		EditorGUILayout.PropertyField(serializedObject.FindProperty("laserMat"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("beamRadius"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("tracerFadeTime"), true);

		if(myTarget.weaponType == InaneGames.Weapon.WeaponType.WeaponTypeBeam)
		{
			//GUILayout.BeginVertical("box");
			myTarget.useShell = EditorGUILayout.Foldout(myTarget.useShell, "Beam");
			//EditorGUILayout.EndVertical();
			if(myTarget.useBeam)
			{
			//	EditorGUILayout.BeginVertical("textField2");

				EditorGUILayout.PropertyField(serializedObject.FindProperty("hitEffectGO"), true);
	

			//	EditorGUILayout.EndVertical();
			}
		}
		serializedObject.ApplyModifiedProperties();



		EditorUtility.SetDirty(target);
	}
}
