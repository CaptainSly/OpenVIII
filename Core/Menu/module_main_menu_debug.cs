﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace OpenVIII
{
    public static partial class Module_main_menu_debug
    {
        #region Fields

        private static float fade, lastfade;

        private static bool LastActive = false;

        private static MainMenuStates state = 0;
        private static Vector2 vp_per;
        private static Vector2 vp;
        private static bool blinkstate;
        private static IGM InGameMenu;
        private static IGM_Junction InGameMenu_Junction;
        private static IGM_Items InGameMenu_Items;
        private static IGM_Lobby InGameMenu_Lobby;

        #endregion Fields

        #region Enums

        /// <summary>
        /// What state the menus are in.
        /// </summary>
        private enum MainMenuStates
        {
            //Init,
            MainLobby,
            DebugScreen,
            NewGameChoosed,
            LoadGameChooseSlot,
            SaveGameChooseSlot,
            LoadGameChooseGame,
            SaveGameChooseGame,
            LoadGameCheckingSlot,
            SaveGameCheckingSlot,
            LoadGameLoading,
            SaveGameSaving,
            InGameMenu,
            IGM_Junction,
            IGM_Items
        }

        #endregion Enums

        #region Properties

        public static float vpSpace { get; private set; }
        private static float Fade { get => fade; set => fade = value; }

        private static Dictionary<Enum, Item> strLoadScreen { get; set; }
        private static MainMenuStates State
        {
            get => state; set
            {
                // Draw will call before the next update(). This prevents that.
                Memory.SuppressDraw = true;
                state = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Trigger required draw function.
        /// </summary>
        public static void Draw()
        {
            Memory.graphics.GraphicsDevice.Clear(Color.Black);
            lastfade = fade;
            switch (State)
            {
                //case MainMenuStates.Init:
                case MainMenuStates.MainLobby:
                    //DrawMainLobby();
                    InGameMenu_Lobby.Draw();
                    break;

                case MainMenuStates.DebugScreen:
                    DrawDebugLobby();
                    break;

                //case MainMenuStates.NewGameChoosed:
                //    DrawMainLobby();
                //    break;

                case MainMenuStates.LoadGameLoading:
                case MainMenuStates.LoadGameChooseSlot:
                case MainMenuStates.LoadGameCheckingSlot:
                case MainMenuStates.LoadGameChooseGame:
                case MainMenuStates.SaveGameChooseSlot:
                case MainMenuStates.SaveGameCheckingSlot:
                case MainMenuStates.SaveGameChooseGame:
                case MainMenuStates.SaveGameSaving:
                    DrawLGSG();
                    break;
                case MainMenuStates.InGameMenu:
                    InGameMenu.Draw();
                    break;
                case MainMenuStates.IGM_Junction:
                    InGameMenu_Junction.Draw();
                    break;
                case MainMenuStates.IGM_Items:
                    InGameMenu_Items.Draw();
                    break;
            }
        }

        /// <summary>
        /// Triggers functions depending on state
        /// </summary>
        public static void Update()
        {
            if (blinkstate)
                blink_Amount += Memory.gameTime.ElapsedGameTime.Milliseconds / 2000.0f * 3;
            else
                blink_Amount -= Memory.gameTime.ElapsedGameTime.Milliseconds / 2000.0f * 3;
            lastscale = scale;
            scale = Memory.Scale();
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            bool forceupdate = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            
            if (LastActive != Memory.IsActive)
            {
                forceupdate = true;
                LastActive = Memory.IsActive;
            }
            if (lastscale != scale)
            {
                forceupdate = true;
            }
            if (Fade < 1.0f && State != MainMenuStates.NewGameChoosed)
            {
                Fade += Memory.gameTime.ElapsedGameTime.Milliseconds / 1000.0f * 3;
            }

            vp_per.X = Memory.PreferredViewportWidth;//Memory.graphics.GraphicsDevice.Viewport.Width;
            vp_per.Y = Memory.PreferredViewportHeight;//Memory.graphics.GraphicsDevice.Viewport.Width;
            vp_per = new Vector2(Memory.PreferredViewportWidth, Memory.PreferredViewportHeight);
            vp = new Vector2(Memory.graphics.GraphicsDevice.Viewport.Width, Memory.graphics.GraphicsDevice.Viewport.Height);

            vpSpace = vp_per.Y * 0.09f;
            DFontPos = new Vector2(vp_per.X * .10f, vp_per.Y * .05f) + Offset;

            IGM_focus = Matrix.CreateTranslation((vp_per.X / -2), (vp_per.Y / -2), 0) * 
                Matrix.CreateScale(new Vector3(scale.X, scale.Y, 1)) * 
                Matrix.CreateTranslation(vp.X / 2, vp.Y / 2, 0);

            ml = Input.MouseLocation.Transform(IGM_focus);
            switch (State)
            {
                //case MainMenuStates.Init:
                //    State++;
                //    Init();
                //    break;

                case MainMenuStates.MainLobby:
                    Memory.IsMouseVisible = true;
                    Offset = new Vector2(-1000, 0);
                    //if (UpdateMainLobby() || (lastfade != fade))
                    //{
                    //    forceupdate = true;
                    //}

                    InGameMenu_Lobby.Update();
                    break;

                case MainMenuStates.DebugScreen:
                    Memory.IsMouseVisible = true;
                    if (Offset != Vector2.Zero)
                    {
                        Offset = Vector2.SmoothStep(Offset, Vector2.Zero, .15f).FloorOrCeiling(Vector2.Zero);
                    }
                    if (UpdateDebugLobby() || (lastfade != fade) || Offset != Vector2.Zero)
                    {
                        forceupdate = true;
                    }

                    break;

                //case MainMenuStates.NewGameChoosed:
                //    Memory.IsMouseVisible = false;
                //    UpdateNewGame();
                //    break;

                case MainMenuStates.LoadGameChooseSlot:
                case MainMenuStates.LoadGameCheckingSlot:
                case MainMenuStates.LoadGameChooseGame:
                case MainMenuStates.LoadGameLoading:
                case MainMenuStates.SaveGameChooseSlot:
                case MainMenuStates.SaveGameCheckingSlot:
                case MainMenuStates.SaveGameChooseGame:
                case MainMenuStates.SaveGameSaving:
                    UpdateLGSG();
                    break;
                case MainMenuStates.InGameMenu:
                    Memory.IsMouseVisible = true;
                    InGameMenu.Update();
                    break;
                case MainMenuStates.IGM_Junction:
                    Memory.IsMouseVisible = true;
                    InGameMenu_Junction.Update();
                    break;
                case MainMenuStates.IGM_Items:
                    Memory.IsMouseVisible = true;
                    InGameMenu_Items.Update();
                    break;

                default:
                    goto case 0;
            }
            //disabled because if you resize the window the next update call undoes this before drawing happens.
            //need a way to detect if drawing has happened before suppressing draw again.
            //if(forceupdate)
                

        }

        

        /// <summary>
        /// Init
        /// </summary>
        public static void Init()
        {
            //InitMain();
            InGameMenu_Lobby = new IGM_Lobby();
            InitLoad();
            InitDebug();

            InGameMenu = new IGM();
            InGameMenu_Junction = new IGM_Junction();
            InGameMenu_Items = new IGM_Items();
        }

        #endregion Methods
    }
}