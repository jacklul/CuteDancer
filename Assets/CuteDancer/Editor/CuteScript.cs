﻿#if VRC_SDK_VRCSDK3
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using AvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;

namespace VRF
{
    public class CuteScript : EditorWindow
    {
        AvatarDescriptor avatar = null;
        CutePrefab cutePrefab = new CutePrefab();
        CuteParams cuteParams = new CuteParams();
        CuteSubmenu cuteSubmenu = new CuteSubmenu();
        CuteLayers cuteLayers = new CuteLayers();

        Vector2 scroll = new Vector2(0, 0);
        bool showPrefab = true;
        bool showParameters = true;
        bool showSubmenu = true;
        bool showLayers = true;

        [MenuItem("Tools/CuteDancer Setup")]
        static void Init()
        {
            CuteScript window = (CuteScript)EditorWindow.GetWindow(typeof(CuteScript));
            window.minSize = new Vector2(500, 400);
            window.titleContent.text = "CuteDancer Script";
            window.Show();
        }

        void OnGUI()
        {
            GUIStyle titleStyle = new GUIStyle();
            titleStyle.fontSize = 20;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.normal.textColor = EditorStyles.boldLabel.normal.textColor;
            titleStyle.margin.top = 15;
            titleStyle.margin.bottom = 15;

            GUIStyle labelStyle = new GUIStyle(EditorStyles.largeLabel);
            labelStyle.wordWrap = true;

            GUILayout.Label("CuteDancer <dev version>", titleStyle);

            GUILayout.Label("Select your main avatar object from scene.\n\n" +
                "The script will try to fill fields in sections below basing on data found on your avatar.\n\n" +
                "Please review them before applying any changes!", labelStyle);

            GUILayout.Space(10);
            AvatarDescriptor newAvatar = EditorGUILayout.ObjectField("Avatar", avatar, typeof(AvatarDescriptor), true) as AvatarDescriptor;
            GUILayout.Space(10);

            if (avatar != newAvatar)
            {
                HandleAvatarChange(newAvatar);
            }

            scroll = GUILayout.BeginScrollView(scroll);
            RenderGroup("Prefabs", cutePrefab, ref showPrefab);
            RenderGroup("Expression parameters", cuteParams, ref showParameters);
            RenderGroup("Submenu", cuteSubmenu, ref showSubmenu);
            RenderGroup("Animator layers", cuteLayers, ref showLayers);
            GUILayout.EndScrollView();

        }

        void HandleAvatarChange(AvatarDescriptor newAvatar)
        {
            if (newAvatar == null)
            {
                avatar = null;
                cutePrefab.ClearForm();
                cuteParams.ClearForm();
                cuteSubmenu.ClearForm();
                cuteLayers.ClearForm();
            }
            else
            {
                avatar = newAvatar;
                cutePrefab.SetAvatar(avatar);
                cuteParams.SetAvatar(avatar);
                cuteSubmenu.SetAvatar(avatar);
                cuteLayers.SetAvatar(avatar);
            }
        }

        void RenderGroup(string title, CuteGroup group, ref bool visibility)
        {
            Separator();
            visibility = EditorGUILayout.BeginFoldoutHeaderGroup(visibility, title);
            if (visibility)
            {
                group.RenderForm();
                group.RenderStatus();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        void Separator()
        {
            GUILayout.Space(5);
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(2));
            r.height = 2;
            r.x -= 10;
            r.width += 20;
            EditorGUI.DrawRect(r, new Color(0, 0, 0, 0.15f));
            GUILayout.Space(5);
        }
    }
}

#endif