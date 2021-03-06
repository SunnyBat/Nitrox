﻿using Harmony;
using NitroxClient.MonoBehaviours.Gui.Input;
using NitroxClient.MonoBehaviours.Gui.Input.KeyBindings;
using NitroxModel.Helper;
using System;
using System.Reflection;

namespace NitroxPatcher.Patches
{
    public class Player_Update_Patch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(Player);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("Update", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(Player __instance)
        {
            KeyBindingManager keyBindingManager = new KeyBindingManager();
            
            foreach(KeyBinding keyBinding in keyBindingManager.KeyboardKeyBindings)
            {
                bool isButtonDown = (bool)ReflectionHelper.ReflectionCall(typeof(GameInput), "GetButtonDown", new Type[] { typeof(GameInput.Button) }, true, true, keyBinding.Button);

                if(isButtonDown)
                {
                    keyBinding.Action.Execute();
                }
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            this.PatchPostfix(harmony, TARGET_METHOD);
        }
    }
}
