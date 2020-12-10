﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

using BDLauncherCSharp.Controls;
using BDLauncherCSharp.Data;
using BDLauncherCSharp.Data.Model;
using BDLauncherCSharp.Extensions;

namespace BDLauncherCSharp
{
    /// <summary>
    /// SaveLoaderDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SaveLoaderDialog : GDialog
    {
        public SaveLoaderDialog()
        {
            InitializeComponent();
            this.I18NInitialize();
            SaveList.ItemsSource = OverAll.SavedGameDirectory.GetFiles("*.sav").Select(SavedGameExtension.GetSavedGameInfo);
        }

        protected override async void PrimaryButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(SaveList.SelectedItem is SavedGameInfo ksi))
                return;// 这里永远也不会遇到
                       // MessageBox.Show(I18NExtension.I18N("msgNoSaveLoadedError"), I18NExtension.I18N("msgCaptain"));

            try
            {
                using (var sw = new StreamWriter(OverAll.SpawnIni.OpenWrite()))
                {
                    await sw.WriteLineAsync(string.Format(
                        ";generated by Singleplayer Campaign Launcher{0}" +
                        "[Settings]{0}" +
                        "Scenario=spawnmap.ini{0}" +
                        "SaveGameName={1}{0}" +
                        "LoadSaveGame=Yes{0}" +
                        "SidebarHack=False{0}" +
                        "Firestorm=No{0}" +
                        "GameSpeed=2{0}",
                        Environment.NewLine,
                        ksi.RealFile.Name));
                    await sw.FlushAsync();
                }
                base.PrimaryButton_Click(sender, e);
            }
            catch (Exception ex)
            {
                // async void 方法将不会向上抛出异常
                throw;
            }
        }
    }
}