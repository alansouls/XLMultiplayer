using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLMultiplayerServer.Extra.GameOfSkate
{
    [Serializable]
    public class TrickComboMessage
    {
        [JsonProperty("trick")]
        public string Trick { get; set; }

        [JsonProperty("bailed")]
        public bool Bailed { get; set; }

        [JsonProperty("aborted")]
        public bool Aborted { get; set; }

        [JsonProperty("landed")]
        public bool Landed { get => !Bailed && !Aborted; }


        public TrickComboMessage(TrickCombo trickCombo)
        {
            Trick = trickCombo.ToString();
            Bailed = trickCombo.Bailed;
            Aborted = trickCombo.Aborted;
        }
    }
}
