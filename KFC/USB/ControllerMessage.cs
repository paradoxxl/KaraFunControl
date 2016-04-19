using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaraFunControl.USB
{
    class ControllerMessage
    {
        private const int GeneralVolumeSliderBytePos = 0;
        private const int VoiceVolumeSliderBytePos = 1;
        private const int MaleVolumeSliderBytePos = 2;
        private const int FemaleVolumeSliderBytePos = 3;
        private const int ButtonsBytePos = 4;

        private const int PlayPauseBtnMask = 1;
        private const int NextBtnMask = 2;
        private const int PrevBtnMask = 4;
        private const int KeyDownBtnMask = 8;
        private const int KeyUpBtnMask = 16;
        private const int PitchDownBtnMask = 32;
        private const int PitchUpBtnMask = 64;
        private const int RecordBtnMask = 128;

        private const int DataLength = 6;


        public bool PlayPauseBtnPressed { get;  }
        public bool NextBtnPressed { get; }
        public bool PrevBtnPressed { get; }
        public bool KeyDownBtnPressed { get; }
        public bool KeyUpBtnPressed { get; }
        public bool PitchDownBtnPressed { get; }
        public bool PitchUpBtnPressed { get; }
        public bool RecordBtnPressed { get; }

        public int GeneralVolumeSliderPos { get;  }
        public int VoiceVolumeSliderPos { get; }
        public int MaleVolumeSliderPos { get; }
        public int FemaleVolumeSliderPos { get; }
        public ControllerMessage(byte[] data)
        {
            if (data == null || data.Length != DataLength) throw new InvalidCastException("Cannot convert gamepad message to 32 bit integer.");
            GeneralVolumeSliderPos = data[GeneralVolumeSliderBytePos];
            VoiceVolumeSliderPos = data[VoiceVolumeSliderBytePos];
            MaleVolumeSliderPos = data[MaleVolumeSliderBytePos];
            FemaleVolumeSliderPos = data[FemaleVolumeSliderBytePos];

            var buttonData = data[ButtonsBytePos];

            PlayPauseBtnPressed = (buttonData & PlayPauseBtnMask) != 0;
            NextBtnPressed = (buttonData & NextBtnMask) != 0;
            PrevBtnPressed = (buttonData & PrevBtnMask) != 0;
            KeyDownBtnPressed = (buttonData & KeyDownBtnMask) != 0;
            KeyUpBtnPressed = (buttonData & KeyUpBtnMask) != 0;
            PitchDownBtnPressed = (buttonData & PitchDownBtnMask) != 0;
            PitchUpBtnPressed = (buttonData & PitchUpBtnMask) != 0;
            RecordBtnPressed = (buttonData & RecordBtnMask) != 0;
        }












    }
}
