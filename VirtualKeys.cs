
namespace PS5Mapper
{
    public enum Game
    {
        AM2R,
        CaveStory,
        ZeldaII,
        OOTMM,
        HollowKnight,
        Minoria,
        LostRuins,
        BloodStained
    }

    public struct VirtualController
    {
        public int DUP;
        public int DDOWN;
        public int DLEFT;
        public int DRIGHT;
        public int LSUP;
        public int LSDOWN;
        public int LSLEFT;
        public int LSRIGHT;
        public int RSUP;
        public int RSDOWN;
        public int RSLEFT;
        public int RSRIGHT;
        public int L1;
        public int L2;
        public int L3;
        public int R1;
        public int R2;
        public int R3;
        public int Square;
        public int Triangle;
        public int Circle;
        public int X;
        public int Options;
        public int Share;
        public int PS;
        public int Touchpad;
        public int Mic;

        public string DUPMap;
        public string DDOWNMap;
        public string DLEFTMap;
        public string DRIGHTMap;
        public string LSUPMap;
        public string LSDOWNMap;
        public string LSLEFTMap;
        public string LSRIGHTMap;
        public string RSUPMap;
        public string RSDOWNMap;
        public string RSLEFTMap;
        public string RSRIGHTMap;
        public string L1Map;
        public string L2Map;
        public string L3Map;
        public string R1Map;
        public string R2Map;
        public string R3Map;
        public string SquareMap;
        public string TriangleMap;
        public string CircleMap;
        public string XMap;
        public string OptionsMap;
        public string ShareMap;
        public string PSMap;
        public string TouchpadMap;
        public string MicMap;
    }

    internal class VirtualKeys
    {
        public VirtualController Active;
        public VirtualController AM2R;
        public VirtualController CaveStory;
        public VirtualController OOTMM;
        public VirtualController HollowKnight;
        public VirtualController ZeldaII;
        public VirtualController Minoria;
        public VirtualController LostRuins;
        public VirtualController BloodStained;

        private int VK_TAB = 0x09;
        private int VK_RETURN = 0x0D;
        private int VK_SHIFT = 0x10;
        private int VK_CONTROL = 0x11;
        private int VK_ESCAPE = 0x1B;
        private int VK_SPACE = 0x20;
        private int VK_PAGEUP = 0x21;
        private int VK_PAGEDOWN = 0x22;
        private int VK_END = 0x23;
        private int VK_HOME = 0x24;
        private int VK_LEFT = 0x25;
        private int VK_UP = 0x26;
        private int VK_RIGHT = 0x27;
        private int VK_DOWN = 0x28;
        private int VK_DELETE = 0x2E;
        private int VK_A = 0x41;
        private int VK_B = 0x42;
        private int VK_C = 0x43;
        private int VK_D = 0x44;
        private int VK_E = 0x45;
        private int VK_F = 0x46;
        private int VK_G = 0x47;
        private int VK_H = 0x48;
        private int VK_I = 0x49;
        private int VK_J = 0x4A;
        private int VK_K = 0x4B;
        private int VK_L = 0x4C;
        private int VK_M = 0x4D;
        private int VK_N = 0x4E;
        private int VK_O = 0x4F;
        private int VK_P = 0x50;
        private int VK_Q = 0x51;
        private int VK_R = 0x52;
        private int VK_S = 0x53;
        private int VK_T = 0x54;
        private int VK_U = 0x55;
        private int VK_V = 0x56;
        private int VK_W = 0x57;
        private int VK_X = 0x58;
        private int VK_Y = 0x59;
        private int VK_Z = 0x5A;

        public VirtualKeys()
        {
            AM2R.DUP = VK_UP;
            AM2R.DDOWN = VK_DOWN;
            AM2R.DLEFT = VK_LEFT;
            AM2R.DRIGHT = VK_RIGHT;
            AM2R.LSUP = VK_UP;
            AM2R.LSDOWN = VK_DOWN;
            AM2R.LSLEFT = VK_LEFT;
            AM2R.LSRIGHT = VK_RIGHT;
            AM2R.RSUP = -1;
            AM2R.RSDOWN = -1;
            AM2R.RSLEFT = -1;
            AM2R.RSRIGHT = -1;
            AM2R.L1 = VK_C;
            AM2R.L2 = VK_CONTROL;
            AM2R.L3 = -1;
            AM2R.R1 = VK_SHIFT;
            AM2R.R2 = VK_SPACE;
            AM2R.R3 = -1;
            AM2R.Square = VK_X;
            AM2R.Triangle = VK_A;
            AM2R.Circle = VK_S;
            AM2R.X = VK_Z;
            AM2R.Options = VK_RETURN;
            AM2R.Share = -1;
            AM2R.PS = -1;
            AM2R.Touchpad = -1;
            AM2R.Mic = -1;
            AM2R.DUPMap = "Up";
            AM2R.DDOWNMap = "Down";
            AM2R.DLEFTMap = "Left";
            AM2R.DRIGHTMap = "Right";
            AM2R.LSUPMap = "Up";
            AM2R.LSDOWNMap = "Down";
            AM2R.LSLEFTMap = "Left";
            AM2R.LSRIGHTMap = "Right";
            AM2R.RSUPMap = "none";
            AM2R.RSDOWNMap = "none";
            AM2R.RSLEFTMap = "none";
            AM2R.RSRIGHTMap = "none";
            AM2R.L1Map = "C";
            AM2R.L2Map = "Control";
            AM2R.L3Map = "none";
            AM2R.R1Map = "Shift";
            AM2R.R2Map = "Space";
            AM2R.R3Map = "none";
            AM2R.SquareMap = "X";
            AM2R.TriangleMap = "A";
            AM2R.CircleMap = "S";
            AM2R.XMap = "Z";
            AM2R.OptionsMap = "Enter";
            AM2R.ShareMap = "none";
            AM2R.PSMap = "none";
            AM2R.TouchpadMap = "none";
            AM2R.MicMap = "none";

            CaveStory.DUP = VK_UP;
            CaveStory.DDOWN = VK_DOWN;
            CaveStory.DLEFT = VK_LEFT;
            CaveStory.DRIGHT = VK_RIGHT;
            CaveStory.LSUP = VK_UP;
            CaveStory.LSDOWN = VK_DOWN;
            CaveStory.LSLEFT = VK_LEFT;
            CaveStory.LSRIGHT = VK_RIGHT;
            CaveStory.RSUP = -1;
            CaveStory.RSDOWN = -1;
            CaveStory.RSLEFT = -1;
            CaveStory.RSRIGHT = -1;
            CaveStory.L1 = -1; //VK_A; // Previous Weapon
            CaveStory.L2 = -1; //VK_C; // Strafe
            CaveStory.L3 = -1;
            CaveStory.R1 = -1; //VK_S; // Next Weapon
            CaveStory.R2 = -1; //VK_C; // Strafe
            CaveStory.R3 = -1;
            CaveStory.Square = -1; //VK_J; // Shoot / Cancel
            CaveStory.Triangle = -1; //VK_I; // Inventory
            CaveStory.Circle = -1; //VK_L; // Shoot / Cancel
            CaveStory.X = -1; //VK_K; // Jump / Accept
            CaveStory.Options = VK_ESCAPE; // Pause
            CaveStory.Share = -1;
            CaveStory.PS = -1;
            CaveStory.Touchpad = -1; //VK_W; // Map
            CaveStory.Mic = -1;
            CaveStory.DUPMap = "Up";
            CaveStory.DDOWNMap = "Down";
            CaveStory.DLEFTMap = "Left";
            CaveStory.DRIGHTMap = "Right";
            CaveStory.LSUPMap = "Up";
            CaveStory.LSDOWNMap = "Down";
            CaveStory.LSLEFTMap = "Left";
            CaveStory.LSRIGHTMap = "Right";
            CaveStory.RSUPMap = "none";
            CaveStory.RSDOWNMap = "none";
            CaveStory.RSLEFTMap = "none";
            CaveStory.RSRIGHTMap = "none";
            CaveStory.L1Map = "none";
            CaveStory.L2Map = "none";
            CaveStory.L3Map = "none";
            CaveStory.R1Map = "none";
            CaveStory.R2Map = "none";
            CaveStory.R3Map = "none";
            CaveStory.SquareMap = "none";
            CaveStory.TriangleMap = "none";
            CaveStory.CircleMap = "none";
            CaveStory.XMap = "none";
            CaveStory.OptionsMap = "none";
            CaveStory.ShareMap = "none";
            CaveStory.PSMap = "none";
            CaveStory.TouchpadMap = "none";
            CaveStory.MicMap = "none";

            OOTMM.DUP = VK_I;
            OOTMM.DDOWN = VK_K;
            OOTMM.DLEFT = VK_J;
            OOTMM.DRIGHT = VK_L;
            OOTMM.LSUP = VK_UP;
            OOTMM.LSDOWN = VK_DOWN;
            OOTMM.LSLEFT = VK_LEFT;
            OOTMM.LSRIGHT = VK_RIGHT;
            OOTMM.RSUP = VK_HOME;
            OOTMM.RSDOWN = VK_END;
            OOTMM.RSLEFT = VK_PAGEDOWN;
            OOTMM.RSRIGHT = VK_DELETE;
            OOTMM.L1 = VK_Z;
            OOTMM.L2 = VK_A;
            OOTMM.L3 = -1;
            OOTMM.R1 = -1;
            OOTMM.R2 = VK_S;
            OOTMM.R3 = -1;
            OOTMM.Square = VK_C;
            OOTMM.Triangle = VK_C;
            OOTMM.Circle = VK_X;
            OOTMM.X = VK_X;
            OOTMM.Options = VK_RETURN;
            OOTMM.Share = -1;
            OOTMM.PS = -1;
            OOTMM.Touchpad = -1;
            OOTMM.Mic = -1;
            OOTMM.DUPMap = "Up";
            OOTMM.DDOWNMap = "Down";
            OOTMM.DLEFTMap = "Left";
            OOTMM.DRIGHTMap = "Right";
            OOTMM.LSUPMap = "Up";
            OOTMM.LSDOWNMap = "Down";
            OOTMM.LSLEFTMap = "Left";
            OOTMM.LSRIGHTMap = "Right";
            OOTMM.RSUPMap = "none";
            OOTMM.RSDOWNMap = "none";
            OOTMM.RSLEFTMap = "none";
            OOTMM.RSRIGHTMap = "none";
            OOTMM.L1Map = "none";
            OOTMM.L2Map = "none";
            OOTMM.L3Map = "none";
            OOTMM.R1Map = "none";
            OOTMM.R2Map = "none";
            OOTMM.R3Map = "none";
            OOTMM.SquareMap = "none";
            OOTMM.TriangleMap = "none";
            OOTMM.CircleMap = "none";
            OOTMM.XMap = "none";
            OOTMM.OptionsMap = "none";
            OOTMM.ShareMap = "none";
            OOTMM.PSMap = "none";
            OOTMM.TouchpadMap = "none";
            OOTMM.MicMap = "none";

            HollowKnight.DUP = VK_UP;
            HollowKnight.DDOWN = VK_DOWN;
            HollowKnight.DLEFT = VK_LEFT;
            HollowKnight.DRIGHT = VK_RIGHT;
            HollowKnight.LSUP = VK_UP;
            HollowKnight.LSDOWN = VK_DOWN;
            HollowKnight.LSLEFT = VK_LEFT;
            HollowKnight.LSRIGHT = VK_RIGHT;
            HollowKnight.RSUP = -1;
            HollowKnight.RSDOWN = -1;
            HollowKnight.RSLEFT = -1;
            HollowKnight.RSRIGHT = -1;
            HollowKnight.L1 = VK_V; // Quick Cast
            HollowKnight.L2 = VK_D; // Dream Nail
            HollowKnight.L3 = -1;
            HollowKnight.R1 = VK_S; // Dash
            HollowKnight.R2 = VK_F; // Super Dash
            HollowKnight.R3 = -1;
            HollowKnight.Square = VK_X; // Attack / Cancel
            HollowKnight.Triangle = VK_I; // Inventory
            HollowKnight.Circle = VK_B; // Focus / Cast
            HollowKnight.X = VK_Z; // Jump / Accept
            HollowKnight.Options = VK_ESCAPE;
            HollowKnight.Share = -1;
            HollowKnight.PS = -1;
            HollowKnight.Touchpad = VK_TAB;
            HollowKnight.Mic = -1;
            HollowKnight.DUPMap = "Up";
            HollowKnight.DDOWNMap = "Down";
            HollowKnight.DLEFTMap = "Left";
            HollowKnight.DRIGHTMap = "Right";
            HollowKnight.LSUPMap = "Up";
            HollowKnight.LSDOWNMap = "Down";
            HollowKnight.LSLEFTMap = "Left";
            HollowKnight.LSRIGHTMap = "Right";
            HollowKnight.RSUPMap = "none";
            HollowKnight.RSDOWNMap = "none";
            HollowKnight.RSLEFTMap = "none";
            HollowKnight.RSRIGHTMap = "none";
            HollowKnight.L1Map = "V - Quck Cast";
            HollowKnight.L2Map = "D - Dream Nail";
            HollowKnight.L3Map = "none";
            HollowKnight.R1Map = "S - Dash";
            HollowKnight.R2Map = "F - Super Dash";
            HollowKnight.R3Map = "none";
            HollowKnight.SquareMap = "X - Attack / Cancel";
            HollowKnight.TriangleMap = "I - Inventory";
            HollowKnight.CircleMap = "B - Focus / Cast";
            HollowKnight.XMap = "Z - Jump / Accept";
            HollowKnight.OptionsMap = "Escape - Menu";
            HollowKnight.ShareMap = "none";
            HollowKnight.PSMap = "none";
            HollowKnight.TouchpadMap = "Tab - Map";
            HollowKnight.MicMap = "none";

            ZeldaII.DUP = VK_UP;
            ZeldaII.DDOWN = VK_DOWN;
            ZeldaII.DLEFT = VK_LEFT;
            ZeldaII.DRIGHT = VK_RIGHT;
            ZeldaII.LSUP = VK_UP;
            ZeldaII.LSDOWN = VK_DOWN;
            ZeldaII.LSLEFT = VK_LEFT;
            ZeldaII.LSRIGHT = VK_RIGHT;
            ZeldaII.RSUP = -1;
            ZeldaII.RSDOWN = -1;
            ZeldaII.RSLEFT = -1;
            ZeldaII.RSRIGHT = -1;
            ZeldaII.L1 = -1;
            ZeldaII.L2 = -1;
            ZeldaII.L3 = -1;
            ZeldaII.R1 = -1;
            ZeldaII.R2 = -1;
            ZeldaII.R3 = -1;
            ZeldaII.Square = VK_CONTROL;
            ZeldaII.Triangle = VK_T;
            ZeldaII.Circle = -1;
            ZeldaII.X = VK_SPACE;
            ZeldaII.Options = VK_RETURN;
            ZeldaII.Share = -1;
            ZeldaII.PS = -1;
            ZeldaII.Touchpad = VK_O;
            ZeldaII.Mic = -1;
            ZeldaII.DUPMap = "Up";
            ZeldaII.DDOWNMap = "Down";
            ZeldaII.DLEFTMap = "Left";
            ZeldaII.DRIGHTMap = "Right";
            ZeldaII.LSUPMap = "Up";
            ZeldaII.LSDOWNMap = "Down";
            ZeldaII.LSLEFTMap = "Left";
            ZeldaII.LSRIGHTMap = "Right";
            ZeldaII.RSUPMap = "none";
            ZeldaII.RSDOWNMap = "none";
            ZeldaII.RSLEFTMap = "none";
            ZeldaII.RSRIGHTMap = "none";
            ZeldaII.L1Map = "none";
            ZeldaII.L2Map = "none";
            ZeldaII.L3Map = "none";
            ZeldaII.R1Map = "none";
            ZeldaII.R2Map = "none";
            ZeldaII.R3Map = "none";
            ZeldaII.SquareMap = "Control";
            ZeldaII.TriangleMap = "T";
            ZeldaII.CircleMap = "none";
            ZeldaII.XMap = "Space";
            ZeldaII.OptionsMap = "Enter";
            ZeldaII.ShareMap = "none";
            ZeldaII.PSMap = "none";
            ZeldaII.TouchpadMap = "O";
            ZeldaII.MicMap = "none";

            Minoria.DUP = VK_UP;
            Minoria.DDOWN = VK_DOWN;
            Minoria.DLEFT = VK_LEFT;
            Minoria.DRIGHT = VK_RIGHT;
            Minoria.LSUP = VK_UP;
            Minoria.LSDOWN = VK_DOWN;
            Minoria.LSLEFT = VK_LEFT;
            Minoria.LSRIGHT = VK_RIGHT;
            Minoria.RSUP = -1;
            Minoria.RSDOWN = -1;
            Minoria.RSLEFT = -1;
            Minoria.RSRIGHT = -1;
            Minoria.L1 = VK_E; // Previous Incense
            Minoria.L2 = VK_W; // Chat with Partner
            Minoria.L3 = -1;
            Minoria.R1 = VK_R; // Next Incense
            Minoria.R2 = VK_W; // Chat with Partner
            Minoria.R3 = -1;
            Minoria.Square = VK_S; // Attack
            Minoria.Triangle = VK_D; // Use Incense
            Minoria.Circle = VK_Q; // Dodge/Parry / Cancel
            Minoria.X = VK_A; // Jump / Accept
            Minoria.Options = VK_RETURN; // Options
            Minoria.Share = -1;
            Minoria.PS = -1;
            Minoria.Touchpad = VK_TAB; // Map
            Minoria.Mic = -1;
            Minoria.DUPMap = "Up";
            Minoria.DDOWNMap = "Down";
            Minoria.DLEFTMap = "Left";
            Minoria.DRIGHTMap = "Right";
            Minoria.LSUPMap = "Up";
            Minoria.LSDOWNMap = "Down";
            Minoria.LSLEFTMap = "Left";
            Minoria.LSRIGHTMap = "Right";
            Minoria.RSUPMap = "none";
            Minoria.RSDOWNMap = "none";
            Minoria.RSLEFTMap = "none";
            Minoria.RSRIGHTMap = "none";
            Minoria.L1Map = "E - Previous Incense";
            Minoria.L2Map = "W - Chat with Partner";
            Minoria.L3Map = "none";
            Minoria.R1Map = "R - Next Incense";
            Minoria.R2Map = "W - Chat with Partner";
            Minoria.R3Map = "none";
            Minoria.SquareMap = "S - Attack";
            Minoria.TriangleMap = "D - Use Incense";
            Minoria.CircleMap = "Q - Parry / Cancel";
            Minoria.XMap = "A - Jump / Accept";
            Minoria.OptionsMap = "Enter - Menu";
            Minoria.ShareMap = "none";
            Minoria.PSMap = "none";
            Minoria.TouchpadMap = "Tab - Map";
            Minoria.MicMap = "none";

            LostRuins.DUP = VK_UP;
            LostRuins.DDOWN = VK_DOWN;
            LostRuins.DLEFT = VK_LEFT;
            LostRuins.DRIGHT = VK_RIGHT;
            LostRuins.LSUP = VK_UP;
            LostRuins.LSDOWN = VK_DOWN;
            LostRuins.LSLEFT = VK_LEFT;
            LostRuins.LSRIGHT = VK_RIGHT;
            LostRuins.RSUP = -1;
            LostRuins.RSDOWN = -1;
            LostRuins.RSLEFT = -1;
            LostRuins.RSRIGHT = -1;
            LostRuins.L1 = VK_A; // Spell 1
            LostRuins.L2 = VK_Q; // Left Tab
            LostRuins.L3 = VK_V; // Sort
            LostRuins.R1 = VK_S; // Spell 2
            LostRuins.R2 = VK_W; // Right Tab
            LostRuins.R3 = VK_F; // Favorite
            LostRuins.Square = VK_Z; // Weapon 1
            LostRuins.Triangle = VK_X; // Weapon 2
            LostRuins.Circle = VK_C; // Roll
            LostRuins.X = VK_SPACE; // Jump
            LostRuins.Options = VK_TAB; // Inventory
            LostRuins.Share = -1;
            LostRuins.PS = -1;
            LostRuins.Touchpad = VK_M; // Map
            LostRuins.Mic = -1;
            LostRuins.DUPMap = "Up";
            LostRuins.DDOWNMap = "Down";
            LostRuins.DLEFTMap = "Left";
            LostRuins.DRIGHTMap = "Right";
            LostRuins.LSUPMap = "Up";
            LostRuins.LSDOWNMap = "Down";
            LostRuins.LSLEFTMap = "Left";
            LostRuins.LSRIGHTMap = "Right";
            LostRuins.RSUPMap = "none";
            LostRuins.RSDOWNMap = "none";
            LostRuins.RSLEFTMap = "none";
            LostRuins.RSRIGHTMap = "none";
            LostRuins.L1Map = "A - Spell 1";
            LostRuins.L2Map = "Q - Left Tab";
            LostRuins.L3Map = "V - Sort";
            LostRuins.R1Map = "S - Spell 2";
            LostRuins.R2Map = "W - Right Tab";
            LostRuins.R3Map = "F - Favorite";
            LostRuins.SquareMap = "Z - Weapon 1";
            LostRuins.TriangleMap = "X - Weapon 2";
            LostRuins.CircleMap = "C - Roll";
            LostRuins.XMap = "Space - Jump";
            LostRuins.OptionsMap = "Tab - Inventory";
            LostRuins.ShareMap = "none";
            LostRuins.PSMap = "none";
            LostRuins.TouchpadMap = "M - Map";
            LostRuins.MicMap = "none";

            BloodStained.DUP = VK_UP;
            BloodStained.DDOWN = VK_DOWN;
            BloodStained.DLEFT = VK_LEFT;
            BloodStained.DRIGHT = VK_RIGHT;
            BloodStained.LSUP = VK_UP;
            BloodStained.LSDOWN = VK_DOWN;
            BloodStained.LSLEFT = VK_LEFT;
            BloodStained.LSRIGHT = VK_RIGHT;
            BloodStained.RSUP = -1;
            BloodStained.RSDOWN = -1;
            BloodStained.RSLEFT = -1;
            BloodStained.RSRIGHT = -1;
            BloodStained.L1 = VK_Q; // Backstep
            BloodStained.L2 = VK_TAB; // Shortcut
            BloodStained.L3 = -1;
            BloodStained.R1 = VK_E; // Use Manipulative Shard
            BloodStained.R2 = VK_G; // Use Directional Shard
            BloodStained.R3 = VK_H; // Aim
            BloodStained.Square = VK_C; // Attack
            BloodStained.Triangle = VK_F; // Use Conjure Shard
            BloodStained.Circle = VK_X; // Back
            BloodStained.X = VK_Z; // Jump / Confirm
            BloodStained.Options = VK_R; // Main Menu
            BloodStained.Share = -1;
            BloodStained.PS = -1;
            BloodStained.Touchpad = VK_T; // View Map
            BloodStained.Mic = -1;
            BloodStained.DUPMap = "Up";
            BloodStained.DDOWNMap = "Down";
            BloodStained.DLEFTMap = "Left";
            BloodStained.DRIGHTMap = "Right";
            BloodStained.LSUPMap = "Up";
            BloodStained.LSDOWNMap = "Down";
            BloodStained.LSLEFTMap = "Left";
            BloodStained.LSRIGHTMap = "Right";
            BloodStained.RSUPMap = "none";
            BloodStained.RSDOWNMap = "none";
            BloodStained.RSLEFTMap = "none";
            BloodStained.RSRIGHTMap = "none";
            BloodStained.L1Map = "A - Spell 1";
            BloodStained.L2Map = "Q - Left Tab";
            BloodStained.L3Map = "V - Sort";
            BloodStained.R1Map = "S - Spell 2";
            BloodStained.R2Map = "W - Right Tab";
            BloodStained.R3Map = "F - Favorite";
            BloodStained.SquareMap = "Z - Weapon 1";
            BloodStained.TriangleMap = "X - Weapon 2";
            BloodStained.CircleMap = "C - Roll";
            BloodStained.XMap = "Space - Jump";
            BloodStained.OptionsMap = "Tab - Inventory";
            BloodStained.ShareMap = "none";
            BloodStained.PSMap = "none";
            BloodStained.TouchpadMap = "M - Map";
            BloodStained.MicMap = "none";
        }

        public void SetGame(Game game)
        {
            switch (game)
            {
                case Game.AM2R: Active = AM2R; break;
                case Game.CaveStory: Active = CaveStory; break;
                case Game.OOTMM: Active = OOTMM; break;
                case Game.ZeldaII: Active = ZeldaII; break;
                case Game.HollowKnight: Active = HollowKnight; break;
                case Game.Minoria: Active = Minoria; break;
                case Game.LostRuins: Active = LostRuins; break;
                case Game.BloodStained: Active = BloodStained; break;
            }
        }
    }
}

