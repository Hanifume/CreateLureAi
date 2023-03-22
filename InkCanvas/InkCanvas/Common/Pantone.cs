using System.Collections.Generic;
using System.Xml.Serialization;

namespace InkCanvas.Common
{
    [XmlRoot("PantoneColors")]
    public class Pantone
    {
        [XmlArray("list")]
        [XmlArrayItem("PantoNone")]
        public List<PantoneColor> PantoneColors;

        public class PantoneColor
        {
            [XmlElement("ColorCode")]
            public string PantoneColorCode { get; set; }

            [XmlElement("R")]
            public int ColorR { get; set; }
            [XmlElement("G")]
            public int ColorG { get; set; }
            [XmlElement("B")]
            public int ColorB { get; set; }
            [XmlElement("HEX")]
            public string ColorHex { get; set; }
        }
    }
}
