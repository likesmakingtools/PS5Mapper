using System.Runtime.InteropServices;
using System.Windows;
using SharpDX.DirectInput;
using SDL2;
//using static System.Net.WebRequestMethods;
//using System.Windows.Media.Media3D;
//using System.Net.Security;

namespace PS5Mapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly object _lock = new object();
        private bool _shouldRun = false;
        private bool _isRunning = false;
        private bool _hasGamepad = false;
        private bool _useSDL = true;
        private bool _isPaused = false;
        private bool _useSendInput = true;
        private bool _usePostMessage = false;

        private const string zeldaIIWindowName = "GameMaker: Studio";
        private const string am2rWindowName = "AM2R";
        private const string caveStoryWindowName = "Cave Story: Tweaked";
        private const string hollowKnightWindowName = "Hollow Knight";
        private const string minoriaWindowName = "Minoria";
        private const string lostRuinsWindowName = "LostRuins";
        private const string bloodStainedWindowName = "Bloodstained: Ritual of the Night";
        private string windowName = string.Empty;
        private string gameInfo = "no game window.";
        private string controllerInfo = "no controller connected yet.";
        private string mapInfo1 = string.Empty;
        private string mapValues1 = string.Empty;
        private string mapTarget1 = string.Empty;
        private string mapInfo2 = string.Empty;
        private string mapValues2 = string.Empty;
        private string mapTarget2 = string.Empty;
        private string runningInfo = "not running.";
        private string debugInfo = string.Empty;

        private IntPtr window = IntPtr.Zero;
        private Joystick? gamepadDI = null;
        private List<Joystick> gamepadsDI = new List<Joystick>();
        private IntPtr gamepadSDL = IntPtr.Zero;
        private List<IntPtr> gamepadsSDL = new List<IntPtr>();
        private int gamepadIndexDI = -1;
        private int gamepadIndexSDL = -1;
        private int deadzone = 20000;
        private ControllerInfo gamepadStateSDL;
        private VirtualKeys VK = new VirtualKeys();

        private int WM_KEYDOWN = 0x100;
        private int WM_KEYUP = 0x101;
        private int VK_UP = 0x26; // D-Pad Up
        private int VK_DOWN = 0x28; // D-Pad Down
        private int VK_LEFT = 0x25; // D-Pad Left
        private int VK_RIGHT = 0x27; // D-Pad Right
        private int VK_O = 0x4F; // PS Button for Options in Zelda

        private Game game;

        public struct ControllerInfo
        {
            public bool LStickUp;
            public bool LStickDown;
            public short LStickValue_V;
            public bool LStickLeft;
            public bool LStickRight;
            public short LStickValue_H;
            public bool RStickUp;
            public bool RStickDown;
            public short RStickValue_V;
            public bool RStickLeft;
            public bool RStickRight;
            public short RStickValue_H;
            public bool DpadUp;
            public bool DpadDown;
            public bool DpadLeft;
            public bool DpadRight;
            public bool L1;
            public bool L2;
            public short L2Value;
            public bool L3;
            public bool R1;
            public bool R2;
            public short R2Value;
            public bool R3;
            public bool Square;
            public bool Triangle;
            public bool Circle;
            public bool X;
            public bool Options;
            public bool Share;
            public bool PSButton;
            public bool Touchpad;
            public bool Mic;
        }

        public MainWindow()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        private void GameRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbZeldaII.IsChecked == true) { windowName = zeldaIIWindowName; game = Game.ZeldaII; VK.SetGame(game); }
            else if (rbAM2R.IsChecked == true) { windowName = am2rWindowName; game = Game.AM2R; VK.SetGame(game); }
            else if (rbCaveStory.IsChecked == true) { windowName = caveStoryWindowName; game = Game.CaveStory; VK.SetGame(game); }
            else if (rbHK.IsChecked == true) { windowName = hollowKnightWindowName; game = Game.HollowKnight; VK.SetGame(game); }
            else if (rbMinoria.IsChecked == true) { windowName = minoriaWindowName; game = Game.Minoria; VK.SetGame(game); }
            else if (rbLostRuins.IsChecked == true) { windowName = lostRuinsWindowName; game = Game.LostRuins; VK.SetGame(game); }
            else if (rbBSRN.IsChecked == true) { windowName = bloodStainedWindowName; game = Game.BloodStained; VK.SetGame(game); }

            mapTarget1 =
                $"{VK.Active.DUPMap}\n" +
                $"{VK.Active.DDOWNMap}\n" +
                $"{VK.Active.DLEFTMap}\n" +
                $"{VK.Active.DRIGHTMap}\n" +
                $"{VK.Active.LSUPMap}\n" +
                $"{VK.Active.LSDOWNMap}\n" +
                $"{VK.Active.LSLEFTMap}\n" +
                $"{VK.Active.LSRIGHTMap}\n" +
                $"{VK.Active.L1Map}\n" +
                $"{VK.Active.L2Map}\n" +
                $"{VK.Active.L3Map}\n" +
                $"{VK.Active.TouchpadMap}\n" +
                $"{VK.Active.ShareMap}\n" +
                $"{VK.Active.PSMap}";

            mapTarget2 =
                $"{VK.Active.TriangleMap}\n" +
                $"{VK.Active.SquareMap}\n" +
                $"{VK.Active.CircleMap}\n" +
                $"{VK.Active.XMap}\n" +
                $"{VK.Active.RSUPMap}\n" +
                $"{VK.Active.RSDOWNMap}\n" +
                $"{VK.Active.RSLEFTMap}\n" +
                $"{VK.Active.RSRIGHTMap}\n" +
                $"{VK.Active.R1Map}\n" +
                $"{VK.Active.R2Map}\n" +
                $"{VK.Active.R3Map}\n" +
                $"{VK.Active.OptionsMap}\n" +
                $"{VK.Active.MicMap}";
        }

        private void CoreRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rbSDL.IsChecked == true) { _useSDL = true; }
            else if (rbDirectInput.IsChecked == true) { _useSDL = false; }
        }

        private void ControllerRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnConnectGame_Click(object sender, RoutedEventArgs e)
        {
            IntPtr tmpWindow = FindWindow(null, windowName);
            if (tmpWindow == IntPtr.Zero)
            {
                window = IntPtr.Zero;
                gameInfo = "No Game Window found.";
            }
            else
            {
                window = tmpWindow;
                gameInfo = $"Game Window found.\nWindow Name: {windowName}\n{tmpWindow}";
            }

            UpdateDisplay();
        }

        private void btnConnectController_Click(object sender, RoutedEventArgs e)
        {
            if (_useSDL)
            {
                _hasGamepad = ConnectController_SDL();
            }
            else
            {
                _hasGamepad = ConnectController_DirectInput();
            }

            UpdateDisplay();
        }

        private bool ConnectController_SDL()
        {
            bool isConnected = false;
            int numJoysticks = 0;
            controllerInfo = string.Empty;

            gamepadsSDL.Clear();

            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_JOYSTICK) < 0)
            {
                controllerInfo = $"couldn't initialize SDL: {SDL.SDL_GetError()}\n";
            }

            numJoysticks = SDL.SDL_NumJoysticks();

            if (numJoysticks > 0)
            {
                SDL.SDL_JoystickEventState(SDL.SDL_ENABLE);

                if (gamepadIndexSDL + 2 > numJoysticks) { gamepadIndexSDL = -1; }
                gamepadIndexSDL++;
                gamepadSDL = SDL.SDL_JoystickOpen(gamepadIndexSDL);
                string name = SDL.SDL_JoystickName(gamepadSDL);
                string guid = SDL.SDL_JoystickGetGUID(gamepadSDL).ToString();
                controllerInfo += $"SDL gamepad name: {name}\nGuid: {guid}\ngamepad {gamepadIndexSDL + 1} acquired.";

                isConnected = true;
            }
            else
            {
                controllerInfo += "no controller found.";
            }

            return isConnected;
        }

        private bool ConnectController_DirectInput()
        {
            bool isConnected = false;

            DirectInput di = new DirectInput();
            gamepadsDI.Clear();
            foreach (DeviceInstance device in di.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                gamepadsDI.Add(new Joystick(di, device.InstanceGuid));
            }
            foreach (DeviceInstance device in di.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                gamepadsDI.Add(new Joystick(di, device.InstanceGuid));
            }

            if (gamepadsDI.Count > 0)
            {
                if (gamepadIndexDI + 2 > gamepadsDI.Count) { gamepadIndexDI = -1; }
                gamepadIndexDI++;
                gamepadDI = gamepadsDI[gamepadIndexDI];
                gamepadDI.Properties.BufferSize = 128;
                gamepadDI.Acquire();
                controllerInfo = $"DI gamepad name: {gamepadDI.Information.InstanceName}\nGuid: {gamepadDI.Information.InstanceGuid}\ngamepad {gamepadIndexDI + 1} acquired.";
                isConnected = true;
            }
            else
            {
                controllerInfo = "no controller found.";
            }

            return isConnected;
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //if (window != IntPtr.Zero && gamepadDI != null)
            if (window != IntPtr.Zero && _hasGamepad)
            {
                runningInfo = "running";
                lock(_lock)
                {
                    _shouldRun = true;
                }

                await Task.Run(MappingThread);

                UpdateDisplay();
            }
            else
            {
                StopMapping();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopMapping();
            UpdateDisplay();
        }

        private void StopMapping()
        {
            lock(_lock)
            {
                _shouldRun = false;
            }
        }

        private void cbPause_Checked(object sender, RoutedEventArgs e)
        {
            SetPause();
        }

        private void psPause_Pressed()
        {
            if (cbPause.IsChecked == true) cbPause.IsChecked = false;
            else cbPause.IsChecked = true;
            UpdateDisplay();
            SetPause();
        }

        private void SetPause()
        {
            if (cbPause.IsChecked == true) _isPaused = true;
            else _isPaused = false;
        }

        private void UpdateDisplay()
        {
            tbGameInfo.Text = gameInfo;
            tbControllerInfo.Text = controllerInfo;
            tbMapInfo1.Text = mapInfo1;
            tbMapValues1.Text = mapValues1;
            tbMapTarget1.Text = mapTarget1;
            tbMapInfo2.Text = mapInfo2;
            tbMapValues2.Text = mapValues2;
            tbMapTarget2.Text = mapTarget2;
            tbRunningInfo.Text = runningInfo;
            //tbDebugInfo.Text = debugInfo;
        }

        private void MappingThread()
        {
            lock(_lock)
            {
                _isRunning = true;
            }

            // current button states for DirectInput
            ControllerInfo current = new ControllerInfo();
            // previous button states for DirectInput
            ControllerInfo previous = new ControllerInfo();

            while (true)
            {
                if (!_isRunning) break;
                if (!_hasGamepad) break;
                //if (gamepadDI == null) break;

                try
                {
                    if (_useSDL)
                    {
                        PollController_SDL();
                    }
                    else
                    {
                        current = PollController_DirectInput();

                        // up check
                        if (previous.DpadUp == false && current.DpadUp == true) { PostMessage(window, WM_KEYDOWN, VK_UP); }
                        if (previous.DpadUp == true && current.DpadUp == false) { PostMessage(window, WM_KEYUP, VK_UP); }

                        // down check
                        if (previous.DpadDown == false && current.DpadDown == true) { PostMessage(window, WM_KEYDOWN, VK_DOWN); }
                        if (previous.DpadDown == true && current.DpadDown == false) { PostMessage(window, WM_KEYUP, VK_DOWN); }

                        // left check
                        if (previous.DpadLeft == false && current.DpadLeft == true) { PostMessage(window, WM_KEYDOWN, VK_LEFT); }
                        if (previous.DpadLeft == true && current.DpadLeft == false) { PostMessage(window, WM_KEYUP, VK_LEFT); }

                        // right check
                        if (previous.DpadRight == false && current.DpadRight == true) { PostMessage(window, WM_KEYDOWN, VK_RIGHT); }
                        if (previous.DpadRight == true && current.DpadRight == false) { PostMessage(window, WM_KEYUP, VK_RIGHT); }

                        // set previous values as current for next iteration
                        previous = current;

                        // Options / PS Button
                        if (windowName == zeldaIIWindowName && current.Options)
                        {
                            // send key pressed event
                            PostMessage(window, WM_KEYDOWN, VK_O);
                            PostMessage(window, WM_KEYUP, VK_O);
                        }
                    }
                }
                catch(Exception ex)
                {
                    runningInfo = $"Exception caught: {ex.Message}";
                }

                if (_useSDL)
                {
                    mapInfo1 =
                        $"Up:\nDown:\nLeft:\nRight:\n" +
                        $"LStick Up:\nLStick Down:\nLStick Left:\nLStick Right:\n" +
                        $"L1:\nL2:\nL3:\nTouchpad:\nShare:\nPS:";

                    mapValues1 =
                        $"{gamepadStateSDL.DpadUp}\n" +
                        $"{gamepadStateSDL.DpadDown}\n" +
                        $"{gamepadStateSDL.DpadLeft}\n" +
                        $"{gamepadStateSDL.DpadRight}\n" +
                        $"{gamepadStateSDL.LStickUp} {gamepadStateSDL.LStickValue_V}\n" +
                        $"{gamepadStateSDL.LStickDown} {gamepadStateSDL.LStickValue_V}\n" +
                        $"{gamepadStateSDL.LStickLeft} {gamepadStateSDL.LStickValue_H}\n" +
                        $"{gamepadStateSDL.LStickRight} {gamepadStateSDL.LStickValue_H}\n" +
                        $"{gamepadStateSDL.L1}\n" +
                        $"{gamepadStateSDL.L2} {gamepadStateSDL.L2Value}\n" +
                        $"{gamepadStateSDL.L3}\n" +
                        $"{gamepadStateSDL.Touchpad}\n" +
                        $"{gamepadStateSDL.Share}\n" +
                        $"{gamepadStateSDL.PSButton}";

                    mapInfo2 =
                        $"Triangle:\nSquare:\nCircle:\nX:\n" +
                        $"RStick Up:\nRStick Down:\nRStick Left:\nRStick Right:\n" +
                        $"R1:\nR2:\nR3:\nOptions:\nMic:";

                    mapValues2 =
                        $"{gamepadStateSDL.Triangle}\n" +
                        $"{gamepadStateSDL.Square}\n" +
                        $"{gamepadStateSDL.Circle}\n" +
                        $"{gamepadStateSDL.X}\n" +
                        $"{gamepadStateSDL.RStickUp} {gamepadStateSDL.RStickValue_V}\n" +
                        $"{gamepadStateSDL.RStickDown} {gamepadStateSDL.RStickValue_V}\n" +
                        $"{gamepadStateSDL.RStickLeft} {gamepadStateSDL.RStickValue_H}\n" +
                        $"{gamepadStateSDL.RStickRight} {gamepadStateSDL.RStickValue_H}\n" +
                        $"{gamepadStateSDL.R1}\n" +
                        $"{gamepadStateSDL.R2} {gamepadStateSDL.R2Value}\n" +
                        $"{gamepadStateSDL.R3}\n" +
                        $"{gamepadStateSDL.Options}\n" +
                        $"{gamepadStateSDL.Mic}";
                }
                else
                {
                    // update mapping info for display
                    mapInfo1 = $"\n" +
                        $"Up:\n" +
                        $"Down:\n" +
                        $"Left:\n" +
                        $"Right:";

                    mapValues1 = $"\n" +
                        $"{previous.DpadUp}\n" +
                        $"{previous.DpadDown}\n" +
                        $"{previous.DpadLeft}\n" +
                        $"{previous.DpadRight}";
                }

                Dispatcher.Invoke(UpdateDisplay);

                if(!_shouldRun)
                {
                    lock (_lock)
                    {
                        _isRunning = false;
                    }
                }
            }

            // update info
            mapInfo1 = string.Empty;
            mapValues1 = string.Empty;
            mapTarget1 = string.Empty;
            mapInfo2 = string.Empty;
            mapValues2 = string.Empty;
            mapTarget2 = string.Empty;
            runningInfo = "not running";
            Dispatcher.Invoke(UpdateDisplay);
            if (_useSDL)
            {
                try
                {
                    SDL.SDL_JoystickClose(gamepadSDL);
                }
                catch(Exception e)
                {
                    runningInfo += $"\n{e.Message}";
                }
            }
        }

        private void PollController_SDL()
        {
            SDL.SDL_Event sdlEvent;
            SDL.SDL_PollEvent(out sdlEvent);

            SDL.SDL_ControllerAxisEvent caxis = sdlEvent.caxis;
            SDL.SDL_ControllerButtonEvent cbutton = sdlEvent.cbutton;

            if (cbutton.type == SDL.SDL_EventType.SDL_JOYBUTTONDOWN)
            {
                if (cbutton.button == 11) { gamepadStateSDL.DpadUp = true; PostMessage(window, WM_KEYDOWN, VK.Active.DUP); }
                if (cbutton.button == 12) { gamepadStateSDL.DpadDown = true; PostMessage(window, WM_KEYDOWN, VK.Active.DDOWN); }
                if (cbutton.button == 13) { gamepadStateSDL.DpadLeft = true; PostMessage(window, WM_KEYDOWN, VK.Active.DLEFT); }
                if (cbutton.button == 14) { gamepadStateSDL.DpadRight = true; PostMessage(window, WM_KEYDOWN, VK.Active.DRIGHT); }
                if (cbutton.button == 9) { gamepadStateSDL.L1 = true; PostMessage(window, WM_KEYDOWN, VK.Active.L1); }
                if (cbutton.button == 7) { gamepadStateSDL.L3 = true; PostMessage(window, WM_KEYDOWN, VK.Active.L3); }
                if (cbutton.button == 10) { gamepadStateSDL.R1 = true; PostMessage(window, WM_KEYDOWN, VK.Active.R1); }
                if (cbutton.button == 8) { gamepadStateSDL.R3 = true; PostMessage(window, WM_KEYDOWN, VK.Active.R3); }
                if (cbutton.button == 3) { gamepadStateSDL.Triangle = true; PostMessage(window, WM_KEYDOWN, VK.Active.Triangle); }
                if (cbutton.button == 1) { gamepadStateSDL.Circle = true; PostMessage(window, WM_KEYDOWN, VK.Active.Circle); }
                if (cbutton.button == 2) { gamepadStateSDL.Square = true; PostMessage(window, WM_KEYDOWN, VK.Active.Square); }
                if (cbutton.button == 0) { gamepadStateSDL.X = true; PostMessage(window, WM_KEYDOWN, VK.Active.X); }
                if (cbutton.button == 6) { gamepadStateSDL.Options = true; PostMessage(window, WM_KEYDOWN, VK.Active.Options); }
                if (cbutton.button == 15) { gamepadStateSDL.Touchpad = true; PostMessage(window, WM_KEYDOWN, VK.Active.Touchpad); }
                if (cbutton.button == 4) { gamepadStateSDL.Share = true; Dispatcher.Invoke(psPause_Pressed); }
                if (cbutton.button == 5) { gamepadStateSDL.PSButton = true; PostMessage(window, WM_KEYDOWN, VK.Active.PS); }
                if (cbutton.button == 16) { gamepadStateSDL.Mic = true; PostMessage(window, WM_KEYDOWN, VK.Active.Mic); }
            }
            if (cbutton.type == SDL.SDL_EventType.SDL_JOYBUTTONUP)
            {
                if (cbutton.button == 11) { gamepadStateSDL.DpadUp = false; PostMessage(window, WM_KEYUP, VK.Active.DUP); }
                if (cbutton.button == 12) { gamepadStateSDL.DpadDown = false; PostMessage(window, WM_KEYUP, VK.Active.DDOWN); }
                if (cbutton.button == 13) { gamepadStateSDL.DpadLeft = false; PostMessage(window, WM_KEYUP, VK.Active.DLEFT); }
                if (cbutton.button == 14) { gamepadStateSDL.DpadRight = false; PostMessage(window, WM_KEYUP, VK.Active.DRIGHT); }
                if (cbutton.button == 9) { gamepadStateSDL.L1 = false; PostMessage(window, WM_KEYUP, VK.Active.L1); }
                if (cbutton.button == 7) { gamepadStateSDL.L3 = false; PostMessage(window, WM_KEYUP, VK.Active.L3); }
                if (cbutton.button == 10) { gamepadStateSDL.R1 = false; PostMessage(window, WM_KEYUP, VK.Active.R1); }
                if (cbutton.button == 8) { gamepadStateSDL.R3 = false; PostMessage(window, WM_KEYUP, VK.Active.R3); }
                if (cbutton.button == 3) { gamepadStateSDL.Triangle = false; PostMessage(window, WM_KEYUP, VK.Active.Triangle); }
                if (cbutton.button == 1) { gamepadStateSDL.Circle = false; PostMessage(window, WM_KEYUP, VK.Active.Circle); }
                if (cbutton.button == 2) { gamepadStateSDL.Square = false; PostMessage(window, WM_KEYUP, VK.Active.Square); }
                if (cbutton.button == 0) { gamepadStateSDL.X = false; PostMessage(window, WM_KEYUP, VK.Active.X); }
                if (cbutton.button == 6) { gamepadStateSDL.Options = false; PostMessage(window, WM_KEYUP, VK.Active.Options); }
                if (cbutton.button == 15) { gamepadStateSDL.Touchpad = false; PostMessage(window, WM_KEYUP, VK.Active.Touchpad); }
                if (cbutton.button == 4) { gamepadStateSDL.Share = false; }
                if (cbutton.button == 5) { gamepadStateSDL.PSButton = false; PostMessage(window, WM_KEYUP, VK.Active.PS); }
                if (cbutton.button == 16) { gamepadStateSDL.Mic = false; PostMessage(window, WM_KEYUP, VK.Active.Mic); }
            }
            if (caxis.type == SDL.SDL_EventType.SDL_JOYAXISMOTION)
            {
                if (caxis.axis == 4) // L2
                {
                    gamepadStateSDL.L2Value = caxis.axisValue;
                    if (caxis.axisValue >= 32767 && !gamepadStateSDL.L2) { gamepadStateSDL.L2 = true; PostMessage(window, WM_KEYDOWN, VK.Active.L2); }
                    if (caxis.axisValue < 32767 && gamepadStateSDL.L2) { gamepadStateSDL.L2 = false; PostMessage(window, WM_KEYUP, VK.Active.L2); }
                }
                if (caxis.axis == 5) // R2
                {
                    gamepadStateSDL.R2Value = caxis.axisValue;
                    if (caxis.axisValue >= 32767 && !gamepadStateSDL.R2) { gamepadStateSDL.R2 = true; PostMessage(window, WM_KEYDOWN, VK.Active.R2); }
                    if (caxis.axisValue < 32767 && gamepadStateSDL.R2) { gamepadStateSDL.R2 = false; PostMessage(window, WM_KEYUP, VK.Active.R2); }
                }
                if (caxis.axis == 0) // Left Stick Left/Right
                {
                    gamepadStateSDL.LStickValue_H = caxis.axisValue;
                    if (caxis.axisValue >= deadzone && !gamepadStateSDL.LStickRight) { gamepadStateSDL.LStickRight = true; PostMessage(window, WM_KEYDOWN, VK.Active.LSRIGHT); }
                    //if (caxis.axisValue > 0 && caxis.axisValue < deadzone && gamepadStateSDL.LStickRight) { gamepadStateSDL.LStickRight = false; PostMessage(window, WM_KEYUP, VK.Active.LSRIGHT); }
                    if (caxis.axisValue < deadzone && gamepadStateSDL.LStickRight) { gamepadStateSDL.LStickRight = false; PostMessage(window, WM_KEYUP, VK.Active.LSRIGHT); }
                    if (caxis.axisValue <= -deadzone && !gamepadStateSDL.LStickLeft) { gamepadStateSDL.LStickLeft = true; PostMessage(window, WM_KEYDOWN, VK.Active.LSLEFT); }
                    //if (caxis.axisValue > -deadzone && caxis.axisValue < 0 && gamepadStateSDL.LStickLeft) { gamepadStateSDL.LStickLeft = false; PostMessage(window, WM_KEYUP, VK.Active.LSLEFT); }
                    if (caxis.axisValue > -deadzone && gamepadStateSDL.LStickLeft) { gamepadStateSDL.LStickLeft = false; PostMessage(window, WM_KEYUP, VK.Active.LSLEFT); }
                }
                if (caxis.axis == 1) // Left Stick Up/Down
                {
                    gamepadStateSDL.LStickValue_V = caxis.axisValue;
                    if (caxis.axisValue >= deadzone && !gamepadStateSDL.LStickDown) { gamepadStateSDL.LStickDown = true; PostMessage(window, WM_KEYDOWN, VK.Active.LSDOWN); }
                    //if (caxis.axisValue > 0 && caxis.axisValue < deadzone && gamepadStateSDL.LStickDown) { gamepadStateSDL.LStickDown = false; PostMessage(window, WM_KEYUP, VK.Active.LSDOWN); }
                    if (caxis.axisValue < deadzone && gamepadStateSDL.LStickDown) { gamepadStateSDL.LStickDown = false; PostMessage(window, WM_KEYUP, VK.Active.LSDOWN); }
                    if (caxis.axisValue <= -deadzone && !gamepadStateSDL.LStickUp) { gamepadStateSDL.LStickUp = true; PostMessage(window, WM_KEYDOWN, VK.Active.LSUP); }
                    //if (caxis.axisValue > -deadzone && caxis.axisValue < 0 && gamepadStateSDL.LStickUp) { gamepadStateSDL.LStickUp = false; PostMessage(window, WM_KEYUP, VK.Active.LSUP); }
                    if (caxis.axisValue > -deadzone && gamepadStateSDL.LStickUp) { gamepadStateSDL.LStickUp = false; PostMessage(window, WM_KEYUP, VK.Active.LSUP); }
                }
                if (caxis.axis == 2) // Right Stick Left/Right
                {
                    gamepadStateSDL.RStickValue_H = caxis.axisValue;
                    if (caxis.axisValue >= deadzone && !gamepadStateSDL.RStickRight) { gamepadStateSDL.RStickRight = true; PostMessage(window, WM_KEYDOWN, VK.Active.RSRIGHT); }
                    if (caxis.axisValue < deadzone && gamepadStateSDL.RStickRight) { gamepadStateSDL.RStickRight = false; PostMessage(window, WM_KEYUP, VK.Active.RSRIGHT); }
                    if (caxis.axisValue <= -deadzone && !gamepadStateSDL.RStickLeft) { gamepadStateSDL.RStickLeft = true; PostMessage(window, WM_KEYDOWN, VK.Active.RSLEFT); }
                    if (caxis.axisValue > -deadzone && gamepadStateSDL.RStickLeft) { gamepadStateSDL.RStickLeft = false; PostMessage(window, WM_KEYUP, VK.Active.RSLEFT); }
                }
                if (caxis.axis == 3) // Right Stick Up/Down
                {
                    gamepadStateSDL.RStickValue_V = caxis.axisValue;
                    if (caxis.axisValue >= deadzone && !gamepadStateSDL.RStickDown) { gamepadStateSDL.RStickDown = true; PostMessage(window, WM_KEYDOWN, VK.Active.RSDOWN); }
                    if (caxis.axisValue < deadzone && gamepadStateSDL.RStickDown) { gamepadStateSDL.RStickDown = false; PostMessage(window, WM_KEYUP, VK.Active.RSDOWN); }
                    if (caxis.axisValue <= -deadzone && !gamepadStateSDL.RStickUp) { gamepadStateSDL.RStickUp = true; PostMessage(window, WM_KEYDOWN, VK.Active.RSUP); }
                    if (caxis.axisValue > -deadzone && gamepadStateSDL.RStickUp) { gamepadStateSDL.RStickUp = false; PostMessage(window, WM_KEYUP, VK.Active.RSUP); }
                }
            }
        }

        private ControllerInfo PollController_DirectInput()
        {
            ControllerInfo info = new ControllerInfo();

            gamepadDI.Poll();
            JoystickUpdate[] states = gamepadDI.GetBufferedData();
            foreach (JoystickUpdate state in states)
            {
                // D-Pad
                if (state.Offset == JoystickOffset.PointOfViewControllers0)
                {
                    info.DpadUp = CalculateDPad_DirectInput(state.Value, "up");
                    info.DpadDown = CalculateDPad_DirectInput(state.Value, "down");
                    info.DpadLeft = CalculateDPad_DirectInput(state.Value, "left");
                    info.DpadRight = CalculateDPad_DirectInput(state.Value, "right");
                }

                // Options / PS Button
                if (windowName == zeldaIIWindowName && state.Offset == JoystickOffset.Buttons12 && state.Value == 128)
                {
                    info.Options = true;
                }
            }

            return info;
        }

        private bool CalculateDPad_DirectInput(int value, string direction)
        {
            bool pressed = false;

            if (direction == "up" && (value == 31500 || value == 0 || value == 4500))
            {
                pressed = true;
            }
            if (direction == "down" && (value == 13500 || value == 18000 || value == 22500))
            {
                pressed = true;
            }
            if (direction == "left" && (value == 22500 || value == 27000 || value == 31500))
            {
                pressed = true;
            }
            if (direction == "right" && (value == 4500 || value == 9000 || value == 13500))
            {
                pressed = true;
            }

            return pressed;
        }

        private void PostMessage(IntPtr window, int messageType, int keycode)
        {
            if (keycode == -1) return;
            if (!_isPaused)
            {
                if (game == Game.HollowKnight || game == Game.CaveStory || game == Game.OOTMM || game == Game.Minoria || game == Game.LostRuins)
                {
                    uint flags = 0;
                    if (messageType == 0x101) flags = 2;
                    INPUT input = new INPUT
                    {
                        Type = 1
                    };
                    input.Data.Keyboard = new KEYBDINPUT();
                    input.Data.Keyboard.Vk = (ushort)keycode;
                    input.Data.Keyboard.Scan = 0;
                    input.Data.Keyboard.Flags = flags;
                    input.Data.Keyboard.Time = 0;
                    input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
                    INPUT[] inputs = new INPUT[] { input };
                    SendInput(1, inputs, Marshal.SizeOf(input));
                }
                else
                {
                    PostMessage(window, messageType, keycode, IntPtr.Zero);
                    //keybd_event((byte)keycode, 0, messageType, 0);
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
        [StructLayout(LayoutKind.Sequential)]
        internal struct  INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }
        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }
    }
}