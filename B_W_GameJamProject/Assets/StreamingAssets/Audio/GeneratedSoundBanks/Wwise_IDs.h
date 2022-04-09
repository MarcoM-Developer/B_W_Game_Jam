/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_BTN_WIRE_SWITCH = 44324533U;
        static const AkUniqueID PLAY_ITM_PU_GENERIC = 3893138630U;
        static const AkUniqueID PLAY_MUSIC = 2932040671U;
        static const AkUniqueID PLAY_OBJ_MAP_ROTATE = 1549592196U;
        static const AkUniqueID PLAY_PLYR_COLLIDE_SWITCH = 3386221183U;
        static const AkUniqueID PLAY_PLYR_JUMP = 1864999798U;
        static const AkUniqueID PLAY_PLYR_LAND = 4227840961U;
        static const AkUniqueID PLAY_PLYR_MOVEMENT = 2271622313U;
        static const AkUniqueID PLAY_PLYR_SWITCH_CHARS = 2304023196U;
        static const AkUniqueID PLAY_TEST_BEEP = 27511013U;
        static const AkUniqueID PLAY_TEST_BEEP_LP = 2235959444U;
        static const AkUniqueID PLAY_UI_MOUSEOVER = 808445146U;
        static const AkUniqueID PLAY_UI_SELECT = 3308548503U;
        static const AkUniqueID STOP_PLYR_MOVEMENT = 369982735U;
        static const AkUniqueID STOP_TEST_BEEP = 701599435U;
        static const AkUniqueID STOP_TEST_BEEP_LP = 641054882U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace CURRENTPLAYER
        {
            static const AkUniqueID GROUP = 470853287U;

            namespace STATE
            {
                static const AkUniqueID BLACK = 3730873346U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID WHITE = 3178740632U;
            } // namespace STATE
        } // namespace CURRENTPLAYER

        namespace MUSIC
        {
            static const AkUniqueID GROUP = 3991942870U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID PLAYERSPEED = 1493153371U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ITEMS = 2151963051U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID OBJECTS = 1695690031U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
