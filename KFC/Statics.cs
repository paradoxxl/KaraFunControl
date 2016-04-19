using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC
{
    public enum WebMessageType
    {
        All,
        Status
    }

    public enum PlayerStatus
    {
        idle,
        playing,
        infoscreen
    }

    public enum SongAttributes
    {
        general,
        bv,
        lead1,
        lead2,
        pitch,
        tempo,
        title,
        artist,
        year,
        duration,
        position
    }

    public enum UsbStatus
    {
        connected,
        dicsonnected
    }


}
