using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Invector;
using RopeMinikit;
using CessilCellsCeaChells.CeaChore;
using System.Collections;
using UnityEngine;

[assembly: RequiresMethod(typeof(CorruptDrone), "Start", typeof(void), new Type[] {})]
[assembly: RequiresMethod(typeof(CorruptExcutor), "Start", typeof(void), new Type[] {})]
[assembly: RequiresMethod(typeof(CorruptPredator), "Start", typeof(void), new Type[] {})]

namespace HPMult 
{
	
	struct Multipliers 
	{
		public float CorruptCenti;
		public float CorruptDrone;
		public float CorruptExecutor;
		public float CorruptFairyMan;
		public float CorruptFrame;
		public float CorruptFrameGrappler;
		public float CorruptMono;
		public float CorruptPredator;
		public float CorruptRaptor;
		public float CorruptWanderer;
	}
	[BepInPlugin("tairasoul.hpmult", "HPMult", "1.0.0")]
	class Plugin : BaseUnityPlugin 
	{
		internal static Multipliers mults;
		internal static ManualLogSource Log;
		
		void Awake() 
		{
			Log = Logger;
			ConfigEntry<float> centi = Config.Bind("Multipliers", "CorruptCenti", 1f);
			ConfigEntry<float> drone = Config.Bind("Multipliers", "CorruptDrone", 1f);
			ConfigEntry<float> executor = Config.Bind("Multipliers", "CorruptFairyMan", 1f);
			ConfigEntry<float> fairy = Config.Bind("Multipliers", "CorruptExecutor", 1f);
			ConfigEntry<float> frame = Config.Bind("Multipliers", "CorruptFrame", 1f);
			ConfigEntry<float> grappler = Config.Bind("Multipliers", "CorruptFrameGrappler", 1f);
			ConfigEntry<float> mono = Config.Bind("Multipliers", "CorruptMono", 1f);
			ConfigEntry<float> predator = Config.Bind("Multipliers", "CorruptPredator", 1f);
			ConfigEntry<float> raptor = Config.Bind("Multipliers", "CorruptRaptor", 1f);
			ConfigEntry<float> wanderer = Config.Bind("Multipliers", "CorruptWanderer", 1f);
			mults.CorruptCenti = centi.Value;
			mults.CorruptDrone = drone.Value;
			mults.CorruptExecutor = executor.Value;
			mults.CorruptFairyMan = fairy.Value;
			mults.CorruptFrame = frame.Value;
			mults.CorruptFrameGrappler = grappler.Value;
			mults.CorruptMono = mono.Value;
			mults.CorruptPredator = predator.Value;
			mults.CorruptRaptor = raptor.Value;
			mults.CorruptWanderer = wanderer.Value;
		}
		
		void Start() 
		{
			Harmony harmony = new("hpmults");
			harmony.PatchAll();
		}
	}
	
	[HarmonyPatch]
	class EnemyPatch 
	{
		static IEnumerable<MethodBase> TargetMethods()
		{
			Type[] targetTypes = {
				typeof(CorruptCenti),
				typeof(CorruptDrone),
				typeof(CorruptExcutor),
				typeof(CorruptFairyMan),
				typeof(CorruptFrame),
				typeof(CorruptFrameGrappler),
				typeof(CorruptMono),
				typeof(CorruptPredator),
				typeof(CorruptRaptor),
				typeof(CorruptWanderer),
				typeof(CorruptVolture)
			};

			foreach (Type targetType in targetTypes)
			{
				Plugin.Log.LogInfo($"Patching Start on {targetType.Name}");
				yield return AccessTools.Method(targetType, "Start");
			}
		}
		
		static IEnumerator ApplyHealthMult(object __instance) 
		{
			if (__instance.GetType() == typeof(CorruptCenti)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptCenti instance = __instance as CorruptCenti;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp.hp;
				Plugin.Log.LogInfo(controller);
				instance.hp.maxHp = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptCenti);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptDrone)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptDrone instance = __instance as CorruptDrone;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptExcutor)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptExcutor instance = __instance as CorruptExcutor;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.core;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptExecutor);
				controller.ChangeHealth(controller.maxHealth);
				vHealthController[] limbs = instance.bodyparts;
				foreach (vHealthController controller1 in limbs) 
				{
					controller1.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptExecutor);
				}
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptFairyMan)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptFairyMan instance = __instance as CorruptFairyMan;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptFrame)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptFrame instance = __instance as CorruptFrame;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.bar.hp;
				Plugin.Log.LogInfo(controller);
				instance.bar.maxHp = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptFrameGrappler)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptFrameGrappler instance = __instance as CorruptFrameGrappler;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.bar.hp;
				Plugin.Log.LogInfo(controller);
				instance.bar.maxHp = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptMono)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptMono instance = __instance as CorruptMono;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptPredator)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptPredator instance = __instance as CorruptPredator;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptRaptor)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptRaptor instance = __instance as CorruptRaptor;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp;
				Plugin.Log.LogInfo(controller);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
			if (__instance.GetType() == typeof(CorruptWanderer)) 
			{
				yield return new WaitForSeconds(0.5f);
				CorruptWanderer instance = __instance as CorruptWanderer;
				Plugin.Log.LogInfo(instance);
				vHealthController controller = instance.hp.hp;
				Plugin.Log.LogInfo(controller);
				instance.hp.maxHp = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.maxHealth = (int)(controller.maxHealth * Plugin.mults.CorruptDrone);
				controller.ChangeHealth(controller.maxHealth);
				yield break;
			}
		}
		
		[HarmonyPrefix]
		static void StartPrefix(MonoBehaviour __instance) 
		{
			__instance.StartCoroutine(ApplyHealthMult(__instance));
		}
	}
}