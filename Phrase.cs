using System;

namespace onscripter_helper
{
    public class Phrase
    {
        public Int32 Position { get; set; }

        public String Text { get; set; }

        public Int32 Length
        {
            get { return Text.Length; }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
